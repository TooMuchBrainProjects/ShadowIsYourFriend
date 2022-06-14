using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

    static public AudioManager Instance { get; private set; }

    static public bool SetInstance(AudioManager value)
    {
        if (Instance != null)
        {
            Debug.Log("There is another AudioManger Instance!");
            return false;
        }
        else
        {
            Instance = value;
            return true;
        }
    } 

    private void Awake()
    {
        if (!SetInstance(this))
            Destroy(this.gameObject);

        DontDestroyOnLoad(this);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = audioMixer;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void StopWithFade(string soundName,float duration)
    {
        SetSoundState(GetSound(soundName), new Sound.AudioState(Sound.AudioState.Action.Stop, Sound.AudioState.Fade.FadeOut,duration));
    }

    public void PauseWithFade(string soundName, float duration)
    {
        SetSoundState(GetSound(soundName), new Sound.AudioState(Sound.AudioState.Action.Pause, Sound.AudioState.Fade.FadeOut,duration));
    }

    public void Stop(string soundName)
    {
        SetSoundState(GetSound(soundName), new Sound.AudioState(Sound.AudioState.Action.Stop,Sound.AudioState.Fade.NoFade));
    }

    public void Pause(string soundName)
    {
        SetSoundState(GetSound(soundName), new Sound.AudioState(Sound.AudioState.Action.Pause, Sound.AudioState.Fade.NoFade));
    }
    public void Play(string soundName)
    {
        SetSoundState(GetSound(soundName), new Sound.AudioState(Sound.AudioState.Action.Play, Sound.AudioState.Fade.NoFade));
    }

    private void SetSoundState(Sound s, Sound.AudioState newstate)
    {
        if(newstate.action == Sound.AudioState.Action.Play && s.source.isPlaying != true) { }
        else if (s.state.action == newstate.action)
            return;

        if (s.state.fader != null)
        {
            StopCoroutine(s.state.fader);
            s.state.fader = null;
        }
        


        Action action = () => { };
        switch (newstate.action)
        {
            case Sound.AudioState.Action.Stop:
                action = () => { s.source.Stop(); s.source.volume = s.volume; };
                break;
            case Sound.AudioState.Action.Play:
                action = () => { s.source.Play(); s.source.volume = s.volume; };

                break;
            case Sound.AudioState.Action.Pause:
                action = () => { s.source.Pause(); s.source.volume = s.volume; };
                break;
        }

        if (newstate.fade != Sound.AudioState.Fade.NoFade)
        {
            if (newstate.action == Sound.AudioState.Action.Play)
            {
                action();
                action = () => { };
            }

            newstate.fader = StartCoroutine(FadeAudioSource.StartFade(s.source, newstate.fade_duration, newstate.fade_target, action));
        }
        else
            action();

        s.state = newstate;
    }

    private Sound GetSound(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s == null)
        {
            Debug.LogError("Cant find Sound " + soundName);
            return null;
        }      
        else
            return s;
    }
}


[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    
    [Range(0f,1f)]
    public float volume = 1f;
    [Range(.1f, 3f)]
    public float pitch = 1f;
    public bool loop = false;

    [HideInInspector] public AudioSource source;

    [HideInInspector] public AudioState state;

    public struct AudioState
    {
        public enum Action { Stop, Play, Pause };
        public enum Fade { NoFade, FadeIn, FadeOut };
        public Fade fade;
        public Action action;

        public float fade_duration;
        public float fade_target;
        public Coroutine fader;
        
        public AudioState(Action a, Fade f, float fade_duration = 0f, float fade_target = 0f) 
        { 
            this.fade = f; 
            this.action = a; 
            this.fade_duration = fade_duration; 
            this.fade_target = fade_target;
            fader = null;
        }
    }
}

public static class FadeAudioSource
{
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume, Action onEnd = null)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        if (onEnd != null)
            onEnd();

        yield break;
    }
}
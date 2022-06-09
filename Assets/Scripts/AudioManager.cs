using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;
    public Sound[] sounds;

    private void Awake()
    {
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
        Sound s = GetSound(soundName);
        IEnumerator e = FadeAudioSource.StartFade(s.source, duration, 0, () => { s.fader.Item1 = Fade.NoFade; s.fader.Item2 = null; Stop(s);});
        Stop(s, e);
    }

    public void PauseWithFade(string soundName, float duration)
    {
        Sound s = GetSound(soundName);
        IEnumerator e = FadeAudioSource.StartFade(s.source, duration, 0, () => { s.fader.Item1 = Fade.NoFade; s.fader.Item2 = null; Pause(s);});
        Pause(s, e);
    }

    public void Stop(string soundName)
    {
        Stop(GetSound(soundName));
    }

    public void Pause(string soundName)
    {
        Pause(GetSound(soundName));
    }
    public void Play(string soundName)
    {
        Play(GetSound(soundName));
    }

    private void Stop(Sound s, IEnumerator fade = null)
    {
        switch (s.fader.Item1)
        {
            case Fade.OutToPause:
                StopCoroutine(s.fader.Item2);
                s.fader.Item2 = null;
                s.fader.Item1 = Fade.NoFade;
                s.source.Stop();
                s.source.volume = s.volume;
                break;
            case Fade.NoFade:
                if(fade != null)
                {
                    s.fader.Item1 = Fade.OutToStop;
                    s.fader.Item2 = StartCoroutine(fade);
                }
                else
                {
                    s.source.Stop();
                    s.source.volume = s.volume;
                }
                break;
            case Fade.OutToStop:
                break;
        }
    }

    private void Pause(Sound s, IEnumerator fade = null)
    {
        switch (s.fader.Item1)
        {
            case Fade.OutToStop:
                StopCoroutine(s.fader.Item2);
                s.fader.Item2 = null;
                s.fader.Item1 = Fade.NoFade;
                s.source.Pause();
                s.source.volume = s.volume;
                break;
            case Fade.NoFade:
                if (fade != null)
                {
                    s.fader.Item1 = Fade.OutToPause;
                    s.fader.Item2 = StartCoroutine(fade);
                }
                else if (s.source.isPlaying == true)
                {
                    s.source.Pause();
                    s.source.volume = s.volume;
                }
                break;

            case Fade.OutToPause:
                break;
        }
    }

    private void Play(Sound s)
    {
        switch (s.fader.Item1)
        {
            case Fade.OutToStop:
            case Fade.OutToPause:
                StopCoroutine(s.fader.Item2);
                s.fader.Item2 = null;
                s.fader.Item1 = Fade.NoFade;
                s.source.volume = s.volume;
                break;
            case Fade.NoFade:
                s.source.volume = s.volume;
                if (s.source.isPlaying != true)
                    s.source.Play();
                break;
        }
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

public enum Fade { NoFade, OutToPause, OutToStop };

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

    [HideInInspector]
    public AudioSource source;

    [HideInInspector]
    public (Fade,Coroutine) fader;
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
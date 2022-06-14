using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsernameManager : MonoBehaviour
{
    public string Username
    {
        get { return PlayerPrefs.GetString("Username"); }
        set { PlayerPrefs.SetString("Username", value); }
    }
}

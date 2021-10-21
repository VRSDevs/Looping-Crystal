using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class globalParameters
{
    public bool activatedSounds;
    public float musicVolume;
    public float soundVolume;

    void Awake() {
        activatedSounds = true;
        musicVolume = 1.0f;
        soundVolume = 1.0f;
    }
}

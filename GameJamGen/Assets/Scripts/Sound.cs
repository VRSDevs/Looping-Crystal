using UnityEngine.Audio;
using UnityEngine;

public enum soundTypes {
    M,
    S
}

[System.Serializable]   // Permite mostrar esta clase propia en el inspector
public class Sound
{
    public AudioClip clip;
    [HideInInspector]public AudioSource source;     // Esta variable no se mostrará en el inspector

    public string name;
    public soundTypes type;

    [Range(0f,1f)] public float volume;
    [Range(.1f, 3f)] public float pitch;
    [SerializeField]public bool loop;
}

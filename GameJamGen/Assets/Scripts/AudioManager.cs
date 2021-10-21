using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // ANTES DE START
    void Awake()
    {
        // Añadir componente de audio a cada sonido en el array
        foreach (Sound s in sounds)
        {
            // Añadir componente de AudioSource al sonido
            s.source = gameObject.AddComponent<AudioSource>();

            // Asignación de atributos del sonido en el inspector a los correspondientes del AudioSource
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeMusic(AudioClip song) {
        Sound soundToChange = Array.Find(sounds, sound => sound.name == "BGMusic");
        soundToChange.source.Stop();
        soundToChange.source.clip = song;
        soundToChange.source.Play();
    }

    public void Play(string soundName) {
        // Buscar en el array de sonidos aquel que tiene el mismo nombre del que se quiere reproducir
        Sound soundToPlay = Array.Find(sounds, sound => sound.name == soundName);

        // Reproducir sonido
        soundToPlay.source.Play();
    }

    public void Stop(string soundName) {
        Sound soundToStop = Array.Find(sounds, sound => sound.name == soundName);

        soundToStop.source.Stop();
    }

    public void Loop(string soundName, bool isLoopable) {
        Sound soundToLoop = Array.Find(sounds, sound => sound.name == soundName);

        // Establecer si se repite o no
        soundToLoop.source.loop = isLoopable;
    }

    public void SetMusicVolume(float volume, soundTypes type) {
        foreach (Sound s in sounds)
        {
            if(s.type == soundTypes.M) {
                s.volume = volume;
                s.source.volume = s.volume;
            }
        }
    }

    public void SetSoundVolume(float volume, soundTypes type) {
        foreach (Sound s in sounds)
        {
            if(s.type == soundTypes.S) {
                s.volume = volume;
                s.source.volume = s.volume;
            }
        }
    }

    public void SetVolume(string soundName, float volume) {
        Sound soundToUpdate = Array.Find(sounds, sound => sound.name == soundName);

        soundToUpdate.source.volume = volume;
    }

    public void MuteSounds(bool muteValue) {
        foreach (Sound s in sounds)
        {
            s.source.mute = !muteValue;
        }
    }
}

using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound targetedSound = Array.Find(sounds, sound => sound.name == name);
        if (targetedSound == null)
        {
            return;
        }

        targetedSound.source.Play();
    }

    public void Stop(string name)
    {
        Sound targetedSound = Array.Find(sounds, sound => sound.name == name);
        if (targetedSound == null)
        {
            return;
        }

        targetedSound.source.Stop();
    }
}

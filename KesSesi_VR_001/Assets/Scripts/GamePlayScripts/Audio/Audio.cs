using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class Audio{

    public AudioClip clip;

    public string name;

    [Range(0.0f,1.0f)]
    public float volume;

    [HideInInspector]
    public AudioSource audioSource;
}

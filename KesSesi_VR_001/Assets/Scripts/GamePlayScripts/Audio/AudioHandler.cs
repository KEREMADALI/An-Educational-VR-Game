using UnityEngine.Audio;
using UnityEngine;



public class AudioHandler : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float volume;
    public Audio[] sounds;

	void Awake () {
        // Every audio class becomes audio source
        foreach(Audio tmp in sounds){
            tmp.audioSource = gameObject.AddComponent<AudioSource>();

            tmp.audioSource.clip = tmp.clip;
        }
	}

    public void play(int index) {
        Audio tmp = sounds[index];

        if (tmp.audioSource != null) {
            tmp.audioSource.volume = volume;
            tmp.audioSource.Play();
        }  
        else
            Debug.Log("Audio source of the "+ index +" is null!");
    }
	
}

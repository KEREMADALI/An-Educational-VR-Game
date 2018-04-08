using UnityEngine.Audio;
using UnityEngine;



public class AudioHandler : MonoBehaviour {

    public static AudioHandler instance;

    [Range(0.0f, 1.0f)]
    public float volume;
    public Audio[] sounds;



	void Awake () {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Every audio class becomes audio source
        foreach (Audio tmp in sounds){
            tmp.audioSource = gameObject.AddComponent<AudioSource>();

            tmp.audioSource.clip = tmp.clip;
            tmp.audioSource.loop = tmp.loop;
        }
	}

    public void playOrStop(int index) {
        Audio tmp = sounds[index];

        if (tmp.audioSource != null) {
            // Stop if it is already playing
            if (tmp.audioSource.isPlaying) {
                tmp.audioSource.Stop();
                return;
            }

            if (tmp.volume == 0)
                tmp.audioSource.volume = volume;
            else
                tmp.audioSource.volume = tmp.volume;
            tmp.audioSource.Play();
        }  
        else
            Debug.Log("Audio source of the "+ index +" is null!");
    }
	
}

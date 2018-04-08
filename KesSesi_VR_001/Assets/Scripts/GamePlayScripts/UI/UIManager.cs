using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image timeBar;
    public GameObject wavingFace;

    public GameObject PlayerCanvas;
    public GameObject stars;
    public GameObject crossHair;

    public static int score;
    public float timer;

    public void startScene() {
        // Enable timeBar
        if (timeBar != null && timeBar.gameObject != null) {
            timeBar.gameObject.SetActive(true);
            timeBar.GetComponent<CountDown>().startGame();
        }

        // Enable stars
        if (stars != null)
            stars.SetActive(true);

        // Reset Score
        score = 0; 
    }

    public void endScene() {
        // Disable timeBar
        if(timeBar != null && timeBar.gameObject != null)
            timeBar.gameObject.SetActive(false);

        // Enable Waving Face
        if(wavingFace != null)
            wavingFace.SetActive(true);
    }
}

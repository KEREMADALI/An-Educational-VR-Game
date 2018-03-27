using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image timeBar;
    public GameObject wavingFace;

    public GameObject PlayerCanvas;
    public GameObject crossHair;

    public static int score;
    public float timer;

    // Useless right now
    public void endScene() {
        // Disable timeBar
        if(timeBar != null && timeBar.gameObject != null)
            timeBar.gameObject.SetActive(false);

        // Disable GazeTimer
        if (PlayerCanvas != null)
            PlayerCanvas.SetActive(false);

        // Disable CrossHair
        if (crossHair != null)
            crossHair.SetActive(false);

        // Enable Waving Face
        if(wavingFace != null)
            wavingFace.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeImageHandler : MonoBehaviour {

    public Transform progressBar;

    public void Start()
    {
        progressBar.GetComponent<Image>().fillAmount = KillTimer.timer;
    }

    void Update () {
        progressBar.GetComponent<Image>().fillAmount = KillTimer.timer;
    }

    void OnDestroy() {
        KillTimer.timer = 0.0f;
    }
}

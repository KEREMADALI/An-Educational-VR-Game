using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeImageHandler : MonoBehaviour {

    public Transform progressBar;
    
    public void Start(){
        progressBar.GetComponent<Image>().fillAmount = 0.0f;
    }


    public void updateProgressBar(float timer, float overTime) {
        progressBar.GetComponent<Image>().fillAmount = timer/overTime;
    }
    
}

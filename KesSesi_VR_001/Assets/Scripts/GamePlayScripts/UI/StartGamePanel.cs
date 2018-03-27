using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGamePanel : MonoBehaviour {

    private float timer;
    private float timerLimit = 2.0f;
    private GazeImageHandler progressBarHandlerScript;

    // true: Start, false: Back
    public bool startOrBack;
    public GameObject UICanvas;

    // Use this for initialization
    void Start () {
        timer = 0.0f;

        GameObject timerCircle = GameObject.Find("TimerCircle");
        if (timerCircle != null)
            progressBarHandlerScript = timerCircle.GetComponent<GazeImageHandler>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(progressBarHandlerScript != null)
            progressBarHandlerScript.updateProgressBar(timer, timerLimit);

        if (timer > timerLimit){
            if (startOrBack)
            {
                /*Start Game*/
                UICanvas.SetActive(true);
                gameObject.SetActive(false);
            }
            else {
                /*Back to the menu*/
            }
        }
    }

    public void resetTimer(){
        timer = 0f;
        if (progressBarHandlerScript != null)
            progressBarHandlerScript.updateProgressBar(timer, timerLimit);
        else
            Debug.Log("ProgressBar is null");
    }

    public void changeTriggerMode(bool _input) {
        startOrBack = _input;
    }
}

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
    public ResultHandler resultHandlerScript;

    private void Start () {
        timer = 0.0f;

        GameObject timerCircle = GameObject.Find("TimerCircle");
        if (timerCircle != null)
            progressBarHandlerScript = timerCircle.GetComponent<GazeImageHandler>();


    }
	
	private void Update () {
        timer += Time.deltaTime;
        if(progressBarHandlerScript != null)
            progressBarHandlerScript.updateProgressBar(timer, timerLimit);

        if (timer > timerLimit){
            FindObjectOfType<AudioHandler>().playOrStop(33);
            resultHandlerScript.uploadResults();
            if (startOrBack){
                startGame();
            }
            else {
                returnBackToMainMenu(); 
            }
        }
    }

    private void startGame() {
        resetTimer();
        UICanvas.SetActive(true);
        UIManager uiManagerScript = UICanvas.GetComponent<UIManager>();
        if (uiManagerScript != null)
            uiManagerScript.startScene();

        gameObject.SetActive(false);
    }

    private void returnBackToMainMenu() {
        resetTimer();
        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null)
        {
            Debug.Log("StartGamePanel_returnBackToMainMenu_MenuHandler object is null!");
            return;
        }

        MenuHandler menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null)
        {
            Debug.Log("StartGamePanel_returnBackToMainMenu_MenuHandler script is null!");
            return;
        }

        UICanvas.SetActive(true);
        menuHandlerScript.loadScene();
        gameObject.SetActive(false);
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

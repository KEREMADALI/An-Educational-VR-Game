using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTimer : MonoBehaviour {

    #region PublicVariables

    public bool killable = true;
    public int targetIndex;
    //public static float timer = 0f;
    public  float timer = 0f;

    #endregion

    #region PrivateVariables

    private float timerLimit = 0.8f;
    private ResultHandler resultHandlerScript;
    private GazeImageHandler progressBarHandlerScript;

    #endregion

    #region Public Functions

    void Start() {
        GameObject resultHandlerObject = GameObject.Find("ResultHandler");
        if (resultHandlerObject == null)
        {
            Debug.Log("KillTimer_Start_ResultHandler object is null!_KillTimer");
            return;
        }

        resultHandlerScript = resultHandlerObject.GetComponent<ResultHandler>();
        if (resultHandlerScript == null)
        {
            Debug.Log("KillTimer_Start_ResultHandler script is null!");
            return;
        }

        GameObject timerCircle = GameObject.Find("TimerCircle");
        if (timerCircle != null)
            progressBarHandlerScript = timerCircle.GetComponent<GazeImageHandler>();
    }

    public void Update() {

        timer += Time.deltaTime;
        if(progressBarHandlerScript != null)
            progressBarHandlerScript.updateProgressBar(timer, timerLimit);

        if (timer >= timerLimit){
            // Might be useless due to it is also called with the pointerexit trigger should be checked
            resetTimer();
            // This letter is dead recalculate the point
            informCalculatorAndUpdateResult();
            // Explode the letter
            sliceLetter();     
        }
    }


    // Zero outs the timer and that zero outs the progress bar
    public void resetTimer() {
        timer = 0f;
        if (progressBarHandlerScript != null)
            progressBarHandlerScript.updateProgressBar(timer, timerLimit);
        else
            Debug.Log("ProgressBar is null");
    }

    #endregion

    #region Private Functions


    // Sets the letter's variables as it is dead and calls the explosition 
    private void sliceLetter() {
        GetComponent<Rigidbody>().detectCollisions = false;
        killable = false;
        if (GetComponent<DetonationController>() != null){
            GetComponent<DetonationController>().explode();
        }
    }


    //Calculates the points
    private void informCalculatorAndUpdateResult(){
        if (killable){
            // Add points to the stars
            UIManager.score = UIManager.score + 2;
            FindObjectOfType<AudioHandler>().playOrStop(32);
        }
        else{
            // Substract points from the stars
            if(UIManager.score > 0)
                UIManager.score = UIManager.score - 2;
        }
        // Update hit details
        resultHandlerScript.updateResultsList(targetIndex, killable);
    }

    #endregion


}

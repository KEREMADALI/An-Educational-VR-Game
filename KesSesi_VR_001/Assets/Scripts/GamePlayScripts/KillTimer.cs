using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTimer : MonoBehaviour {

    #region PublicVariables

    public bool killable = true;
    public static float timer = 0f;

    public Material deadBlue;

    #endregion

    #region PrivateVariables

    private float timerLimit = 1f;
    
    #endregion

    #region Public Functions

    public void Update() { 

        timer += Time.deltaTime;

        if (timer >= timerLimit){
            Debug.Log("Called");
            resetTimer();
            informCalculator();
            sliceLetter();     
        }
    }

    // Zero outs the timer and that zero outs the progress bar
    public void resetTimer() {
        timer = 0f;
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
    private void informCalculator()
    {
        if (killable)
        {
            // Puan ekle
            ScoreCalculator.score = ScoreCalculator.score + 2;

        }
        else
        {
            //Puan çıkar
            ScoreCalculator.score = ScoreCalculator.score - 2;
        }
    }

    #endregion


}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillTimer : MonoBehaviour {

    #region PublicVariables

    public bool killable = true;
    public int targetIndex;
    public static float timer = 0f;

    #endregion

    #region PrivateVariables

    private float timerLimit = 1f;
    private MenuHandler menuHandlerScript;

    #endregion

    #region Public Functions

    void Start() {
        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null)
        {
            Debug.Log("KillTimer_Start_MenuHandler object is null!_KillTimer");
            return;
        }

        menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null)
        {
            Debug.Log("KillTimer_Start_MenuHandler script is null!");
            return;
        }
    }

    public void Update() { 

        timer += Time.deltaTime;

        if (timer >= timerLimit){;
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
            // Add points to the stars
            UIManager.score = UIManager.score + 2;
            // Record hit details
            menuHandlerScript.gameResults[targetIndex].hit();
        }
        else
        {
            // Substract points from the stars
            if(UIManager.score > 0)
            UIManager.score = UIManager.score - 2;
            // Record hit details
            menuHandlerScript.gameResults[targetIndex].miss();
        }
    }
    #endregion


}

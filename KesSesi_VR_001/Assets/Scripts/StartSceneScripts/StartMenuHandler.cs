using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class StartMenuHandler : MonoBehaviour {

    private MenuHandler menuHandlerScript;

    public Button playButton;

    private int awakeNum = 0;


    private void Awake() {
           // Disabling VR mode transferred to MenuHandler

        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null) {
            Debug.Log("MenuHandler object is null play button trigger can not be added.");
            return;
        }

        menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null)
        {
            Debug.Log("MenuHandler script is null play button trigger can not be added.");
            return;
        }

        playButton.onClick.AddListener(menuHandlerScript.loadScene);
    }

    private void OnEnable() {
        if (awakeNum == 0)
            awakeNum++;
        else
            menuHandlerScript.save();
    }

}

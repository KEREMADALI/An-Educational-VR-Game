using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class StartMenuHandler : MonoBehaviour {

    public Button playButton;

    void Awake() {
        XRSettings.enabled = false;

        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null) {
            Debug.Log("MenuHandler object is null play button trigger can not be added.");
            return;
        }

        MenuHandler menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null)
        {
            Debug.Log("MenuHandler script is null play button trigger can not be added.");
            return;
        }

        playButton.onClick.AddListener(menuHandlerScript.loadScene);
    }
}

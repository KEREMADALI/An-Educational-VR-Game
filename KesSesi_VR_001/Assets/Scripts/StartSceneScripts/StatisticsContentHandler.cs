using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsContentHandler : MonoBehaviour {

    private MenuHandler menuHandlerScript;

    private void Awake() {
        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null) {
            Debug.Log("StatisticsContentHandler_start_MenuHandler object is null!");
            return;
        }
        Debug.Log("StatisticsContentHandler_start_MenuHandler object is assigned");
        menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();

        if (menuHandlerScript == null) {
            Debug.Log("StatisticsContentHandler_start_MenuHandler script is null!");
            return;
        }
        Debug.Log("StatisticsContentHandler_start_MenuHandler script is assigned");

        updatePanels();
    }

    private void updatePanels() {
        int index = 0;
        int success = 0;
        int fail = 0;
        string percentage = "-";

        foreach (Transform child in transform){
            if (menuHandlerScript.finalGameResults.Length == transform.childCount)
            {
                success = menuHandlerScript.finalGameResults[index].getSuccessCount();
                fail = menuHandlerScript.finalGameResults[index].getFailCount();

                if ((success + fail) != 0)
                    percentage = "%" + (success * 100 / (success + fail)).ToString();
                else
                    percentage = "-";
            }
            else {
                success = 0;
                fail = 0;
                percentage = "-";
            }

            child.GetChild(1).GetComponent<Text>().text = success.ToString();
            child.GetChild(2).GetComponent<Text>().text = fail.ToString();
            child.GetChild(3).GetComponent<Text>().text = percentage;

            index++;
        }
    }

    private void OnEnable() {
        updatePanels();
    }

    public void reset() {
        int index = 0;

        foreach (Transform child in transform){
            if (menuHandlerScript.finalGameResults.Length == transform.childCount)
                menuHandlerScript.finalGameResults[index].reset();

            child.GetChild(1).GetComponent<Text>().text = "0";
            child.GetChild(2).GetComponent<Text>().text = "0";
            child.GetChild(3).GetComponent<Text>().text = "-";

            index++;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenuHandler : MonoBehaviour {

    private Color disabledColor = new Color(1f, 1f, 1f, 100f / 255f);
    private Color enabledColor = new Color(52f / 255f, 109f / 255f, 25f / 255f, 1f);
    private GameObject handler;
    private MenuHandler menuHandlerScript;

    // LetterGroup Panel objects for listener set up. Initiated on Unity API
    public GameObject[] letterGroupPanels;


    private void Start()
    {
        // Find MenuHandler object for further use
        handler = GameObject.Find("MenuHandler");
        // Inform if can not be found
        if (handler == null)
            Debug.Log("Menu Handler object is null, settings will not be updated!");

        // Take the menuHandler script
        menuHandlerScript = handler.GetComponent<MenuHandler>();
        // Inform if can not be found
        if (menuHandlerScript == null)
        {
            Debug.Log("MenuHandler script is null, random toggle trigger can not be added.");
            return;
        }

        // Will update the on screen values on opening 
        updateFirst();

        // Set Group variable toggles and exit the function 
        if (transform.name.Contains("Grup")) {
            foreach (GameObject obj in letterGroupPanels) {
                // Add Listener
                obj.GetComponent<Toggle>().onValueChanged.AddListener(delegate { menuHandlerScript.updateSettings(obj.transform.name); });
            }
            return;
        }

        // Add listener to buttons on the screem
        for(int index = 0; index<transform.childCount;index++)
        {
            Transform child = transform.GetChild(index);

            // Add event trigger to every child object except the headers
            if (!child.name.Contains("Txt"))
            {
                EventTrigger trig = child.gameObject.AddComponent<EventTrigger>();
                // Create an event entry
                EventTrigger.Entry entry = new EventTrigger.Entry();
                // Specify the entry type
                entry.eventID = EventTriggerType.PointerClick;
                // Sİze buttons works different then other buttons
                if (transform.name == "Pnl_Boyut")
                    entry.callback.AddListener((eventData) => { highlightPanel(child, enabledColor); });
                else
                    entry.callback.AddListener((eventData) => { highlightPanelAndDisableOthers(child); });

                trig.triggers.Add(entry);
            }

        }


    }

    public void highlightPanelAndDisableOthers(Transform trans)
    {
        // Take image variable
        Image img = trans.GetComponent<Image>();
        if (img.color == enabledColor)
            return;
        // First: color every button as disabled except header
        foreach (Transform tr in transform)
        {
            if (!tr.name.Contains("Txt"))
            {
                highlightPanel(tr, disabledColor);
            }
        }
        // Second: color desired button enabled
        highlightPanel(trans, enabledColor);
    }

    public void highlightPanel(Transform trans, Color _color)
    {
        // Take image variable
        Image img = trans.GetComponent<Image>();
        // If the object is selected and user clicks it again deselects it
        if (img != null)
        {
            if (img.color == _color)
                _color = disabledColor;

            img.color = _color;
            // Size button value changes with every click
            if (trans.name.Contains("Boyut") || _color == enabledColor)
                updateMenuHandler(trans);
        }
        else
        {
            Debug.Log("Error: Panel doesn't have an image!");
        }
    }

    public void updateMenuHandler(Transform trans)
    {
        if (menuHandlerScript == null)
        {
            Debug.Log("MenuHandler script is null.");
            return;
        }
        // Update real game variables
        menuHandlerScript.updateSettings(trans.name);
    }

    public void updateFirst() {

        if (menuHandlerScript == null)
        {
            Debug.Log("Menu Handler script is null, screen can not be updated!");
            return;
        }


        string name = transform.name.Substring(4); ;

        switch (name) {
            case "Zorluk": {
                    int index = menuHandlerScript.difficulty;
                    highlightPanelAndDisableOthers(transform.GetChild(index+1));
                }
                break;
            case "Hiz": {
                    int index = (int) menuHandlerScript.speed;
                    highlightPanelAndDisableOthers(transform.GetChild(index));
                }
                break;
            case "Sure": {
                    int index = menuHandlerScript.time;
                    highlightPanelAndDisableOthers(transform.GetChild(index + 1));
                }
                break;
            case "Boyut": {
                    bool[] letterSizes = menuHandlerScript.letterSizes;
                    for (int i = 0; i < letterSizes.Length; i++){
                        if (letterSizes[i])
                        {
                            Image img = transform.GetChild(i + 1).GetComponent<Image>();
                            if (img != null && !img.color.ToString().Equals(enabledColor.ToString()))
                                img.color = enabledColor;
                        }
                        else{
                            Image img = transform.GetChild(i + 1).GetComponent<Image>();
                            if (img != null && !img.color.ToString().Equals(disabledColor.ToString()))
                                img.color = disabledColor;  
                        }
                    }
                }
                    break;
            case "Ses": {
                    int index = menuHandlerScript.voice;
                    highlightPanelAndDisableOthers(transform.GetChild(index + 1));
                }
                break;
            case "Rastgele": {
                    if (transform.GetChild(1).GetComponent<Toggle>() != null)
                        if (menuHandlerScript.isRandom)
                            transform.GetChild(1).GetComponent<Toggle>().isOn = true;
                        else
                            transform.GetChild(1).GetComponent<Toggle>().isOn = false;
                    else
                        Debug.Log("Toggle component is missing! Random toggle can not be updated!");
                }
                break;
            case "Grup": {
                    bool[] letterGroups = menuHandlerScript.activeLetterGroups;

                    for(int index = 0; index < letterGroups.Length ; index++) {
                        if (letterGroups[index])
                            letterGroupPanels[index].GetComponent<Toggle>().isOn = true;
                        else
                            letterGroupPanels[index].GetComponent<Toggle>().isOn = false;
                    }
                }
                break;
        }


    }
}

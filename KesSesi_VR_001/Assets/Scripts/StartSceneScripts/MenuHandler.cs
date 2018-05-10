using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MenuHandler : MonoBehaviour {

    public static MenuHandler instance;

    // Looks like not being used
    //public GameObject audioHandler;

    public static bool isopen;

    // 0:Easy, 1:Medium, 2:Hard
    public int difficulty;

    // 1,2,3
    public float speed = 1.0f;

    // 0:1mn, 1:2mn, 2:3mn, 3:4min, 4:5min
    public int time;

    // 0:LowerCase, 1:UpperCase
    public bool[] letterSizes;
    // 0:Woman, 1:Man
    public int voice;

    public Result[] finalGameResults;

    /*
        0 = Group E, L, A, T
        1 = Group İ, N, O, R
        2 = Group M, U, K, I
        3 = Group Y, S, D, Ö
        4 = Group B, Ü, Ş, Z
        5 = Group Ç, G, C, P
        6 = Group H, Ğ, V, F, J
    */
    public bool[] activeLetterGroups = new bool[7];

    // Will be deleted
    public bool isRandom { get; internal set; }

    private void Awake(){

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        FindObjectOfType<AudioHandler>().playOrStop(30);

        finalGameResults = new Result[29];
        // Create the result array where score will be recorded.
        for (int index = 0; index < finalGameResults.Length; index++)
        {
            finalGameResults[index] = new Result(index);
        }

        load();
    }

    private IEnumerator startGame()
    {
        yield return changeVRSetting(true);
        FindObjectOfType<AudioHandler>().playOrStop(30);
        FindObjectOfType<AudioHandler>().playOrStop(33);
        SceneManager.LoadScene(1);
    }

    private IEnumerator endGame()
    {

        Debug.Log("MenuHandler_endGame_End Request");
        // Find UI canvas to disable it
        GameObject uiCanvas = GameObject.Find("UICanvas");

        if (uiCanvas == null)
        {
            Debug.Log("MenuHandler_endGame_UICAnvas is null!");
            yield return null;
        }
        // Take UIHandler script
        UIManager uiManagerScript = uiCanvas.GetComponent<UIManager>();

        if (uiManagerScript == null)
        {
            Debug.Log("MenuHandler_endGame_UIManager script is null!");
            yield return null;
        }
        // Ring the bells
        FindObjectOfType<AudioHandler>().playOrStop(31);

        // Wait for the final letters to fall
        yield return new WaitForSeconds(1.0f);
        // Disable UI elements
        uiManagerScript.endScene();

        yield return new WaitForSeconds(8.0f);
        // Load start menu
        FindObjectOfType<AudioHandler>().playOrStop(31);
        FindObjectOfType<AudioHandler>().playOrStop(30);
        yield return changeVRSetting(false);
        SceneManager.LoadScene(0);
    }

    private IEnumerator changeVRSetting(bool _input)
    {

        if (_input)
        {
            XRSettings.LoadDeviceByName("cardboard");
            yield return null;
            XRSettings.enabled = true;
        }

        else
        {
            XRSettings.LoadDeviceByName("");

            yield return null;

            XRSettings.enabled = false;

        }

    }

    public void loadScene() {
        

        if (1 == SceneManager.GetActiveScene().buildIndex){
            // Open start menu
            StartCoroutine("endGame");  
        }
        else if (0 == SceneManager.GetActiveScene().buildIndex) {
            /*
            // Reset the previous result array for the next game
            for (int index = 0; index < finalGameResults.Length; index++)
                finalGameResults[index].reset();
                */
            // Open game
            StartCoroutine("startGame");
        }
    }

    public void updateSettings(string variableName) {
        // Category names are like 'Zorluk_0'
        string category = variableName.Substring(0, variableName.Length - 2);
        int index = System.Int32.Parse( variableName.Substring(variableName.Length-1));

        switch (category) {
            case "Zorluk": {
                    Debug.Log("Difficulty is updated from " + difficulty + "to " + index);
                    difficulty = index;
                }
                break;
            case "Hiz": {
                    Debug.Log("Speed is updated from " + speed + "to " + index);
                    speed = index;
                }
                break;
            case "Sure": {
                    Debug.Log("Time is updated from " + (time+1) + " mn to " + (index+1) +" mn");
                    time = index;
                }
                break;
            case "Boyut": {
                    letterSizes[index] = !letterSizes[index];

                    if (letterSizes[0] && letterSizes[1])
                        Debug.Log("Size is updated to 'Both'.");
                    else if (letterSizes[0])
                        Debug.Log("Size is updated to 'LowerCase'");
                    else if (letterSizes[1])
                        Debug.Log("Size is updated to 'UpperCase'");
                }
                break;
            case "Ses": {
                    if (index == 0) {
                        Debug.Log("Voice is updated from 'Man' to 'Woman'");
                        voice = 0;
                    }
                        
                    else{
                        Debug.Log("Voice is updated from 'Woman' to 'Man'");
                        voice = 1;
                    }                 
                }
                break;
            case "Grup": {
                    activeLetterGroups[index] = !activeLetterGroups[index];
                    Debug.Log("Group " + index + " is now " + activeLetterGroups[index]);
                }
                break;
        }

    }

    public void save(){
        SaveAndLoad.save(this);
    }

    private void load()
    {
        SaveFormatData loadedData = SaveAndLoad.load();

        if (loadedData == null) {
            return;
        }
        difficulty = (int)loadedData.settings[0];
        speed = loadedData.settings[1];
        time = (int)loadedData.settings[2];
        voice = (int)loadedData.settings[3];

        letterSizes = loadedData.letterSizes;
        activeLetterGroups = loadedData.activeLetterGroups;

        finalGameResults = loadedData.scores;
    }

    public void updateGameResults(Dictionary<int, Result> gameResults){
        foreach (KeyValuePair<int, Result> temp in gameResults){
            // Take the id of the letter
            int id = temp.Value.getId();

            for (int i = 0; i < temp.Value.getResultOrder().Count; i++) {
                bool value = temp.Value.getResultOrder()[i];
                finalGameResults[id].update(value);
            }
        }
    }


}

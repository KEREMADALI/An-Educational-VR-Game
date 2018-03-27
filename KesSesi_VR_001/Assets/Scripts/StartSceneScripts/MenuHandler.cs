using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MenuHandler : MonoBehaviour {

    public static MenuHandler instance;

    public GameObject audioHandler;

    public static bool isopen;

    // 0:Easy, 1:Medium, 2:Hard
    public int difficulty;

    // 1,2,3
    public float speed = 1.0f;
    // TODO
    public bool isRandom;
    // 0:1mn, 1:2mn, 2:3mn 
    public int time;

    // 0:LowerCase, 1:UpperCase
    public bool[] letterSizes;
    // 0:Woman, 1:Man
    public int voice;

    public Result[] gameResults =  new Result[29];

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

    void Awake(){

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        // Create the result array where score will be recorded.
        for (int index = 0; index < gameResults.Length; index++) {
            gameResults[index] = new Result();
        }
            
    }

    public void loadScene() {
        

        if (1 == SceneManager.GetActiveScene().buildIndex){
            // Open start menu
            StartCoroutine("endGame");  
        }
        else if (0 == SceneManager.GetActiveScene().buildIndex) {
            // Reset the previous result array for the next game
            for (int index = 0; index < gameResults.Length; index++)
                gameResults[index].reset();

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

    private IEnumerator startGame() {
        yield return changeVRSetting(true);
        SceneManager.LoadScene(1);
    }

    private IEnumerator endGame() {

        Debug.Log("MenuHandler_endGame_End Request");
        // Find UI canvas to disable it
        GameObject uiCanvas = GameObject.Find("UICanvas");

        if (uiCanvas == null) {
            Debug.Log("MenuHandler_endGame_UICAnvas is null!");
            yield return null;
        }
        // Take UIHandler script
        UIManager uiManagerScript = uiCanvas.GetComponent<UIManager>();

        if (uiManagerScript == null) {
            Debug.Log("MenuHandler_endGame_UIManager script is null!");
            yield return null;
        }
        // Ring the bells
        endBell();
        // Wait for the final letters to fall
        yield return new WaitForSeconds(2.0f);
        // Disable UI elements
        uiManagerScript.endScene();

        yield return new WaitForSeconds(10.0f);
        // Load start menu

        yield return changeVRSetting(false);
        SceneManager.LoadScene(0);
    }

    // TODO find ringing bell audio
    private void endBell() {


        audioHandler = GameObject.Find("AudioManager");
        
        if (audioHandler != null && audioHandler.GetComponent<AudioHandler>() == null)
        {
            Debug.Log("Audio Handler script is null!");
            return;
        }
        /*
        int endingSoundIndex = ?;
        audioHandler.GetComponent<AudioHandler>().play(endingSoundIndex);
        */
    }

    private IEnumerator changeVRSetting(bool _input) {

        if (_input) {
            XRSettings.LoadDeviceByName("cardboard");
            yield return null;
            XRSettings.enabled = true;
        }

        else {
            XRSettings.LoadDeviceByName("");

            yield return null;

            XRSettings.enabled = false;

        }

    }
}

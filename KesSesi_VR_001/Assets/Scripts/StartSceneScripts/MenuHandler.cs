using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MenuHandler : MonoBehaviour {

    public static MenuHandler instance;

    public static bool isopen;

    // 0:Easy, 1:Medium, 2:Hard
    public int difficulty;
    // TODO
    public bool isRandom;
    // 0:1mn, 1:2mn, 2:3mn 
    public int time;

    // 0:LowerCase, 1:UpperCase
    public bool[] letterSizes;
    // 0:Woman, 1:Man
    public int voice;

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

    public void loadScene() {

        if (1 == SceneManager.GetActiveScene().buildIndex)
        {
            // Open start menu
            SceneManager.LoadScene(0);
        }
        else if (0 == SceneManager.GetActiveScene().buildIndex) {
            // Open game
            SceneManager.LoadScene(1);
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
            case "Rastgele": {
                    isRandom = !isRandom;
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

}

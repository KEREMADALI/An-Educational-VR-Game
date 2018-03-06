using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LetterGroupHandler : MonoBehaviour {

    /*
     A:0,  B:1,  C:2,  Ç:3,  D:4,
     E:5,  F:6,  G:7,  Ğ:8,  H:9,
     I:10, İ:11, J:12, K:13, L:14,
     M:15, N:16, O:17, Ö:18, P:19,
     R:20, S:21, Ş:22, T:23, U:24,
     Ü:25, V:26, Y:27, Z:28      
         */

    public List<LetterGroup> groups = new List<LetterGroup>();

	void Awake () {
        // MenuHandler object from the start menu
        GameObject menuHandler = GameObject.Find("MenuHandler");
        if (menuHandler == null) {
            Debug.Log("MenuHandler object is null!");
            return;
        }
        // MenuHandler script of the MenuHandler object that contains the activegroup list
        MenuHandler menuHandlerScript = menuHandler.GetComponent<MenuHandler>();

        if (menuHandlerScript == null) {
            Debug.Log("MenuHandlerScript is null!");
            return;
        }

        // Group E, L, A, N
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[0], new int[] { 5, 14, 0, 16 }));
        // Group İ, T, O, B
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[1], new int[] { 11, 23, 17, 1 }));
        // Group R, U, K, I
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[2], new int[] { 20, 24, 13, 10 }));
        // Group Ü, S, M , Ö
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[3], new int[] { 25, 21, 15, 18 }));
        // Group D, Y, Ş, C
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[4], new int[] { 4, 27, 22, 2 }));
        // Group Ç, G, Z, P
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[5], new int[] { 3, 7, 28, 19 }));
        // Group H, Ğ, V, F, J
        groups.Add(new LetterGroup(menuHandlerScript.activeLetterGroups[6], new int[] { 9, 8, 26, 12 }));
	}

    // Returns activated group list for the other classes
    public List<LetterGroup> getActiveGroupList() {
        List<LetterGroup> activeGroups = new List<LetterGroup>();

        foreach (LetterGroup tempGroup in groups) {
            if (tempGroup.isGroupActive())
                activeGroups.Add(tempGroup);
        }

        Debug.Log("getActiveGroupList is called and the return value was: "+ activeGroups);
        return activeGroups;
    } 
}

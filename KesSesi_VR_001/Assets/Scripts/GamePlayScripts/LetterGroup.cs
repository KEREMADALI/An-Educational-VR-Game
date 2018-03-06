using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class LetterGroup
{

    // Is this group activated from the settings: konsoldan kontrol edebilmek için public yaptım
    public bool isActive;
    // Index numbers of the letters
    private int[] indexOfLetters;

    // Returns isActive
    public bool isGroupActive()
    {
        return isActive;
    }

    //Constructer
    public LetterGroup(bool _isActive, int[] _indexOfLetters)
    {
        isActive = _isActive;
        indexOfLetters = _indexOfLetters;
        Debug.Log("LetterGroup Constructed");
    }
    // Activates the group
    public void activate()
    {
        isActive = true;
    }
    // Deactivates the group
    public void deactivate()
    {
        isActive = false;
    }

    //Returns the index numbers of the letters
    public int[] getIndexOfLetters()
    {
        return indexOfLetters;
    }

    //Randomizes the int array for spawn purpose
    public int[] getRandomizedArray()
    {
        var rand = new System.Random();
        int[] randomizedArray = indexOfLetters.OrderBy(x => rand.Next()).ToArray();
        Debug.Log("getRAndomizedArray called. Return value was: " + randomizedArray);

        return randomizedArray;
    }
}


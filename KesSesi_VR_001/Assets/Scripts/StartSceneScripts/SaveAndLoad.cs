using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveAndLoad {

    public static void save(MenuHandler _menuHandler) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream saveFile = new FileStream(Application.persistentDataPath + "/saveFile_0", FileMode.Create);

        SaveFormatData saveData = new SaveFormatData(_menuHandler);

        bf.Serialize(saveFile,saveData);
        saveFile.Close();
    }

    public static SaveFormatData load() {
        string saveFileName = "/saveFile_0";

        if (File.Exists(Application.persistentDataPath + saveFileName)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream loadFile = new FileStream(Application.persistentDataPath + saveFileName, FileMode.Open);

            if (loadFile == null) {
                Debug.Log("SaveAndLoad_load_Opening file named " + saveFileName + " is failed!");
                return null;
            }

            SaveFormatData returnData = bf.Deserialize(loadFile) as SaveFormatData;

            loadFile.Close();

            return returnData;
        }
        Debug.Log("SaveAndLoad_load_SaveFile doesn't exists!");
        return null;
    }



}

[Serializable]
public class SaveFormatData
{

    public float[] settings;

    public bool[] letterSizes;

    public bool[] activeLetterGroups;

    public Result[] scores;

    public SaveFormatData(MenuHandler _menuHandler)
    {
        settings = new float[4];
        letterSizes = new bool[2];

        settings[0] = (float)_menuHandler.difficulty;
        settings[1] = _menuHandler.speed;
        settings[2] = (float)_menuHandler.time;
        settings[3] = _menuHandler.voice;

        letterSizes = _menuHandler.letterSizes;
        activeLetterGroups = _menuHandler.activeLetterGroups;

        scores = _menuHandler.finalGameResults;
    }
}
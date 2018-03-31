using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHandler : MonoBehaviour {

    private Dictionary<int, Result> gameResults = new Dictionary<int, Result>();
    private List<GameObject> resultWithCanvasList = new List<GameObject>();

    public Canvas letterResult;
    public GameObject letter;
    public Sprite tick;
    public Sprite cross;


    public void displayResults() {
        int lenghtOfResultList = gameResults.Count;
        GameObject headerObject;
        Canvas contentCanvas;

        foreach (KeyValuePair<int, Result> temp in gameResults) {
            int id = temp.Value.getId();
            if (letter != null) {
                // Create the header lettter
                headerObject = Instantiate(letter.transform.GetChild(id).gameObject);
                Destroy(headerObject.GetComponent<Rigidbody>());
            }
            else{
                Debug.Log("ResultHandler_displayRsults_letter is null!");
                return;
            }
            // Create the canvas for content
            contentCanvas = Instantiate(letterResult,headerObject.transform);
            Sprite tmpSprite = new Sprite();

            for (int i = 0; i < temp.Value.getResultOrder().Count && i < 8;i++){
                bool isTick = temp.Value.getResultOrder()[i];
                if (isTick){
                    /* Add tick to canvas*/
                    tmpSprite = tick;   
                }
                else{
                    /* Add cross to canvas*/
                    tmpSprite = cross;
                }
                Transform trans = contentCanvas.transform.GetChild(i + 1);
                if (trans == null){
                    Debug.Log(this.name + "_displayResults_" + i + 1 + ". child of the contentCanvas is null!");
                    return;
                }
                trans.GetComponent<Image>().enabled = true;
                trans.GetComponent<Image>().sprite = tmpSprite;
            }
            // Make the content canvas child of the header object
            //contentCanvas.enabled = false;
            //contentCanvas.transform.parent = headerObject.transform;
          

            resultWithCanvasList.Add(headerObject); 
        }
        spreadResultsAroundPlayer();
    }

    private void spreadResultsAroundPlayer(){
        float radius = 4.5f;
        float ang = 20.0f;


        for (int i = 0; i < resultWithCanvasList.Count ;i++) {
            Vector3 headerPos = new Vector3();
            headerPos.x = transform.position.x + radius * Mathf.Sin(i* ang * Mathf.Deg2Rad);
            headerPos.y = 4.0f;
            headerPos.z = transform.position.z + radius * Mathf.Cos(i * ang * Mathf.Deg2Rad);

            Vector3 contentPos = new Vector3(headerPos.x,headerPos.y-4.5f,headerPos.z);

            resultWithCanvasList[i].transform.position = headerPos;
            resultWithCanvasList[i].transform.GetChild(0).transform.position = contentPos;
            resultWithCanvasList[i].transform.LookAt(Camera.main.transform.position,Vector3.up);
            resultWithCanvasList[i].transform.GetChild(0).transform.LookAt(Camera.main.transform.position, Vector3.up);
        }
    }

    public void updateResultsList(int _id, bool isHit) {      
        if(!gameResults.ContainsKey(_id)){
            gameResults[_id] = new Result(_id);
        }

        gameResults[_id].update(isHit) ;
    }
}

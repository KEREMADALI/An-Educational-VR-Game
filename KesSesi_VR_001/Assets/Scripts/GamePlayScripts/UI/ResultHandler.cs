using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultHandler : MonoBehaviour {

    private Dictionary<int, Result> gameResults = new Dictionary<int, Result>();
    private bool areThereResults = false;

    public Canvas letterResult;
    public GameObject letter;
    public Sprite tick;
    public Sprite cross;
    public Sprite plus;

    public float defaultPosition;


    public void displayResults() {
        int lenghtOfResultList = gameResults.Count;
        // 3D header of the letter result
        GameObject headerObject;
        // Will include one letters result as content
        Transform contentCanvas;
        int index = 0;
        
        // Loop the result list
        foreach (KeyValuePair<int, Result> temp in gameResults) {
            // Take the id of the letter to find its 3D model
            int id = temp.Value.getId();
            // If the letter is null it means the models are lost
            if (letter != null) {
                // Create the header lettter 3D model
                headerObject = createAndPosition(id,index);
                // Destroy its rigidbody because of gravity issues
                Destroy(headerObject.GetComponent<Rigidbody>());
            }
            else{
                Debug.Log("ResultHandler_displayRsults_letter is null!");
                return;
            }
            // Create the canvas for content (if statement is for letters like İ,Ş,Ğ... letters that include child objects)
            if(headerObject.transform.GetChild(0).name =="Result Letter Canvas(Clone)")
                contentCanvas = headerObject.transform.GetChild(0);
            else
                contentCanvas = headerObject.transform.GetChild(1);

            // Sprite that will hold the check or the cross image
            Sprite tmpSprite = new Sprite();

            // Loop every result of the letters but the panel only takes 8 letters (TODO there might be an another way, will be considered)
            for (int i = 0; i < temp.Value.getResultOrder().Count && i < 8;i++){
                // Is it correct or false
                bool isTick = temp.Value.getResultOrder()[i];
                // Image object
                GameObject imgObj = new GameObject();
                if ( i == 7) {
                    /* Add plus to canvas*/
                    tmpSprite = plus;
                    imgObj.name = "Plus";
                }
                else if (isTick){
                    /* Add tick to canvas*/
                    tmpSprite = tick;
                    imgObj.name = "Check";   
                }
                else{
                    /* Add cross to canvas*/
                    tmpSprite = cross;
                    imgObj.name = "Cross";
                }
                // Add image component to object
                Image img = imgObj.AddComponent<Image>();
                // An error happened at the adding component process
                if (img == null){
                    Debug.Log(this.name + "_displayResults_" + i + 1 + ". child of the contentCanvas is null!");
                    return;
                }
                // Enable image
                img.enabled = true;
                // Assign sprite to object
                img.sprite = tmpSprite;

                // Assign parent canvas
                imgObj.transform.SetParent(contentCanvas.transform.GetChild(0).transform);
                // Scale Image
                imgObj.transform.localScale = new Vector3(0.2f,0.2f,1.0f);

                // Set anchor and pivot values
                img.rectTransform.anchorMax = new Vector2(0.5f,1.0f);
                img.rectTransform.anchorMin = new Vector2(0.5f,1.0f);
                img.rectTransform.pivot = new Vector2(0.5f,0.5f);



                if (i % 2 == 0){
                    // Left column position settings
                    img.transform.localPosition = new Vector2(10.0f, 30.0f-(i/2)*15.0f);
                }
                else {
                    // Right column position settings
                    img.transform.localPosition = new Vector2(-10.0f, 30.0f-(i-1)/2*15.0f);
                }

                img.transform.localRotation = new Quaternion(0.0f,180.0f,0.0f,1.0f);


                // Set rotation of the header image and content canvas, they should look at the player
                headerObject.transform.LookAt(Camera.main.transform.position,Vector3.up);
                contentCanvas.transform.LookAt(Camera.main.transform.position,Vector3.up);

            }
            // Increase index
            index++;
        }
    }

    // Will create and position header and its content canvas and return the header object
    private GameObject createAndPosition(int id, int index)
    {
        // Radius of the circle that results are located on
        float radius = 4.5f;
        // Angle between every result table
        float unitAng = 20.0f;
        // Results start right after go/nogo menu
        /*float*/ defaultPosition = -1 * ((float)gameResults.Count / 2) + 0.5f;//0.0f;//2.2f;

        // Set position of the header according to its index value
        Vector3 headerPos = new Vector3();
        headerPos.x = Camera.main.transform.position.x + radius * Mathf.Sin((index + defaultPosition) * unitAng * Mathf.Deg2Rad);
        headerPos.y = 4.5f;
        headerPos.z = Camera.main.transform.position.z + radius * Mathf.Cos((index + defaultPosition) * unitAng * Mathf.Deg2Rad);

        // Position the content canvas below header
        Vector3 contentPos = new Vector3(headerPos.x,headerPos.y-1.75f, headerPos.z);

        // Header object is the 3D model of the related letter
        GameObject returnObject = Instantiate(letter.transform.GetChild(id).gameObject,headerPos,Quaternion.identity,transform);
        // Canvas that will include content
        Canvas returnCanvas = Instantiate(letterResult);
        // Scale canvas 
        returnCanvas.transform.localScale = new Vector3(1.0f,1.0f,0.0f);
        // Header is the parent of the canvas
        returnCanvas.transform.parent = returnObject.transform;
        // Set Canvas position
        returnCanvas.transform.position = contentPos;
        // Rotate header because it is initiated backwards
        returnObject.transform.Rotate(0.0f, 180.0f, 0.0f);

        // return header object
        return returnObject;
    }

    // It is called to update the result list by Letter's KillTimerScript
    public void updateResultsList(int _id, bool isHit) {
        areThereResults = true;
        // If the letter is new in the result list create it      
        if(!gameResults.ContainsKey(_id)){
            gameResults[_id] = new Result(_id);
        }

        // Update it's value
        gameResults[_id].update(isHit) ;
    }

    // Upload results to MenuHandler, clear current result list for the next game and destroy displayed result objects
    public void uploadResults() {
        if (!areThereResults) {
            Debug.Log("There are no results to upload!");
            return;
        }

        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null) {
            Debug.Log(this.name + "uploadResults_MenuHandler object is null" );
            return;
        }
        MenuHandler menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null)
        {
            Debug.Log(this.name + "uploadResults_MenuHandler script is null");
            return;
        }

        // Update the results array at the menuhandler
        menuHandlerScript.updateGameResults(gameResults);
        Debug.Log("Results are uploaded.");
        // Clear results dictionary for the next use
        gameResults.Clear();
        
        // Destroy every child object
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {


    private bool isGameOn = false;
    private bool isGameFinished = false;
    private MenuHandler menuHandlerScript;


    public UIManager UIManagerScript;
    public float timer;
    // Time limit of the game
    [HideInInspector] 
    public float gameTime;
    // Time limit of the counter
    public float timeLimit;
    // Text field of the counter of the first screen
    public Text counterField;
    // Is here for to be enabled
    public GameObject letterSpawner;

    void Start () {
        timer = timeLimit;

        // Take the time limit for the game
        GameObject menuHandlerObject = GameObject.Find("MenuHandler");
        if (menuHandlerObject == null){
            Debug.Log("MenuHandler object is null! Clock update failed.");
            return;
        }

        menuHandlerScript = menuHandlerObject.GetComponent<MenuHandler>();
        if (menuHandlerScript == null) {
            Debug.Log("MenuHandler script is null! Clock update failed.");
            return;
        }
        // TODO Multiplies with 15 from debug purpose
        //60sec * Time Variable From Settings Menu
        gameTime = 20 * (menuHandlerScript.time + 1);

        // Initial timer value for the UI element
        UIManagerScript.timer = gameTime;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (!isGameOn)
            updateCounter();
        else if(!isGameFinished) {
            updateGameClock();
        }
    }

    void updateCounter() {
        counterField.text = ((int)timer).ToString("00");
        if (counterField.text == "00")
        {
            timer = gameTime;
            //gameObject.SetActive(false);
            GetComponent<Text>().enabled = false;
            letterSpawner.gameObject.SetActive(true);
            isGameOn = true;
        }
    }

    void updateGameClock() {
        // update Time bar
        UIManagerScript.timer = timer;
        // TODO Update the clock on the screen
        if (timer < 0.5f) {
            isGameFinished = true;
            // Stop letter spawner
            letterSpawner.gameObject.SetActive(false);
            // Load start menu
            menuHandlerScript.loadScene();
        }
    }

}

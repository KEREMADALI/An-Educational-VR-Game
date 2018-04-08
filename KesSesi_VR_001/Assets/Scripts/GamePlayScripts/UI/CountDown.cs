using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    private MenuHandler menuHandlerScript;
    private bool isGameFinished = false;

    public UIManager UIManagerScript;
    public float timer;
    // Time limit of the game
    [HideInInspector] 
    public float gameTime;
    // Is here for to be enabled
    public GameObject letterSpawner;
    public ResultHandler resultHandlerScript;
    public GameObject playDecisionPanel;

    private void Awake () {
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
        startGame();
    }
	
	// Update is called once per frame
	private void Update () {
        if (!isGameFinished) {
            timer -= Time.deltaTime;
            updateGameClock();
        }        
    }

    public void startGame() {
        isGameFinished = false;

        letterSpawner.gameObject.SetActive(true);

        // TODO Multiplies with 20 for debug purpose
        //60sec * Time Variable From Settings Menu
        gameTime = 20 * (menuHandlerScript.time + 1);
        timer = gameTime;

        // Initial timer value for the UI element
        UIManagerScript.timer = gameTime;
    }

    private void updateGameClock() {

        if (timer < 0.5f) {
            // Reset game time for the next game (For replay option)
            timer = gameTime;
            // Avoids multiple end requests
            isGameFinished = true;
            // Stop letter spawner
            letterSpawner.gameObject.SetActive(false);
            // Display results
            resultHandlerScript.displayResults();

            // Open go/nogo decision menu
            playDecisionPanel.gameObject.SetActive(true);
            // Disable its script it should be enabled with its own trigger
            playDecisionPanel.GetComponent<StartGamePanel>().enabled = false;
        }
        // Update Time bar
        UIManagerScript.timer = timer;
    }

}

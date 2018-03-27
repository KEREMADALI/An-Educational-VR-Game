using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour {

    private int currentScore = 0;

    public Sprite[] Stars;
    public Image Scoreboard;


    void Update () {

        currentScore = UIManager.score;

        int starIndex = 0;

        if(currentScore > 0)
            starIndex = currentScore / 2;

        Scoreboard.sprite = Stars[starIndex];
	}

    void OnDestroy() {
        UIManager.score = 0;
    }
}

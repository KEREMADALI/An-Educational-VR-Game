using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour {

    public static int score = 0;
    //public TextMesh displayText;

    public Image Scoreboard;
    public Sprite[] Stars; 
    
	
	void Update () {
        int starIndex = 0;

        if(score > 0)
            starIndex = score / 2;

        Scoreboard.sprite = Stars[starIndex];

       // displayText.text = score.ToString("000");
	}

    void OnDestroy() {
        score = 0;
    }
}

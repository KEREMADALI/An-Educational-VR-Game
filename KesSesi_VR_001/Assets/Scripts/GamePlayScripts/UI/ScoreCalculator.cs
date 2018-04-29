using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour {

    private int currentScore = 0;
    private float starLimit;
    private float starFillAmount;

    public Sprite[] Stars;
    public Image Scoreboard;

    public Image[] newStars;

    /*
        void Update () {

            currentScore = UIManager.score;

            int starIndex = 0;

            if(currentScore > 0)
                starIndex = currentScore / 2;

            Scoreboard.sprite = Stars[starIndex];
        }
    */

    void Start() {
        starLimit = 3;
        Scoreboard.fillAmount = 0.4f;
        currentScore = 0;
    }

    void Update() {
        if (currentScore > (UIManager.score / 2)) {
            currentScore = UIManager.score / 2;

            if (starFillAmount > 0.0f) {
                starFillAmount = starFillAmount - 1.0f / starLimit;
            }
            else if (starFillAmount <= 0.0f) {
                if (starLimit == 0)
                    return;
                starLimit--;
                Scoreboard.fillAmount = Scoreboard.fillAmount - 0.2f;
                starFillAmount = 1 - 1 / starLimit;
            }
        }
        else if (currentScore < (UIManager.score / 2)){
            currentScore = UIManager.score / 2;


            if (starFillAmount < 1.0f){ // Current star is loading
                starFillAmount = starFillAmount + 1.0f / starLimit;
            }
            else if (starFillAmount >= 1.0f) { // Current is full, new star gets created
                    if (starLimit == 7)
                        return;
                    starLimit++;
                    Scoreboard.fillAmount = Scoreboard.fillAmount + 0.2f;
                    starFillAmount = 1 / starLimit;
            }
        }

        if (newStars[(int)starLimit - 3] != null)
            newStars[(int)starLimit - 3].fillAmount = starFillAmount;
        else
            Debug.Log("Star Index out of bound!");
    }
    void OnDestroy() {
        UIManager.score = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {

    public float time;
    public float fullTime;
    private Image bar;

    public Color fullColor;
    public Color lowColor;
    public UIManager UIManagerSCript;




    void Start() {
        time = UIManagerSCript.timer;
        fullTime = time;
        bar = transform.GetComponent<Image>();
    }

	// Update is called once per frame
	void Update () {
        time = UIManagerSCript.timer;
        updateBar();
	}

    void updateBar() {
        bar.fillAmount = time / fullTime;

        bar.color = Color.Lerp(lowColor,fullColor,bar.fillAmount);
    }
}

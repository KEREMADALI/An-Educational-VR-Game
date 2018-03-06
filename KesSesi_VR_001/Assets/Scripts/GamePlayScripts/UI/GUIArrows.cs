using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIArrows : MonoBehaviour {

    public Text[] arrows;
    public Camera headCamera;

    public void Update()
    {
        float yAngleOfTheCamera;

        // Take the camera angle if the the camera exists
        if (headCamera != null)
            yAngleOfTheCamera = headCamera.transform.eulerAngles.y;
        else {
            Debug.Log("Head Camera is null!");
            return;
        }

        // Re-calculate the angle if it is below zero
        if (yAngleOfTheCamera > 180.0f)
            yAngleOfTheCamera = yAngleOfTheCamera - 360.0f;

        // A calculation for fade in ad out effect of the arrows
        if (yAngleOfTheCamera > 45.0f || yAngleOfTheCamera < -45.0f) {
            float calcAlpha = 2 * (Mathf.Abs(yAngleOfTheCamera) - 45.0f) + 75.0f;
            // For not to overflow
            if (calcAlpha > 255){
                calcAlpha = 255;
            }
            // Fade in every arrow
            foreach(Text txt in arrows) {
                Color tmp = txt.color;
                // Alpha value is between 0 and 1
                tmp.a = calcAlpha/255.0f;
                txt.color = tmp;
            }
        } // Fade out every arrow
        else if (yAngleOfTheCamera < 45.0f && yAngleOfTheCamera > -45.0f) {
            foreach(Text txt in arrows) {
                Color tmp = txt.color;
                tmp.a = 0.0f;
                txt.color = tmp;
            }
        }
    }
}

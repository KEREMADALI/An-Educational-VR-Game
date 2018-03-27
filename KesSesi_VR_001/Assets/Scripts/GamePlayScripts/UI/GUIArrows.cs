using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GUIArrows : MonoBehaviour {

    public Image[] arrows;
    public Camera headCamera;
    public float currentAlpha;

    private bool isFadingIn = true;
    private float fadeSpeed = 2.0f;

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

        // A calculation for fade in and out effect of the arrows
        if (yAngleOfTheCamera > 45.0f || yAngleOfTheCamera < -45.0f) {
            float calcAlpha = 2 * (Mathf.Abs(yAngleOfTheCamera) - 45.0f) + 75.0f;
            // For not to overflow
            if (calcAlpha > 255){
                calcAlpha = 255;
            }
            // Fade in/out calculations for alphe value
            if (currentAlpha < 75.0f)
            {
                currentAlpha = 75.0f;
                isFadingIn = true;
            }
            else if (currentAlpha < calcAlpha && isFadingIn) {
                currentAlpha = currentAlpha + fadeSpeed;
            }
            else{
               currentAlpha = currentAlpha - fadeSpeed;
                isFadingIn = false;
            }
    
            // Change alpha channel of all arrows
            foreach (Image img in arrows) {

                // Changing Alpha Value         
                Color tmp = img.color;
                // Alpha value is between 0 and 1
                tmp.a = currentAlpha / 255.0f;
                img.color = tmp;

                // Constant Alpha Value
                /*
                Color tmp = txt.color;
                // Alpha value is between 0 and 1
                tmp.a = calcAlpha/255.0f;
                txt.color = tmp;
                */
            }
        } // Hide every arrow
        else if (yAngleOfTheCamera < 45.0f && yAngleOfTheCamera > -45.0f) {
            currentAlpha = 0;
            foreach(Image img in arrows) {
                Color tmp = img.color;
                tmp.a = 0.0f;
                img.color = tmp;
            }
        }
    }

    
}

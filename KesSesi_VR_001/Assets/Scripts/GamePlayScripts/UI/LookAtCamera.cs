using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

    //private Camera camera;

	void Update () {

        // Rotates the score as it is always readible
        transform.LookAt(Camera.main.transform.position,transform.up);
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}

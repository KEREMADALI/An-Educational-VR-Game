using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRSetupForScore : MonoBehaviour {

    public Camera camera;

	void Update () {
        // Rotates the score as it is always readible
        transform.LookAt(camera.transform.position,transform.up);
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}

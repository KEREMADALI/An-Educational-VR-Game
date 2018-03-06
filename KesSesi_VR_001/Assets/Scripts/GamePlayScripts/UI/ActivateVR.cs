using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ActivateVR : MonoBehaviour {

	void Awake () {
        // Will enable VR mode
        XRSettings.enabled = true;
	}
	
}

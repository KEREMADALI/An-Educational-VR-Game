using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCrosshair : MonoBehaviour {

    //public Camera camera;


    private Vector3 originScale;

	// Use this for initialization
	void Start () {
        originScale = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float distance;

        if (Physics.Raycast(new Ray(Camera.main.transform.position, Camera.main.transform.rotation * Vector3.forward), out hit))
        {
            distance = hit.distance;
        }
        else {
            distance = Camera.main.farClipPlane * 0.95f;
        }

        transform.position = Camera.main.transform.position + Camera.main.transform.rotation * Vector3.forward * distance;
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0.0f,180.0f,0.0f);
        transform.localScale = originScale * distance;
	}
}

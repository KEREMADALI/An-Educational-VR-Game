﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRExit : MonoBehaviour {

    // VR activation moved to MenuHandler

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
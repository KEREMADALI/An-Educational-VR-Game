using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminateIfExists : MonoBehaviour {
	void Update () {
        if (this.transform.position.y < 0.11f)
        {
            this.GetComponent<KillTimer>().resetTimer();
            Destroy(gameObject);     
        }
    }
}

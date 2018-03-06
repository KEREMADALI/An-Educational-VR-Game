using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private ParticleSystem ps;
    private ParticleSystemRenderer psr;

    public GameObject a;

	// Use this for initialization
	void Start () {
        ps = gameObject.GetComponent<ParticleSystem>();
        psr = gameObject.GetComponent<ParticleSystemRenderer>();
        psr.mesh = a.GetComponent<MeshFilter>().mesh;
        StartCoroutine(bum());
	}
	
    IEnumerator bum() {
        yield return new WaitForSeconds(3.0f);
        GetComponent<MeshRenderer>().enabled = false;
        ps.Play();
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}

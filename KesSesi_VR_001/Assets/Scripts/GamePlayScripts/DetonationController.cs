using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetonationController : MonoBehaviour {

    #region Public Variables
    // Setted as the diying object
    public GameObject pref;
    #endregion

    #region Private Variables
    //Power of the explostion
    private float power = 20.0f;
    //Radius of the explostion
    private float radius = 5.0f;
    //Particles that will be effected from the explosion
    private GameObject[] colliders = new GameObject[20];
    private int particleNum = 20;
    private float explosionTime = 2.0f;
    private float particleScale = 0.3f;
    #endregion

    public void explode() {
        //Will create explosion particles around the object
        createParticles();
        //Will start and end the explosion
        StartCoroutine(detonation());
    }

    // Create explosion particles
    private void createParticles() {
        GameObject temp;
        // Create particles on by one
        for (int i=0;i< particleNum; i++) {
            // Create a random position around the center 
            Vector3 pos = UnityEngine.Random.onUnitSphere * 0.2f + transform.position;
            // Letter looks at explosion center
            Quaternion rot = Quaternion.FromToRotation(transform.position - pos, Vector3.forward);
            // Instantiate
            temp = Instantiate(pref.gameObject, pos, rot);
            // Make particles child of the object to destroy all of them at once
            temp.transform.parent = transform;
            // SCale particles as small versions of the original object
            temp.transform.localScale = new Vector3(
                temp.transform.localScale.x * particleScale, 
                temp.transform.localScale.y * particleScale, 
                temp.transform.localScale.z * particleScale);
            // Setting for the explosion effect
            Rigidbody rb = temp.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.mass = 3;
            // Add object to the colliders list
            colliders[i] = temp;
        }
    }

    //Create explosion effect
    private IEnumerator detonation() {
        // Explosion center is thr objects positon
        Vector3 explositionCenter = transform.position;
        //Add explosion force to particles
        foreach (GameObject hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(power,explositionCenter,radius,0.0f,ForceMode.Impulse);
        }
        // Make exploded object and it's children(if it exists) invisible
        GetComponent<MeshRenderer>().enabled = false;
        if (transform.childCount > 0)
            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
        // Disable rigidbodies of the particles to stop rayCast collisions  
        foreach (GameObject temp in colliders) {
            if(temp.GetComponent<Rigidbody>() != null)
                temp.GetComponent<Rigidbody>().detectCollisions = false;
        }
        // Wait for explosion effect
        yield return new WaitForSeconds(explosionTime);
        // Destroy everthing
        Destroy(gameObject); 
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroll : MonoBehaviour {

    public Transform cmr;
    public float speedCoefficient;
    Vector3 lastpos;


    void Start () {
        cmr = GameObject.Find("Main Camera").transform;
        lastpos = cmr.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position -= ((lastpos - cmr.position) * speedCoefficient);
        lastpos = cmr.position;
    }
}

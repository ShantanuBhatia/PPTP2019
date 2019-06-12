using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrollSideways : MonoBehaviour {

    public Transform cmr;
    public float speedCoefficient;
    Vector3 lastpos;


    void Start()
    {
        cmr = GameObject.Find("Main Camera").transform;
        lastpos = cmr.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position -= ((lastpos - cmr.position) * speedCoefficient);
        transform.position -= new Vector3(((lastpos.x - cmr.position.x) * speedCoefficient), 0, 0);
        lastpos = cmr.position;
    }
}

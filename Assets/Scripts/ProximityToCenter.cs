using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityToCenter : MonoBehaviour
{
	// GameObject leftperson;
	// public Transform target;
    Camera cam;
    public float centerDist;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
    	//if (visible){
        Vector3 screenPos = cam.WorldToScreenPoint(transform.position);
        centerDist = Mathf.Abs((Screen.width / 2) - screenPos.x);
        //Debug.Log("target is " + centerDist + " pixels from the center");
    //}

    }

    public float GetCenterDistance(){
    	return centerDist;
    }
}

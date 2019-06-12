using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
A Pacer will pace left and right between its starting point and an specified X coordinate
*/

public class Pacer : MonoBehaviour {

    public float startX, stopX, xSpeed;
    //private float xPos;
    private enum direction { L_TO_R, R_TO_L};
    private direction dir;
    // Use this for initialization
    void Start () {
        transform.localPosition = new Vector3(startX, transform.localPosition.y, transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
        //xPos = transform.position.x;
        if (dir == direction.L_TO_R)
        {
            transform.Translate(new Vector3(xSpeed * Time.deltaTime, 0f, 0f));
            if (transform.localPosition.x > stopX)
            {
                dir = direction.R_TO_L;
            }

        }
        else if (dir == direction.R_TO_L)
        {
            transform.Translate(new Vector3(-xSpeed * Time.deltaTime, 0f, 0f));
            if (transform.localPosition.x < startX)
            {
                dir = direction.L_TO_R;
            }
        }
        

    }
}

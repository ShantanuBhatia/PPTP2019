using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour {

    private bool hasKey;
    private bool animOver;
    public float xSpeed;
    public float keyX;
    public float exitX;
    public GameObject key;
    public GameObject manNoKey, manWithKey;
    public GameController gc;
	// Use this for initialization
	void Start () {
        hasKey = false;
        animOver = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!animOver)
        {
            if (!hasKey)
            {
                transform.Translate(xSpeed * Time.deltaTime, 0f, 0f);
                if (transform.position.x >= keyX)
                {
                    hasKey = true;
                    key.transform.parent = transform;

                }
            }
            if (hasKey)
            {
                transform.Translate(-xSpeed * Time.deltaTime, 0f, 0f);
                if(transform.position.x <= exitX)
                {
                    gc.ReleaseCameraMovement();
                    manNoKey.SetActive(false);
                    manWithKey.SetActive(true);
                    gameObject.SetActive(false);
                    gc.keyPickupComplete = true;
                }
            }
        }
        
	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

    public bool shouldMove;
    private bool reachedDoor;
    private bool animOver;
    public float xSpeed;
    public float keyHoleX;
    public GameController gc;

    // Use this for initialization
    void Start () {
        shouldMove = false;
        reachedDoor = false;
        animOver = false;
	}
	


    /*
     * Once triggered, start moving towards the door. When you get to the door keyhole, stop and get the GameController to open the door.
     */
	void Update () {
        if (shouldMove)
        {
            transform.Translate(xSpeed * Time.deltaTime, 0f, 0f);
            if (transform.position.x >= keyHoleX)
            {
                Debug.Log("Hello");
                shouldMove = false;
                gc.ReleaseCameraMovement();
                gc.UnlockDoor();
            }
        }
	}

    public void TriggerMove()
    {
        shouldMove = true;
    }
}

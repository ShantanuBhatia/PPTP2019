using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int screenDivisions; // the screen will be divided into an NxN grid with N=screenDivisions
    public float observeThreshold; // how many seconds you have to be watching something before it counts as observing it
    public bool canMoveCamera;
    public bool verticalScrollLock;
    public GameObject manNoKey, manWithKey, arm, tableKey, keyHole, closedDoor, openDoor;
    public Camera cam;
    private bool keyPickupTriggered;
    public bool keyPickupComplete;
    private bool doorUnlockTriggered;
    public bool doorUnlockComplete;
    // Use this for initialization
    void Start () {
        canMoveCamera = true;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.transparencySortMode = TransparencySortMode.Orthographic;
        arm.SetActive(false);
        manWithKey.SetActive(false);
        manNoKey.SetActive(true);
        keyPickupComplete = false;
        keyPickupTriggered = false;
        doorUnlockTriggered = false;
        doorUnlockComplete = false;
    }
	
	// Update is called once per frame
	void Update () {

        // For debugging
        if (Input.GetKeyDown(","))
        {
            BlockCameraMovement();
        }
        else if (Input.GetKeyDown(".")){
            ReleaseCameraMovement();
        }

        // Observe key, it gets picked up
        if(tableKey.GetComponent<VisibilityTracker>().beingObserved && !keyPickupTriggered)
        {
            keyPickupTriggered = true;
            TriggerArmAnim();
            BlockCameraMovement();
        }

        // Once key is picked up, watch out for the doorUnlock
        if(keyPickupComplete && keyHole.GetComponent<VisibilityTracker>().beingObserved && !doorUnlockTriggered)
        {
            doorUnlockTriggered = true;
            TriggerDoorUnlock();
            BlockCameraMovement();
        }
	}

    public bool CameraMovementAllowed()
    {
        return canMoveCamera;
    }

    public void BlockCameraMovement()
    {
        canMoveCamera = false;
    }

    public void ReleaseCameraMovement()
    {
        canMoveCamera = true;
    }

    public bool GetVerticalScrollLock()
    {
        return verticalScrollLock;
    }

    public void TriggerArmAnim()
    {
        arm.SetActive(true);
    }

    public void TriggerDoorUnlock()
    {
        manWithKey.GetComponent<UnlockDoor>().TriggerMove();
    }

    public void UnlockDoor()
    {
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
        keyHole.SetActive(false);
    }
}

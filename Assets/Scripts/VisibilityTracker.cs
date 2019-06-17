using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTracker : MonoBehaviour {
    public float currentScreenTime, observeTimer;
    public bool visible;
    public bool beingObserved;
    public CameraController camCon;
    public Camera cam;
    private int screenDivisions; // the screen will be divided into an NxN grid with N=screenDivisions
    private float observeThreshold; // how many seconds you have to be watching something before it counts as observing it
    public bool canBeSpotted; // set true if this character is going to regularly go in and out of vision - for example, someone pacing behind a window.
    private bool spottedFlag; // becomes true if the character was ever spotted
    private GameController gc;


	void Start () {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        screenDivisions = gc.screenDivisions;
        observeThreshold = gc.observeThreshold;
        visible = false;
        beingObserved = false;
        currentScreenTime = 0f;
        observeTimer = 0f;
        if (camCon == null || cam == null) {
            camCon = GameObject.Find("Main Camera").GetComponent<CameraController>();
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
	}

	void Update () {
        if (visible) {
            currentScreenTime += Time.deltaTime;
            if (Input.GetKeyDown("g")) {
                DescribeRelativePositions();
            }

            checkObserve();


            //// Movement mechanic 1: observing objects
            //Vector2 sector = getCurrentScreenSector();
            //if (sector.x.Equals(0) || sector.x.Equals(screenDivisions - 1) || sector.y.Equals(0) || sector.y.Equals(screenDivisions - 1) || !camCon.canObserve())
            //{

            //    observeTimer = 0f;
            //    if(beingObserved)
            //    {
            //        DescribeCurrentScreenSector();
            //        Debug.Log(transform.name + " is no longer being observed");
            //    }
            //    beingObserved = false;

            //}
            //else
            //{
            //    observeTimer += Time.deltaTime;
            //}

            //if (!beingObserved)
            //{
            //    if (observeTimer > observeThreshold && camCon.canObserve())
            //    {
            //        beingObserved = true;
            //        Debug.Log(transform.name + " is now being observed");
            //    }
            //}
            ////getCurrentScreenSector();
        }
	}

    void OnBecameInvisible() {
        Debug.Log(transform.name + " was on screen for " + currentScreenTime + " seconds.");
        visible = false;
        currentScreenTime = 0f;
        observeTimer = 0f;
        camCon.removeFromVisible(transform.name);
    }

    void OnBecameVisible() {
        visible = true;

        camCon.addToVisible(transform.name);
    }

    // Out of all the pixels on the screen, how many are taken up by this object?
    // TODO: implement
    float screenSizePercentage() {
        return 0f;
    }

    private void checkObserve()
    {
        // Movement mechanic 1: observing objects
        Vector2 sector = getCurrentScreenSector();
        if (sector.x.Equals(0) || sector.x.Equals(screenDivisions - 1) || sector.y.Equals(0) || sector.y.Equals(screenDivisions - 1) || !camCon.canObserve())
        {

            observeTimer = 0f;
            if (beingObserved)
            {
                DescribeCurrentScreenSector();
                Debug.Log(transform.name + " is no longer being observed");
            }
            beingObserved = false;

        }
        else
        {
            observeTimer += Time.deltaTime;
        }

        if (!beingObserved)
        {
            if (observeTimer > observeThreshold && camCon.canObserve())
            {
                beingObserved = true;
                canBeSpotted = false;
                Debug.Log(transform.name + " is now being observed");
            }
        }
        //getCurrentScreenSector();
    }

    public Vector2 getCurrentScreenSector() {
        Vector2 screenSector = new Vector2(Mathf.Floor(cam.WorldToScreenPoint(transform.position).x / (Screen.width / screenDivisions)), Mathf.Floor(cam.WorldToScreenPoint(transform.position).y / (Screen.height / screenDivisions)));
        return screenSector;
    }

    public void DescribeCurrentScreenSector()
    {
        Vector2 screenSector = getCurrentScreenSector();
        string posDescr = transform.name + " is in " + screenSector;
        //if (screenSector.y.Equals(0))
        //{
        //    posDescr += "Bottom ";
        //}
        //else if (screenSector.y.Equals(1))
        //{
        //    posDescr += "Middle ";
        //}
        //else if (screenSector.y.Equals(2))
        //{
        //    posDescr += "Top ";
        //}

        //if (screenSector.x.Equals(0))
        //{
        //    posDescr += "Left";
        //}
        //else if (screenSector.x.Equals(1))
        //{
        //    posDescr += "Middle";
        //}
        //else if (screenSector.x.Equals(2))
        //{
        //    posDescr += "Right";
        //}

        Debug.Log(posDescr);
    }

    public void DescribeRelativePositions() {
        List<GameObject> objectsOnScreen = camCon.getAllVisible();
        foreach(GameObject go in objectsOnScreen) {
            if (gameObject != go) {
                if(transform.position.y > go.transform.position.y) {
                    Debug.Log(transform.name + " is higher than " + go.transform.name);
                }
                else {
                    Debug.Log(go.transform.name + " is higher than " + transform.name);
                }
            }
        }
    }


    // A character is "spotted" if the player's view is a closeup of an area, and a spottable character enters and exits the players view ALL WITHIN THE PERIPHERY OF THEIR VISION
    // A character can only be spotted if they have not yet been observed - because a Spot is like spotting something out of the corner of your eye
    public void checkSpotted()
    {
        if (!canBeSpotted && spottedFlag)
        {
            //TODO
        }
    }
}

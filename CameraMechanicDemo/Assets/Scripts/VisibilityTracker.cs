using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTracker : MonoBehaviour {
    float currentScreenTime;
    bool visible;
    public CameraController camCon;
    public Camera cam;


	void Start () {
        visible = false;
        currentScreenTime = 0f;
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
            //getCurrentScreenSector();
        }
	}

    void OnBecameInvisible() {
        Debug.Log(transform.name + " was on screen for " + currentScreenTime + " seconds.");
        visible = false;
        currentScreenTime = 0f;
        camCon.removeFromVisible(transform.name);
    }

    void OnBecameVisible() {
        visible = true;
        camCon.addToVisible(transform.name);
    }

    // Out of all the pixels on the screen, how many are taken up by this object?
    float screenSizePercentage() {
        return 0f;
    }

    public Vector2 getCurrentScreenSector() {
        Vector2 screenSector = new Vector2(Mathf.Floor(cam.WorldToScreenPoint(transform.position).x / (Screen.width / 3)), Mathf.Floor(cam.WorldToScreenPoint(transform.position).y / (Screen.height / 3)));
        string posDescr = transform.name + " is in the ";
        if (screenSector.y.Equals(0)) {
            posDescr += "Bottom ";
        }
        else if (screenSector.y.Equals(1)) {
            posDescr += "Middle ";
        }
        else if (screenSector.y.Equals(2)) {
            posDescr += "Top ";
        }

        if (screenSector.x.Equals(0)) {
            posDescr += "Left";
        }
        else if (screenSector.x.Equals(1)) {
            posDescr += "Middle";
        }
        else if (screenSector.x.Equals(2)) {
            posDescr += "Right";
        }

        Debug.Log(posDescr);
        return screenSector;
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
}

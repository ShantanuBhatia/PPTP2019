using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // XY camera movement, plus a simple zoom in-zoom out
    public enum zoomLevels { CLOSEUP, MIDRANGE, WIDE } ;
    private zoomLevels zoomLevel;
    public float maxSpeed;
    public float acc;
    private List<GameObject> visibleObjects;
    private Camera cam;
	
	void Start () {
        visibleObjects = new List<GameObject>();
        cam = GetComponent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {
        float yTranslation = Mathf.Clamp(Input.GetAxis("Vertical"), -maxSpeed, maxSpeed);
        float xTranslation = Mathf.Clamp(Input.GetAxis("Horizontal"), -maxSpeed, maxSpeed);

        transform.Translate(xTranslation, yTranslation, 0);

        if (Input.GetKeyDown("f")) {
            foreach (GameObject ob in visibleObjects) {
                ob.GetComponent<VisibilityTracker>().DescribeCurrentScreenSector();
            }
        }
        else if (Input.GetKeyDown("1"))
        {
            zoomToLevel(zoomLevels.CLOSEUP);
        }
        else if (Input.GetKeyDown("2"))
        {
            zoomToLevel(zoomLevels.MIDRANGE);
        }
        else if (Input.GetKeyDown("3"))
        {
            zoomToLevel(zoomLevels.WIDE);
        }
    }

    public bool addToVisible(string newVisObjName) {
        GameObject g = GameObject.Find(newVisObjName);
        if (g) {
            visibleObjects.Add(g);
            Debug.Log("Can now also see " + newVisObjName);
            return true;
        }
        return false;
    }

    public bool removeFromVisible(string remObjName) {
        GameObject g = GameObject.Find(remObjName);
        if (g) {
            visibleObjects.Remove(g);
            Debug.Log("Object removed. Objects now visible: " + listOfVisible());
        }
        return false;
    }

    public string listOfVisible() {
        string visList = "";
        foreach (GameObject ob in visibleObjects) {
            visList += ob.name + " ";
        }
        return visList;
    }

    public List<GameObject> getAllVisible() {
        return visibleObjects;
    }

    private void zoomToLevel(zoomLevels targetZoomLvl)
    {
        zoomLevel = targetZoomLvl;
        if (targetZoomLvl == zoomLevels.CLOSEUP)
        {
            cam.orthographicSize = 8;
        }
        if(targetZoomLvl == zoomLevels.MIDRANGE)
        {
            cam.orthographicSize = 16;
        }
        if (targetZoomLvl == zoomLevels.WIDE)
        {
            cam.orthographicSize = 32;
        }

    }

    // As long as we're not at a wide zoom, observation is allowed.
    public bool canObserve()
    {
        return zoomLevel != zoomLevels.WIDE;
    }


}

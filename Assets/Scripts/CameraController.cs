using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // XY camera movement, plus a simple zoom in-zoom out
    public enum zoomLevels { CLOSEUP = 10, MIDRANGE = 10, WIDE = 10 } ;
    public float xMin, xMax, yMin, yMax;
    private zoomLevels zoomLevel;
    public float maxSpeed;
    public float acc;
    private List<GameObject> visibleObjects;
    private Camera cam;
	public GameController gc;
	private bool verticalScrollLock;

	void Start () {
		visibleObjects = new List<GameObject>();
        cam = GetComponent<Camera>();
        zoomToLevel(zoomLevels.MIDRANGE);
		gc = GameObject.Find("GameController").GetComponent<GameController>();
		verticalScrollLock = gc.GetVerticalScrollLock();

	}
	
	// Update is called once per frame
	void Update () {
		if (gc.CameraMovementAllowed())
		{
			float xTranslation = Mathf.Clamp(Input.GetAxis("Horizontal"), -maxSpeed, maxSpeed);
			float yTranslation = 0f;
			if (!verticalScrollLock || zoomLevel == zoomLevels.CLOSEUP)
			{
				yTranslation = Mathf.Clamp(Input.GetAxis("Vertical"), -maxSpeed, maxSpeed);
			}
            if(transform.position.x + xTranslation > xMin && transform.position.x + xTranslation < xMax && transform.position.y + yTranslation > yMin && transform.position.y + yTranslation < yMax)
			{
				transform.Translate(xTranslation, yTranslation, 0);
			}
			


			if (Input.GetKeyDown("1"))
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
		//if (targetZoomLvl == zoomLevels.CLOSEUP)
		//{
		//    cam.orthographicSize = zoomVals;
		//}
		//if(targetZoomLvl == zoomLevels.MIDRANGE)
		//{
		//    cam.orthographicSize = 16;
		//}
		//if (targetZoomLvl == zoomLevels.WIDE)
		//{
		//    cam.orthographicSize = 32;
		//}
		cam.transform.position = new Vector3(cam.transform.position.x, 0f, cam.transform.position.z);
        cam.orthographicSize = (int) targetZoomLvl;

    }

    // As long as we're not at a wide zoom, observation is allowed.
    public bool canObserve()
    {
		return true;
    }

    public bool canSpot()
    {
        return true;
    }



}

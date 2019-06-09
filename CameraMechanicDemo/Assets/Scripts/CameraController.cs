using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float maxSpeed;
    public float acc;
    private List<GameObject> visibleObjects;
	// Use this for initialization
	void Start () {
        visibleObjects = new List<GameObject>();    

    }
	
	// Update is called once per frame
	void Update () {
        float yTranslation = Mathf.Clamp(Input.GetAxis("Vertical"), -maxSpeed, maxSpeed);
        float xTranslation = Mathf.Clamp(Input.GetAxis("Horizontal"), -maxSpeed, maxSpeed);

        transform.Translate(xTranslation, yTranslation, 0);

        if (Input.GetKeyDown("f")) {
            foreach (GameObject ob in visibleObjects) {
                ob.GetComponent<VisibilityTracker>().getCurrentScreenSector();
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
}

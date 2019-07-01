using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttentionSeeking : MonoBehaviour {

    /*
     * Behaviour of someone who wants to be noticed and on their pedestal.
     * If the pedestal is on screen, they will move to be on their pedestal
     * If the pedestal is off screen, they will move to go to the center of the screen.
     */
    public float cameraFollowDelay;
    public float movementSpeed;
    public GameObject cameraObj;
    public Camera cam;
    public CameraController camCon;
    public Transform pedestal;
    public VisibilityTracker pedestalVizTrack;
    public float pedestalPosMargin;
    public float DistFromCenter;

    private bool shouldFollowCamera;
    private Vector2 currentScreenSector;
    private int screenDivisions;
    [SerializeField] private VisibilityTracker vizTrack;
    [SerializeField] private bool moving;
    [SerializeField] private float waitTimeElapsed;
    [SerializeField] private bool reachedDestination;
    [SerializeField] private GameController gc;

    void Start () {
        shouldFollowCamera = false;
        moving = false;
        waitTimeElapsed = 0f;
        cameraObj = GameObject.Find("Main Camera");
        cam = cameraObj.GetComponent<Camera>();
        camCon = cameraObj.GetComponent<CameraController>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        screenDivisions = gc.screenDivisions;
        vizTrack = gameObject.GetComponent<VisibilityTracker>();
        if (vizTrack == null)
        {
            Debug.LogError("BRO WHY NO VIZ TRACKER???");
        }
        pedestal = GameObject.Find("pedestal").transform;
        pedestalVizTrack = GameObject.Find("pedestal").GetComponent<VisibilityTracker>();
        if (pedestalVizTrack == null)
        {
            Debug.LogError("BRO WHY NO PEDESTAL VIZ TRACKER???");
        }
    }


void Update()
    {
        if (shouldFollowCamera)
        {
            if (reachedDestination)
            {
                waitTimeElapsed = 0f;
            }

            if (!vizTrack.checkVisible())
            {
                reachedDestination = false;
                waitTimeElapsed += Time.deltaTime;
            }
            else
            {
                if (pedestalVizTrack.checkVisible() && Mathf.Abs(transform.position.x - pedestal.position.x) > pedestalPosMargin)
                {
                    reachedDestination = false;
                    waitTimeElapsed += Time.deltaTime;
                }
                else
                {
                    currentScreenSector = vizTrack.getCurrentScreenSector();
                    if (currentScreenSector.x.Equals(0) || currentScreenSector.x.Equals(screenDivisions - 1) || currentScreenSector.y.Equals(0) || currentScreenSector.y.Equals(screenDivisions - 1))
                    {
                        reachedDestination = false;
                        waitTimeElapsed += Time.deltaTime;
                    }

                    //else if (currentScreenSector.x > 0 && currentScreenSector.x < (screenDivisions - 1) && currentScreenSector.y > (0) && currentScreenSector.y < (screenDivisions - 1))
                    else if (Mathf.Abs(transform.position.x - cam.transform.position.x) < DistFromCenter)
                    {
                        reachedDestination = true;

                    }
                }


            }



            if (!reachedDestination && waitTimeElapsed > cameraFollowDelay)
            {
                //if (pedestalVizTrack.checkVisible())
                //{
                //    if (pedestal.position.x > transform.position.x) { transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f); }
                //    else { transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f); }
                //}
                //else
                //{
                    if (cam.transform.position.x > transform.position.x) { transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f); }
                    else { transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f); }
                //}


            }
        }

	}

    public void StartFollowingCamera()
    {
        shouldFollowCamera = true;
    }
    public void StopFollowingCamera()
    {
        shouldFollowCamera = false;
    }
}

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
    public float pedestalPosMargin, talkingDistance;
    public float DistFromCenter;
    public string spriteHeadTag, townieTag;
    public enum Direction { LEFT, RIGHT };
    public Direction lookDirection;

    private GameObject stan;
    [SerializeField] private bool shouldFollowCamera;
    private Vector2 currentScreenSector;
    private int screenDivisions;
    private Animator anim;
    [SerializeField] private VisibilityTracker vizTrack;
    [SerializeField] private bool moving, townieOnScreen;
    [SerializeField] private float waitTimeElapsed;
    [SerializeField] private bool reachedDestination;
    [SerializeField] private GameController gc;

    void Start () {

        townieOnScreen = false;
        lookDirection = Direction.RIGHT;
        stan = transform.GetChild(0).gameObject;
        foreach (Transform child in stan.transform)
        {
            Debug.Log(child.name);
            if (child.tag == spriteHeadTag)
            {
                vizTrack = child.gameObject.GetComponent<VisibilityTracker>();
                break;
            }
        }


        shouldFollowCamera = false;
        moving = false;
        waitTimeElapsed = 0f;
        cameraObj = GameObject.Find("Main Camera");
        cam = cameraObj.GetComponent<Camera>();
        camCon = cameraObj.GetComponent<CameraController>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        screenDivisions = gc.screenDivisions;
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
                townieOnScreen = camCon.ObjectOnScreenWithTag(townieTag);
                currentScreenSector = vizTrack.getCurrentScreenSector();
                if (townieOnScreen && DistanceToNearestTownie() > talkingDistance)
                {
                    reachedDestination = false;
                    waitTimeElapsed += Time.deltaTime;
                }
                else if (currentScreenSector.x.Equals(0) || currentScreenSector.x.Equals(screenDivisions - 1) || currentScreenSector.y.Equals(0) || currentScreenSector.y.Equals(screenDivisions - 1))
                {
                    reachedDestination = false;
                    waitTimeElapsed += Time.deltaTime;
                }
                else if (Mathf.Abs(transform.position.x - cam.transform.position.x) < DistFromCenter)
                {
                    reachedDestination = true;
                }
            }

            if(!reachedDestination && camCon.ObjectOnScreenWithTag(townieTag))
            {
                GameObject nearestTownie = NearestOnScreenTownie();
                if (nearestTownie.transform.position.x > transform.position.x)
                {
                    transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
                    lookDirection = Direction.RIGHT;
                }
                else
                {
                    transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
                    lookDirection = Direction.LEFT;
                }
            }
            else if (!reachedDestination && waitTimeElapsed > cameraFollowDelay)
            {
    
                if (cam.transform.position.x > transform.position.x)
                {
                    transform.Translate(movementSpeed * Time.deltaTime, 0f, 0f);
                    lookDirection = Direction.RIGHT;
                }
                else
                {
                    transform.Translate(-movementSpeed * Time.deltaTime, 0f, 0f);
                    lookDirection = Direction.LEFT;
                }
            }
        }

	}

    private float DistanceToNearestTownie()
    {
        
        List<GameObject> onScreenTownies = camCon.GetAllOnScreenWithTag(townieTag);
        if (onScreenTownies.Count == 0)
        {
            return 0f;
        }
        float dist = Mathf.Abs(transform.position.x - onScreenTownies[0].transform.position.x);
        foreach (GameObject townie in onScreenTownies)
        {
            if (Mathf.Abs(transform.position.x - townie.transform.position.x) < dist)
            {
                dist = Mathf.Abs(transform.position.x - townie.transform.position.x);
            }
        }
        return dist;
    }

    private GameObject NearestOnScreenTownie()
    {
        
        List<GameObject> onScreenTownies = camCon.GetAllOnScreenWithTag(townieTag);
        if (onScreenTownies.Count == 0)
        {
            return null;
        }
        float dist = Mathf.Abs(transform.position.x - onScreenTownies[0].transform.position.x);
        GameObject g = onScreenTownies[0];
        foreach (GameObject townie in onScreenTownies)
        {
            if (Mathf.Abs(transform.position.x - townie.transform.position.x) < dist)
            {
                dist = Mathf.Abs(transform.position.x - townie.transform.position.x);
                g = townie;
            }
        }

        return g;
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

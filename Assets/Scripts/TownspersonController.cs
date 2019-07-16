using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownspersonController : MonoBehaviour
{


    
   
    [SerializeField] private State state;
    private Direction lookDirection;
    private Transform cam;
    private VisibilityTracker vizTrack;
    private GameController gc;
    private enum Visibility { VISIBLE, NOT_VISIBLE };
    private Animator anim;
    private Visibility myViz;
    private GameObject head;
    
    private float stanConversationCount;
    private Vector3 lookRightScale, lookLeftScale;
    private bool talkingToStan;

    public int edgeSectorCount;
    public enum State { SOLO_IGNORING_STAN, WILLING_TO_CONVERSE, TALKING_TO_STAN = 98, WALKING_TO_THEATER=99,  REACHED_THEATER=100};
    public enum Direction { LEFT, RIGHT };
    public GameObject stan;
    public GameObject gatheringPoint;
    public float conviction;
    public float convictionSecondsNeeded, convictionFallRateDamper;
    public float walkingSpeed, reachedTheaterMargin;
    public string spriteHeadTag;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera").transform;
        anim = GetComponent<Animator>();
        foreach(Transform child in transform)
        {
            if (child.tag == spriteHeadTag)
            {
                head = child.gameObject;
                vizTrack = head.GetComponent<VisibilityTracker>();
                break;
            }
        }
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        // TEMP
        state = State.SOLO_IGNORING_STAN;
        conviction = 0;
        Debug.Log("???");
        lookRightScale = transform.localScale;
        lookLeftScale = new Vector3(-lookRightScale.x, lookRightScale.y, lookRightScale.z);
    }


    void Start()
    {
        Debug.Log("Townsperson, activate!");
    }


    void Update()
    {
        SetVisiblity();
        SetTalkingState();
        OutputTalkingState();
        SetLookDirection();
        ChangeCharacterFacingDirection();
        HandleConviction();
        if (state == State.WALKING_TO_THEATER)
        {
            WalkToAmphi();
        }
        
        SetAnimationFlags();
    }

    private void SetLookDirection()
    {
        if (state == State.SOLO_IGNORING_STAN)
        {
            lookDirection = transform.position.x > stan.transform.position.x ? Direction.RIGHT : Direction.LEFT;
        }
        else if (state == State.WILLING_TO_CONVERSE)
        {
            lookDirection = cam.position.x > transform.position.x ? Direction.RIGHT : Direction.LEFT;
        }
    }


    private void SetVisiblity()
    {
        bool viz = vizTrack.visible;
        myViz = viz ? Visibility.VISIBLE : Visibility.NOT_VISIBLE;
    }


    private void SetTalkingState()
    {
        if (state != State.WALKING_TO_THEATER)
        {
            float myScreenSector = vizTrack.getCurrentScreenSector().x;
            float sectors = gc.screenDivisions;
            if (myScreenSector < edgeSectorCount || sectors - myScreenSector <= edgeSectorCount)
            {
                state = State.WILLING_TO_CONVERSE;
            }
            else
            {
                state = State.SOLO_IGNORING_STAN;
            }
        }

    }
    private void SetAnimationFlags()
    {
        if (state == State.SOLO_IGNORING_STAN)
        {
            anim.SetBool("TalkToStan", false);
        }
        else if (state == State.WILLING_TO_CONVERSE)
        {
            anim.SetBool("TalkToStan", true);
        }
    }

    private void OutputTalkingState()
    {
        if (state == State.SOLO_IGNORING_STAN)
        {
            Debug.Log("I'm ignoring you lalalala");
        }
        else
        {
            Debug.Log("I'm at an edge and willing to talk");
        }
        Debug.Log("Look direction: " + lookDirection);
    }
    
    private void ChangeCharacterFacingDirection()
    {
        transform.localScale = lookDirection == Direction.LEFT ? lookLeftScale : lookRightScale;
    }

    private void HandleConviction()
    {
        if (talkingToStan && state != State.WALKING_TO_THEATER)
        {
            conviction += Time.deltaTime;
        }
        else
        {
            conviction = conviction-(Time.deltaTime*convictionFallRateDamper) > 0 ? conviction - (Time.deltaTime * convictionFallRateDamper) : 0f ;
        }
        if (conviction > convictionSecondsNeeded)
        {
            state = State.WALKING_TO_THEATER;
        }
        if (conviction == 0f)
        {
            state = State.SOLO_IGNORING_STAN;
        }
    }

    private void WalkToAmphi()
    {
        if (Mathf.Abs(transform.position.x - gatheringPoint.transform.position.x) > reachedTheaterMargin)
        {
            transform.Translate(walkingSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            state = State.REACHED_THEATER;
            transform.position = gatheringPoint.transform.position;
        }
    }

    public void StartRisingConviction()
    {
        talkingToStan = true;
    }
    public void StopRisingConviction()
    {
        talkingToStan = false;
    }
}


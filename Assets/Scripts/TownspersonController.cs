using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownspersonController : MonoBehaviour
{

    //Look in the specified direction unless you're all the way up in that direction, in which case, look the other way.

    /*
     * If I'm flipped because I'm at the edge of the screen, I'm willing to converse with Stan
    */
    
   
    [SerializeField] private State state;
    private Direction lookDirection;

    private VisibilityTracker vizTrack;
    private GameController gc;
    private enum Visibility { VISIBLE, NOT_VISIBLE };
    private Animator anim;
    private Visibility myViz;

    private float stanConversationCount;

    public int edgeSectorCount;
    public enum State { TALKING_TO_TOWNIE, SOLO_IGNORING_STAN, WILLING_TO_CONVERSE };
    public enum Direction { LEFT, RIGHT };
    public GameObject stan;
    public float conviction;
    public float convictionSecondsNeeded;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        vizTrack = GetComponent<VisibilityTracker>();
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        // TEMP
        state = State.TALKING_TO_TOWNIE;
        conviction = 0;
        Debug.Log("???");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Townsperson, activate!");
    }

    // Update is called once per frame
    void Update()
    {
        SetVisiblity();
        SetTalkingState();
        OutputTalkingState();
        

        //if (state == State.SOLO_IGNORING_STAN)
        //{
        //    LookAwayFromStan();
        //}
        //else if (state == State.TALKING_TO_TOWNIE)
        //{
        //    FaceClosestTownie();
        //}

        SetAnimationFlags();
    }

    private void LookAwayFromStan()
    {
        lookDirection = transform.position.x > stan.transform.position.x ? Direction.RIGHT : Direction.LEFT;
    }

    private void FaceClosestTownie()
    {
        // Find the nearest townsperson
        GameObject[] townies = GameObject.FindGameObjectsWithTag("townie");
        GameObject closestTownie = townies[0];
        float minDist = Mathf.Abs(transform.position.x - closestTownie.transform.position.x);
        foreach (GameObject townie in townies)
        {
            if (Mathf.Abs(transform.position.x - townie.transform.position.x) < minDist)
            {
                closestTownie = townie;
                minDist = Mathf.Abs(transform.position.x - townie.transform.position.x);
            }

        }

        // Turn to face this townsperson
        lookDirection = transform.position.x < closestTownie.transform.position.x ? Direction.RIGHT : Direction.LEFT;
    }


    private void SetVisiblity()
    {
        bool viz = vizTrack.Observed();
        myViz = viz ? Visibility.VISIBLE : Visibility.NOT_VISIBLE;
    }


    private void SetTalkingState()
    {
        float myScreenSector = vizTrack.getCurrentScreenSector().x;
        float sectors = gc.screenDivisions;

        if (myScreenSector < edgeSectorCount || sectors - myScreenSector <= edgeSectorCount)
        {
            state = State.WILLING_TO_CONVERSE;
        }
        else
        {
            state = State.TALKING_TO_TOWNIE;
        }

    }
    private void SetAnimationFlags()
    {
        if (state == State.TALKING_TO_TOWNIE)
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
        if (state == State.TALKING_TO_TOWNIE)
        {
            Debug.Log("I'm talkig to another townsperson");
        }
        else
        {
            Debug.Log("I'm at an edge and willing to talk");
        }
    }


}


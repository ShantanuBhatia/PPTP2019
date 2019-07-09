using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanBehaviour : MonoBehaviour
{
    public enum StanStates { INITIAL_SITTING, NOTICING_PLAYER, GATHERING_TOWNIES, PROCLAIMING };
    public int groupCount;
    public string spriteHeadTag;

    [SerializeField] private int collectedGroups;
    public AttentionSeeking cameraFollower;
    private VisibilityTracker vizTrack;
    private StanStates currentState;
    private Animator anim;
    private Vector3 proclaimingSpot;
    private Vector3 positionLastFrame;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == spriteHeadTag)
            {
                vizTrack = child.gameObject.GetComponent<VisibilityTracker>();
                break;
            }
        }


        positionLastFrame = transform.position;
        proclaimingSpot = GameObject.Find("ProclaimFromHere").transform.position;
        collectedGroups = 0;
        anim = GetComponent<Animator>();
        
        //cameraFollower = GetComponent<AttentionSeeking>();
        currentState = StanStates.INITIAL_SITTING;
    }

    // Update is called once per frame
    void Update()
    {
        if (collectedGroups == 3)
        {
            currentState = StanStates.PROCLAIMING;
            cameraFollower.StopFollowingCamera();
        } else
        {
            Debug.Log("Current observation state: " + vizTrack.Observed() + ", current state: " + currentState);
            if (currentState == StanStates.INITIAL_SITTING && vizTrack.Observed())
            {
                currentState = StanStates.GATHERING_TOWNIES;
                //anim.SetBool("ntc", true);
                //anim.SetBool("isRunning", false);
                cameraFollower.StartFollowingCamera();
            }


            if (currentState == StanStates.GATHERING_TOWNIES)
            {
                if (Input.GetKeyDown(","))
                {
                    currentState = StanStates.PROCLAIMING;
                    cameraFollower.StopFollowingCamera();
                }
            }
        }
        if (currentState == StanStates.PROCLAIMING)
        {
            Debug.Log("PROCLAIM! ETC");
            transform.position = proclaimingSpot;
            //anim.SetBool("beingObserved", true);
        }

        setAnimationFlags();

    }

    public void CollectGroup()
    {
        Debug.Log("I got one!");
        collectedGroups++;
    }

    public void setAnimationFlags()
    {
        if (currentState == StanStates.INITIAL_SITTING)
        {
            anim.SetBool("ntc", false);
        }
        else if (currentState == StanStates.GATHERING_TOWNIES)
        {
            anim.SetBool("ntc", true);
            anim.SetBool("isRunning", false);
        }
        Vector3 currentPosition = transform.position;
        if (currentPosition != positionLastFrame)
        {
            anim.SetBool("isRunning", true);
        } else
        {
            anim.SetBool("isRunning", false);
        }
        positionLastFrame = transform.position;
    }
}

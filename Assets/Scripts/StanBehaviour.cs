using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanBehaviour : MonoBehaviour
{
    public enum StanStates { INITIAL_SITTING, NOTICING_PLAYER, GATHERING_TOWNIES, PROCLAIMING };
    public int groupCount;

    [SerializeField] private int collectedGroups;
    private AttentionSeeking cameraFollower;
    private VisibilityTracker vizTrack;
    private StanStates currentState;
    private Animator anim;
    private Vector3 proclaimingSpot;
    // Start is called before the first frame update
    void Start()
    {
        proclaimingSpot = GameObject.Find("ProclaimFromHere").transform.position;
        collectedGroups = 0;
        anim = GetComponent<Animator>();
        vizTrack = GetComponent<VisibilityTracker>();
        cameraFollower = GetComponent<AttentionSeeking>();
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
                currentState = StanStates.NOTICING_PLAYER;
                anim.SetBool("ntc", true);
            }
            if (currentState == StanStates.NOTICING_PLAYER)
            {
                if (Input.GetKeyDown("g"))
                {
                    currentState = StanStates.GATHERING_TOWNIES;
                    cameraFollower.StartFollowingCamera();
                }
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
            anim.SetBool("beingObserved", true);
        }

    }

    public void CollectGroup()
    {
        Debug.Log("I got one!");
        collectedGroups++;
    }
}

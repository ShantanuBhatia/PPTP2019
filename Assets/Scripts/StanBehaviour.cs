using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanBehaviour : MonoBehaviour
{
    public enum StanStates { INITIAL_SITTING, NOTICING_PLAYER, GATHERING_TOWNIES, PROCLAIMING };

    private AttentionSeeking cameraFollower;
    private VisibilityTracker vizTrack;
    private StanStates currentState;

    // Start is called before the first frame update
    void Start()
    {
        vizTrack = GetComponent<VisibilityTracker>();
        cameraFollower = GetComponent<AttentionSeeking>();
        currentState = StanStates.INITIAL_SITTING;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current observation state: " + vizTrack.Observed() + ", current state: " + currentState);
        if (currentState == StanStates.INITIAL_SITTING && vizTrack.Observed())
        {
            currentState = StanStates.NOTICING_PLAYER;
        }
        if (currentState == StanStates.NOTICING_PLAYER)
        {
            if (Input.GetKeyDown("g"))
            {
                currentState = StanStates.GATHERING_TOWNIES;
                cameraFollower.StartFollowingCamera();
            }
        }
        if(currentState == StanStates.GATHERING_TOWNIES)
        {
            if (Input.GetKeyDown(","))
            {
                currentState = StanStates.PROCLAIMING;
                cameraFollower.StopFollowingCamera();
            }
        }

    }
}

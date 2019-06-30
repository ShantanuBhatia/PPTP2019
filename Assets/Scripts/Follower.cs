using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Follower objects follow their leader to try and maintain a minumum distance from them. 
 If a follower object reaches its target, it stops following.
 */
public class Follower : MonoBehaviour
{
    private enum FollowerStates { CONVERSATION, FOLLOWING, REACHED_DEST };
    private FollowerStates currentState;
    public GameObject leader;

    void Start()
    {
        currentState = FollowerStates.CONVERSATION;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            currentState = FollowerStates.FOLLOWING;
        }
        if (Input.GetKeyDown("w"))
        {
            currentState = FollowerStates.REACHED_DEST;
        }

        if (currentState == FollowerStates.FOLLOWING)
        {

        }
    }
}

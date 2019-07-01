using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Follower objects follow their leader to try and maintain a minumum distance from them. 
 If a follower object reaches its target, it stops following.
 */
public class Follower : MonoBehaviour
{
    private enum FollowerStates { NOT_FOLLOWING, CONVERSATION, FOLLOWING, REACHED_DEST };
    private FollowerStates currentState;
    public GameObject leader;
    public Vector3 targetPosition;
    public float maxTalkTime;
    public float leaderConversationDist;

    [SerializeField] private float currentTalkTime;
    [SerializeField] private bool moving;
    [SerializeField] private float waitTimeElapsed;
    [SerializeField] private bool reachedDestination;
    [SerializeField] private GameController gc;

    void Start()
    {
        currentState = FollowerStates.NOT_FOLLOWING;
        currentTalkTime = 0f;


        gc = GameObject.Find("GameController").GetComponent<GameController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == FollowerStates.NOT_FOLLOWING && Mathf.Abs(leader.transform.position.x - transform.position.x) < leaderConversationDist)
        {
            currentState = FollowerStates.CONVERSATION;
        }
        else if (currentState == FollowerStates.CONVERSATION && Mathf.Abs(leader.transform.position.x - transform.position.x) > leaderConversationDist)
        {
            currentState = FollowerStates.NOT_FOLLOWING;
        }

        if (currentState == FollowerStates.CONVERSATION)
        {
            currentTalkTime += Time.deltaTime;
            Debug.Log("Conversing...");
            if (currentTalkTime >= maxTalkTime)
            {
                Debug.Log("We're convinced, and we're going to follow you now");
                currentState = FollowerStates.FOLLOWING;
                TriggerFollowStart();
            }
        } else
        {
            currentTalkTime = 0f;
            if (currentState == FollowerStates.REACHED_DEST)
            {
                transform.position = targetPosition;
            }
        }
        
    }

    public void TriggerConversation()
    {
        currentState = FollowerStates.CONVERSATION;
    }

    public void TriggerFollowStart()
    {
        currentState = FollowerStates.FOLLOWING;
        transform.SetParent(leader.transform);
    }

    public void TriggerFollowStop()
    {
        currentState = FollowerStates.REACHED_DEST;
    }

    public bool EnableConversationLook()
    {
        return (currentState == FollowerStates.NOT_FOLLOWING);
    }

}

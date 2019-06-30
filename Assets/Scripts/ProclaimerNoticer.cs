using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProclaimerNoticer : MonoBehaviour
{
    [SerializeField]
    private enum StanStates  { SAD, FOLLOW, LEADER, GOTOSTAGE, PROCLAIMER };
    private enum crowdStates { CONVERSATION, FOLLOWING, SEATED };
    private VisibilityTracker Stan;
    private VisibilityTracker[] crowd1, crowd2, crowd3;
    


    private StanStates stanState;
    private crowdStates crowd1State;
    private crowdStates crowd2State;
    private crowdStates crowd3State;


    void Start()
    {
        stanState = StanStates.SAD;
        crowd1State = crowd2State = crowd3State = crowdStates.CONVERSATION;
        crowd1 = new VisibilityTracker[3];
        crowd2 = new VisibilityTracker[3];
        crowd3 = new VisibilityTracker[4];
    }


    void Update()
    {
        /**/
    }
}

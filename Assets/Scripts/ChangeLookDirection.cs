using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Logic for Townsperson behaviour
 * Townsperson can be talking to another townsperson, or, if on their own, will face away from Stan unless cornered.
 * 
 */

public class ChangeLookDirection : MonoBehaviour
{
    private VisibilityTracker vizTrack;
    private GameController gc;
    private enum Visibility { VISIBLE, NOT_VISIBLE };
    private enum TalkingState { TALKING_AMONG_SELVES, WILLING_TO_CONVERSE, TALKING_TO_STAN };
    private Visibility myViz;
    private TalkingState myTalkStatus;
    private float stanConversationCount;

    public int edgeSectorCount;

    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
    }


    void Update()
    {

        Debug.Log("REMOVE THIS SCRIPT! IT'S DEPRECATED");
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
            myTalkStatus = TalkingState.WILLING_TO_CONVERSE;
        }
        else
        {
            myTalkStatus = TalkingState.TALKING_AMONG_SELVES;
        }

    }
    private void SetAnimationFlags()
    {
        return;
    }

    private void OutputTalkingState()
    {
        if (myTalkStatus == TalkingState.TALKING_AMONG_SELVES)
        {
            Debug.Log("I'm not at an edge");
        }
        else
        {
            Debug.Log("I'm at an edge and willing to talk");
        }
    }
}

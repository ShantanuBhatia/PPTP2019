using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanBehaviour : MonoBehaviour
{
    public enum StanStates { INITIAL_SITTING, NOTICING_PLAYER, GATHERING_TOWNIES, PROCLAIMING, DESPERATE };
    public int groupCount;
    public string spriteHeadTag, noticingAnimationClipName;
    public int animationLayer;
    public float desperation;
    public float desperationCutoffSeconds;
    [SerializeField] private int collectedGroups;
    public AttentionSeeking cameraFollower;
    private VisibilityTracker vizTrack;
    private StanStates currentState;
    private Animator anim;
    private Vector3 proclaimingSpot;
    private Vector3 positionLastFrame, currentPosition;
    private Vector3 lookRightScale, lookLeftScale;
    private string m_ClipName;
    private AnimatorClipInfo[] m_CurrentClipInfo;

    void Start()
    {
        lookRightScale = transform.localScale;
        lookLeftScale = new Vector3(-lookRightScale.x, lookRightScale.y, lookRightScale.z);
        foreach (Transform child in transform)
        {
            if (child.tag == spriteHeadTag)
            {
                vizTrack = child.gameObject.GetComponent<VisibilityTracker>();
                break;
            }
        }
        desperation = 0f;

        positionLastFrame = transform.parent.position;
        proclaimingSpot = GameObject.Find("ProclaimFromHere").transform.position;
        collectedGroups = 0;
        anim = GetComponent<Animator>();
        

        currentState = StanStates.INITIAL_SITTING;
    }


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
                
            }


            if (currentState == StanStates.GATHERING_TOWNIES)
            {
                m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(animationLayer);
                m_ClipName = m_CurrentClipInfo[0].clip.name;
                if (m_ClipName == "idle2" )
                {
                    cameraFollower.StartFollowingCamera();
                }

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
        HandleDesperation();
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
            if (transform.parent.position != positionLastFrame)
            {
                anim.SetBool("isRunning", true);
                
                
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
            if (cameraFollower.getTalkStatus())
            {
                anim.SetBool("TalkingToTownies", true);
            }
            else
            {
                anim.SetBool("TalkingToTownies", false);
            }

        }
        transform.localScale = cameraFollower.lookDirection == 0 ? lookLeftScale : lookRightScale; 
        positionLastFrame = transform.parent.position;

    }

    public void HandleDesperation()
    {
        if (currentState != StanStates.DESPERATE)
        {
            if(desperation + Time.deltaTime > desperationCutoffSeconds)
            {
                currentState = StanStates.DESPERATE;
            }
            desperation += Time.deltaTime;
            cameraFollower.UpdateDesperationMovementSpeed(desperation);
        }
    }

}

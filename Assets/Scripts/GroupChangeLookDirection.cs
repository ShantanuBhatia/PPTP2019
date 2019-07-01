using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Like change look direction, but for a whole group of people. It stores each child gameobject's initial x-flip of the Sprite as the default.
 * If any of the sprites are near the edge of the screen, they should all look the other way
 * If they aren't, then there is no flip and they keep looking in their initial directions.
 */
public class GroupChangeLookDirection : MonoBehaviour
{
    private enum FlipStates { NO, LEFT, RIGHT };
    private FlipStates flipState;
    private Follower myFollow;

    public int groupSize;
    public bool LeftLookValue;
    public GameObject[] groupMembers;
    public bool[] initialDirections;
    public GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        myFollow = GetComponent<Follower>();
        groupMembers = new GameObject[groupSize];
        initialDirections = new bool[groupSize];
        int i = 0;
        foreach (Transform t in transform)
        {
            groupMembers[i] = t.gameObject;
            initialDirections[i] = t.gameObject.GetComponent<SpriteRenderer>().flipX;
            Debug.Log(t.name + "," + initialDirections[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myFollow.EnableConversationLook())
        {
            FlipStates flipTo = RequiredFlip();
            if (flipTo == FlipStates.LEFT)
            {
                foreach (GameObject townie in groupMembers)
                {
                    Debug.Log("Flipping the townie named " + townie.transform.name);
                    townie.GetComponent<SpriteRenderer>().flipX = LeftLookValue;
                }
            }
            else if (flipTo == FlipStates.RIGHT)
            {
                foreach (GameObject townie in groupMembers)
                {
                    Debug.Log("Flipping the townie named " + townie.transform.name);
                    townie.GetComponent<SpriteRenderer>().flipX = !LeftLookValue;
                }
            }
            else
            {
                int i = 0;
                foreach (GameObject townie in groupMembers)
                {
                    townie.GetComponent<SpriteRenderer>().flipX = initialDirections[i];
                    i++;
                }
            }
        }
       
    }

    /*
     * A Flip is required if everyone is on screen, and everyone is near one of the edges.
     */
    FlipStates RequiredFlip()
    {
        FlipStates check = FlipStates.NO;
        foreach(GameObject townie in groupMembers)
        {
            // Automatically fail if there is anyone off screen
            if (townie.GetComponent<VisibilityTracker>().checkVisible() == false)
            {
                return FlipStates.NO;
            }

            // For the remainig ones, set based on whether everyone is in the same sector or not.
            if (townie.GetComponent<VisibilityTracker>().getCurrentScreenSector().x >= (gc.screenDivisions-1))
            {
                check = FlipStates.RIGHT;
            }
            if(townie.GetComponent<VisibilityTracker>().getCurrentScreenSector().x < 1)
            {
                check = FlipStates.LEFT;
            }
        }

        return check;
    }
}

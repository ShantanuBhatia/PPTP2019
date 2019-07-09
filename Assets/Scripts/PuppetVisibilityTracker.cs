using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetVisibilityTracker : MonoBehaviour
{
    public GameObject head;
    public VisibilityTracker HeadVisibilityTracker;
    public string SpriteHeadTag;

    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.tag == SpriteHeadTag)
            {
                head = child.gameObject;
                break;
            }
        }
        HeadVisibilityTracker = head.GetComponent<VisibilityTracker>();
    }

    void Update()
    {
        
    }
}

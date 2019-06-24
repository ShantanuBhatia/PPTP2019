using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLookDirection : MonoBehaviour {


    //Look in the specified direction unless you're all the way up in that direction, in which case, look the other way.
    private SpriteRenderer mySpriteRenderer;
    private bool lookingLeft;
    void Start () {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        lookingLeft = mySpriteRenderer.flipX; // they start off looking left, and then every type they flip, flip this value and assign it.
    }
	
	
	void Update () {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (mySpriteRenderer != null)
            {
                // flip the sprite
                lookingLeft = !lookingLeft;
                mySpriteRenderer.flipX = lookingLeft;
            }
        }

            
    }
}

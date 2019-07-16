﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
	private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim=GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("f")){
        	Debug.Log("test1");
        	anim.SetBool("isstand",true);
        }
        else{
        	anim.SetBool("isstand",false);
        	Debug.Log("tests");
        }
    }
}

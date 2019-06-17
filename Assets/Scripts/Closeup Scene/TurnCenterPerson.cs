using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCenterPerson : MonoBehaviour
{
	public Sprite middlepersonr;
	public Sprite middlepersonl;
	public CameraController camCon;
	private float leftpersondist;
	private float rightpersondist;
	GameObject leftperson, rightperson, middleperson;

    // Start is called before the first frame update
    void Start()
    {
        leftperson = GameObject.Find("leftperson");
        rightperson = GameObject.Find("rightperson");
        middleperson = GameObject.Find("middleperson");
        camCon = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        leftpersondist = leftperson.GetComponent<ProximityToCenter>().GetCenterDistance();
        //Debug.Log("LEFT PERSON is " + leftpersondist + " pixels from the center");
        rightpersondist = rightperson.GetComponent<ProximityToCenter>().GetCenterDistance();
        //Debug.Log("RIGHT PERSON is " + rightpersondist + " pixels from the center");
        if( camCon.canObserve()){
	        if ( leftpersondist>rightpersondist){
	        	Debug.Log("RIGHT PERSON is closer to center");
	        	this.GetComponent<SpriteRenderer>().sprite=middlepersonr;
	        }
	        else{
	        	Debug.Log("LEFT PERSON is closer to center");
	        	this.GetComponent<SpriteRenderer>().sprite=middlepersonl;
	        }
        }
    }
}

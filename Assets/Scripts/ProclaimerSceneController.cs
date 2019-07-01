using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProclaimerSceneController : MonoBehaviour {


    /*
     * Controller script for the Proclaimer scene
     * If we're in the initial phase of the scene, nothing is being observed, and the Proclaimer does nothing. The townspeople also do nothing
     * While the guy is being observed, the animation of the proclaimer...proclaiming...plays
     * If the tower is observed, from then on the only thing being done is everyone bowing to the tower.
     */

    public GameObject proclaimer, tower, townperson1, townperson2;


    private VisibilityTracker proclaimerViz, towerViz;
    private enum SceneState { NO_ACTION, PROC_OBSERVING, TOWER_SPOTTED};
    private SceneState sceneState;
	
	void Start () {
        proclaimerViz = proclaimer.GetComponent<VisibilityTracker>();
        towerViz = tower.GetComponent<VisibilityTracker>(); 
        sceneState = SceneState.NO_ACTION;
	}
	

	void Update () {

		if (sceneState == SceneState.TOWER_SPOTTED)
        {
            //Debug.Log("Bow down!");
        }
        else
        {
            if (proclaimerViz.beingObserved)
            {
                sceneState = SceneState.PROC_OBSERVING;
            }
            else if (towerViz.beingObserved)
            {
                sceneState = SceneState.TOWER_SPOTTED;
                tower.transform.GetChild(0).GetComponent<LightFlicker>().stopFlickering = false;
            }
            else
            {
                sceneState = SceneState.NO_ACTION;
            }
        }

        //Debug.Log("Current game state is " + sceneState);
	}
}

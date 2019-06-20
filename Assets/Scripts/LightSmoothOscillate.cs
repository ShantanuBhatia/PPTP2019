using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSmoothOscillate : MonoBehaviour {

    public float maxDiff;
    public float speedMultiplier;
    private float intensity;
    private float baseIntensity;
    private Light lightSource;


    // Use this for initialization
    void Start () {
        lightSource = GetComponent<Light>();
        baseIntensity = lightSource.intensity;

    }
	
	// Update is called once per frame
	void Update () {
        intensity = baseIntensity + Mathf.Sin(Time.time * speedMultiplier) * maxDiff;
        lightSource.intensity = intensity;
	}
}

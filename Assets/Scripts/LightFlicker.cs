using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    public float maxReduction;
    public float maxIncrease;
    public float minFlickerIntensity;
    public float maxFlickerIntensity;
    public float rateDamping;
    public float strength;
    public bool stopFlickering;


    private Light lightSource;
    private float baseIntensity;
    private bool flickering;


    public void Reset()
    {
        maxReduction = 0.2f;
        maxIncrease = 0.2f;
        rateDamping = 0.1f;
        strength = 300;
    }

    // Use this for initialization
    void Start () {
        //stopFlickering = true;
        lightSource = GetComponent<Light>();
        baseIntensity = lightSource.intensity;
        StartCoroutine(DoFlicker());
    }
	
	// Update is called once per frame
	void Update () {
        if (!stopFlickering && !flickering)
        {
            StartCoroutine(DoFlicker());
        }
    }

    private IEnumerator DoFlicker()
    {
        flickering = true;
        while (!stopFlickering)
        {
            lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(baseIntensity - maxReduction, baseIntensity + maxIncrease), strength * Time.deltaTime);
            yield return new WaitForSeconds(rateDamping);
        }
    }
}

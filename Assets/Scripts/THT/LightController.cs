using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class LightController : MonoBehaviour
{
    // Start is called before the first frame update
    Light2D theLight;
    Transform LightTransform;
    bool isStart;
    [HideInInspector]
    public bool isRange;
    void Start()
    {
        GameObject l = GameObject.Find("Light Effects");
        LightTransform = l.transform;
        theLight = l.GetComponent<Light2D>();
        isStart = true;
    }
    void EndLight()
    {
        if (isRange)
        {
            LightTransform.position = transform.position;
            if (theLight.pointLightOuterRadius > 0)
            {
                theLight.pointLightOuterRadius -= 0.5f;
            }
            else
            {
                isRange = false;
            }
        }
    }
    void StartLight()
    {
        if (isStart)
        {
            if (theLight.pointLightOuterRadius < 100)
            {
                theLight.pointLightOuterRadius += 1;
            }
            else
            {
                isStart = false;
                theLight.pointLightOuterRadius = 100;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        LightTransform.position = transform.position;
        StartLight();
        EndLight();
    }
}

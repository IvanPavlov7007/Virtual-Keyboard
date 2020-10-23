﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightingKey : MonoBehaviour
{
    Color baseColor, highColor;
    TextMeshPro textDisplayer;

    [SerializeField]
    float maxSinTime, transmissionSpeed;
    float t;

    static List<LightingKey> allLightingKeys;

    private void Awake()
    {
        if (allLightingKeys == null)
            allLightingKeys = new List<LightingKey>();
        allLightingKeys.Add(this);
    }

    void Start()
    {
        textDisplayer = GetComponentInChildren<TextMeshPro>();
        baseColor = textDisplayer.color;
        highColor = Color.red;
    }

    bool stimulated = false;

    public void StimulateCenter()
    {
        foreach(var lightingKey in allLightingKeys)
        {
            if (transmissionSpeed > 0f)
                lightingKey.StimulateInTime(Vector3.Distance(transform.position, lightingKey.transform.position) / transmissionSpeed);
            else
                lightingKey.Stimulate();
        }
    }

    public void StimulateInTime(float time)
    {
        StartCoroutine(waitToStimulate(time));
    }

    public void Stimulate(float t = 0f)
    {
        this.t = t;
        stimulated = true;
    }

    [SerializeField]
    float phaseDistR, phaseDistG, phaseDistB;


    IEnumerator waitToStimulate(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        Stimulate();
    }

    void Update()
    {
        if (stimulated)
        {
            Color res;
            t += Time.deltaTime;
            if (t >= maxSinTime)
            {
                //t = 0f;
                res = baseColor;
                stimulated = false;
            }
            else
            {
                float r = Mathf.Sin((t * Mathf.PI + phaseDistR * Mathf.PI) / maxSinTime),
                    g = Mathf.Sin((t * Mathf.PI + phaseDistG * Mathf.PI) / maxSinTime),
                    b = Mathf.Sin((t * Mathf.PI + phaseDistB * Mathf.PI) / maxSinTime);
                res = new Color(r, g, b);
            }
            textDisplayer.color = res;
        }
    }
}

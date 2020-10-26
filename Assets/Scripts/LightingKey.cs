using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightingKey : KeyListener
{
    Color baseColor;
    TextMeshPro textDisplayer;
    protected override void Start()
    {
        base.Start();
        textDisplayer = GetComponentInChildren<TextMeshPro>();
        baseColor = textDisplayer.color;
    }

    public void SetColor(Color color)
    {
        textDisplayer.color = color;
    }

    public void ResetColor()
    {
        textDisplayer.color = baseColor;
    }
}

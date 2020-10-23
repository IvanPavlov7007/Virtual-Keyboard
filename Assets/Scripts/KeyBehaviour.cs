using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class KeyBehaviour : MonoBehaviour
{
    TextMeshPro textDisplayer;
    PhysicalKeyResponse physicalResponse;
    public string actualKeyboardKey;

    void Start()
    {
        textDisplayer = GetComponentInChildren<TextMeshPro>();
        physicalResponse = GetComponent<PhysicalKeyResponse>();
        textDisplayer.text = actualKeyboardKey.ToUpper();
    }

    void Update()
    {
#if UNITY_EDITOR
        textDisplayer.text = actualKeyboardKey.ToUpper();
#endif
        if (Input.GetKeyDown(actualKeyboardKey))
        {
            physicalResponse.Press();
            Debug.Log(actualKeyboardKey);
        }
        else if (Input.GetKeyUp(actualKeyboardKey))
            physicalResponse.Release();
    }
}

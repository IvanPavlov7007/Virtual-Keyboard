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
        gameObject.name = actualKeyboardKey;
#endif
        if (Input.GetKeyDown(actualKeyboardKey))
        {
            physicalResponse.Press();
            TurnManager.Instance.FieldPressed(this);
            Debug.Log(actualKeyboardKey);
        }
        else if (Input.GetKeyUp(actualKeyboardKey))
            physicalResponse.Release();
    }
}

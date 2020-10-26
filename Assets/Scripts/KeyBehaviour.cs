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

    public event KeyBehaviourAction onKeyPressed, onKeyReleased;

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
            if (onKeyPressed != null)
                onKeyPressed(this);
            
            //Debug.Log(actualKeyboardKey);
        }
        else if (Input.GetKeyUp(actualKeyboardKey))
        {
            if (onKeyReleased != null)
                onKeyReleased(this);
        }
    }
}

public delegate void KeyBehaviourAction(KeyBehaviour behaviour);
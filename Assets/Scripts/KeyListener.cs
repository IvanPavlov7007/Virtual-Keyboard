using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(KeyBehaviour))]
public class KeyListener : MonoBehaviour
{
    KeyBehaviour keyBehaviour;
    protected virtual void Start()
    {
        keyBehaviour = GetComponent<KeyBehaviour>();
        keyBehaviour.onKeyPressed += OnKeyPressed;
        keyBehaviour.onKeyReleased += OnKeyReleased;
    }

    protected virtual void OnKeyPressed(KeyBehaviour field)
    {

    }

    protected virtual void OnKeyReleased(KeyBehaviour field)
    {

    }
}

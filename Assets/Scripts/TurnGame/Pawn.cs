using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{

    KeyBehaviour curField;
    public KeyBehaviour currentField
    {
        get { return curField; }
        private set { }
    }

    public void MoveToField(KeyBehaviour field)
    {
        transform.position = CommonTools.yPlaneVector(field.transform.position, transform.position.y);
        curField = field;
    }

    void Start()
    {
        TurnManager.Instance.allFigures.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

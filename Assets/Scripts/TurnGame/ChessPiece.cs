using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{

    public float movementTime = 1f, aTanVal;

    KeyBehaviour curField;
    public KeyBehaviour currentField
    {
        get { return curField; }
        private set { }
    }

    public Coroutine MoveToField(KeyBehaviour field, bool moveImediately = false)
    {
        curField = field;
        Vector3 pos = CommonTools.yPlaneVector(field.transform.position, transform.position.y);
        if (!moveImediately)
            return StartCoroutine(moveAnimation(pos));
        else
        {
            transform.position = pos;
            return null;
        }
    }

    IEnumerator moveAnimation(Vector3 destination)
    {
        Vector3 initPos = transform.position;
        Vector3 velocity = Vector3.zero;
        float t = 0f;
        while(t < movementTime)
        {
            t += Time.deltaTime;
            transform.position = Vector3.Lerp(initPos, destination, CommonTools.ClampAtan01(t, aTanVal));
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    void Start()
    {
        TurnManager.Instance.allChessPieces.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

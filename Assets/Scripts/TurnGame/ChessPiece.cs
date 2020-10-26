using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessSide { White, Black}

public class ChessPiece : MonoBehaviour
{
    public ChessSide side;

    public float movementTime = 1f, aTanVal;

    ChessFieldKeyBehaviuor curField;
    public ChessFieldKeyBehaviuor currentField
    {
        get { return curField; }
        private set { }
    }

    public virtual Coroutine MoveToField(ChessFieldKeyBehaviuor field, bool moveImediately = false)
    {
        Vector3 pos = CommonTools.yPlaneVector(field.transform.position, transform.position.y);
        if (!moveImediately)
            return StartCoroutine(processMove(field,pos));
        else
        {
            curField = field;
            field.chessPieceHere = this;
            transform.position = pos;
            return null;
        }
    }

    public virtual Coroutine ReturnToStock()
    {
        curField.chessPieceHere = null;
        curField = null;
        return StartCoroutine(moveAnimation(TurnManager.Instance.GetStockPosition()));
    }

    public bool PossibleField(ChessFieldKeyBehaviuor field)
    {
        return System.Array.Find(PossibleFields(), x => x == field);
    }

    public virtual ChessFieldKeyBehaviuor[] PossibleFields()
    {
        return TurnManager.Instance.flattenedFieldsGrid;
    }

    protected virtual IEnumerator processMove(ChessFieldKeyBehaviuor field, Vector3 destination)
    {
        if(field.chessPieceHere != null)
        {
            yield return field.chessPieceHere.ReturnToStock();
        }

        yield return StartCoroutine(moveAnimation(destination));

        curField.chessPieceHere = null;
        curField = field;
        field.chessPieceHere = this;

        yield return null;
    }

    protected virtual IEnumerator moveAnimation(Vector3 destination)
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

    protected void Start()
    {
        TurnManager.Instance.allChessPieces.Add(this);
    }
}

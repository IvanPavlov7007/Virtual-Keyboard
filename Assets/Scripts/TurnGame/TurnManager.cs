using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static TurnManager instance = null;
    public static TurnManager Instance
    {
        get { return instance; }
        private set { }
    }
    ChessPiece currentPawn = null;

    public List<ChessPiece> allChessPieces;

    bool nextTurnIsReady = true;
    public bool NextTurnIsReady
    {
        get { return nextTurnIsReady; }
        private set { nextTurnIsReady = value; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        allChessPieces = new List<ChessPiece>();
    }


    public void FieldPressed(KeyBehaviour field)
    {
        if (!nextTurnIsReady)
            return;
        nextTurnIsReady = false;
        StartCoroutine(doTurn(field));
        
    }

    IEnumerator doTurn(KeyBehaviour field)
    {
        ChessPiece figureUnderField = allChessPieces.Find(x => x.currentField == field);
        if (figureUnderField != null)
            currentPawn = figureUnderField;
        else if (instance.currentPawn != null)
            yield return instance.currentPawn.MoveToField(field);

        nextTurnIsReady = true;
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [Header("Fields setting")]
    public RowOfFields[] GridOfFields;
    //public ChessFieldKeyBehaviuor[] flattenedFieldsGrid;
    //public int[] countOfFieldsInRow;

    //List<ChessFieldKeyBehaviuor[]> fieldGrid;

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

        //fieldGrid = new List<ChessFieldKeyBehaviuor[]>();
        //int i = 0;
        //for(int row = 0; row < countOfFieldsInRow.Length; row++)
        //{
        //    int curFieldsCount = countOfFieldsInRow[row];
        //    fieldGrid.Add(new ChessFieldKeyBehaviuor[curFieldsCount]);
        //    for (int x = 0; x < curFieldsCount; x++)
        //    {
        //        fieldGrid[row][x] = flattenedFieldsGrid[i]; 
        //        i++;
        //    }
        //}
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

[System.Serializable]
public class RowOfFields
{
    public List<ChessFieldKeyBehaviuor> fields;

    public ChessFieldKeyBehaviuor this[int x]
    {
        get => fields[x];
        set => fields[x] = value;
    }
}

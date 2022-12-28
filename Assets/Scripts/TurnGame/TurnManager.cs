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
    ChessPiece currentChessPiece = null;

    public List<ChessPiece> allChessPieces;

    [Header("Fields setting")]
    public RowOfFields[] GridOfFields;
    [HideInInspector]
    public ChessFieldKeyBehaviuor[] flattenedFieldsGrid;
    //public int[] countOfFieldsInRow;

    //List<ChessFieldKeyBehaviuor[]> fieldGrid;

    [Header("Stock settings")]
    public Transform StockInitialPosition;
    public Vector3 nextPieceInStockDirection = Vector3.right;
    int lastStockIndex = 0;

    bool nextTurnIsReady = true;
    public bool NextTurnIsReady
    {
        get { return nextTurnIsReady; }
        private set { nextTurnIsReady = value; }
    }

    SpriteIndicator currentPieceIndicator;

    private void Start()
    {
        for(int y = 0; y < GridOfFields.Length; y++)
        {
            var row = GridOfFields[y];
            for (int x = 0; x < row.Count; x++)
            {
                row[x].Initialise(x, y);
            }
        }

        currentPieceIndicator = FindObjectOfType<SpriteIndicator>();
        currentPieceIndicator.HideIndicator();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        allChessPieces = new List<ChessPiece>();

        int count = 0;
        foreach(var r in GridOfFields)
        {
            count += r.Count;
        }

        flattenedFieldsGrid = new ChessFieldKeyBehaviuor[count];
        int i = 0;
        for (int row = 0; row < GridOfFields.Length; row++)
        {
            int curFieldsCount = GridOfFields[row].Count;
            for (int x = 0; x < curFieldsCount; x++)
            {
                flattenedFieldsGrid[i] = GridOfFields[row][x];
                i++;
            }
        }
    }


    public void FieldPressed(ChessFieldKeyBehaviuor field)
    {
        if (!nextTurnIsReady)
            return;
        nextTurnIsReady = false;
        StartCoroutine(doTurn(field));
        
    }

    ChessFieldKeyBehaviuor[] lastHighlightedFields = null;

    IEnumerator doTurn(ChessFieldKeyBehaviuor field)
    {
        ChessPiece figureUnderField = allChessPieces.Find(x => x.currentField == field);

        if (lastHighlightedFields != null)
        {
            foreach (var f in lastHighlightedFields)
            {
                f.GetComponent<LightingKey>().ResetColor();
            }
        }


        if (currentChessPiece != null && currentChessPiece.PossibleField(field))
        {
            yield return currentChessPiece.MoveToField(field);
            deselectPiece();
        }
        else if (figureUnderField != null)
        {
            currentChessPiece = figureUnderField;
            currentPieceIndicator.ShowIndicator(currentChessPiece.transform);
            lastHighlightedFields = currentChessPiece.PossibleFields();
            foreach (var f in lastHighlightedFields)
                f.GetComponent<LightingKey>().SetColor(f.chessPieceHere == null ? Color.green : Color.red);

        }
        else
            deselectPiece();

        nextTurnIsReady = true;
        yield return null;
    }

    void deselectPiece()
    {
        currentPieceIndicator.HideIndicator();
        currentChessPiece = null;
    }


    public Vector3 GetStockPosition()
    {
        return StockInitialPosition.position + nextPieceInStockDirection * lastStockIndex++;
    }
}

[System.Serializable]
public class RowOfFields
{
    public List<ChessFieldKeyBehaviuor> fields;

    public ChessFieldKeyBehaviuor this[int x]
    {
        get
        {
            if(x < 0 || x >= Count)
                return null;
            return fields[x];
        }
        set => fields[x] = value;
    }

    public int Count
    {
        get => fields.Count;
    }
}

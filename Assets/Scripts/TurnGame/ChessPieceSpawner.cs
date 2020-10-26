using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject whitePawnPrefab, blackPawnPrefab;
    [SerializeField]
    ChessFieldKeyBehaviuor[] keysForWhitePawns,keysForBlackPawns;

    private void Start()
    {
        foreach(var field in keysForWhitePawns)
        {
            SpawnChessPiece(field,true);
        }

        foreach (var field in keysForBlackPawns)
        {
            SpawnChessPiece(field, false);
        }

    }

    public void SpawnChessPiece(ChessFieldKeyBehaviuor field, bool spawnWhite)
    {
        Instantiate(spawnWhite?whitePawnPrefab : blackPawnPrefab).GetComponent<ChessPiece>().MoveToField(field, true);
    }
}

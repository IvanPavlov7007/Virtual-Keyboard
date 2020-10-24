using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject pawnPrefab;
    [SerializeField]
    KeyBehaviour[] keysForPawns;


    private void Start()
    {
        foreach(var field in keysForPawns)
        {
            SpawnChessPiece(field);
        }
    }

    public void SpawnChessPiece(KeyBehaviour field)
    {
        Instantiate(pawnPrefab).GetComponent<ChessPiece>().MoveToField(field, true);
    }
}

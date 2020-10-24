using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiguresSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject pawnPrefab;
    [SerializeField]
    KeyBehaviour[] keysForPawns;


    private void Start()
    {
        foreach(var field in keysForPawns)
        {
            SpawnPawn(field);
        }
    }

    public void SpawnPawn(KeyBehaviour field)
    {
        Instantiate(pawnPrefab).GetComponent<Pawn>().MoveToField(field);
    }
}

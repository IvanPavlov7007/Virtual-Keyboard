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
    Pawn currentPawn = null;

    public List<Pawn> allFigures;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        allFigures = new List<Pawn>();
    }


    public void FieldPressed(KeyBehaviour field)
    {
        Pawn figureUnderField = allFigures.Find(x => x.currentField == field);
        if (figureUnderField != null)
            currentPawn = figureUnderField;
        else if (instance.currentPawn != null)
            instance.currentPawn.MoveToField(field);
    }
}

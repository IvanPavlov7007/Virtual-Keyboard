using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFieldKeyBehaviuor : KeyListener
{
    int x, y;
    public int X { get { return x; } private set { x = value; } }
    public int Y { get { return y; } private set { y = value; } }

    public ChessPiece chessPieceHere = null;

    public void Initialise(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    protected override void OnKeyPressed(KeyBehaviour field)
    {
        base.OnKeyPressed(field);
        TurnManager.Instance.FieldPressed(this);
    }
}

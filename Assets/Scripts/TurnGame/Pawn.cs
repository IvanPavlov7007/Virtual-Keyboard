using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece
{
    //public override Coroutine MoveToField(ChessFieldKeyBehaviuor field, bool moveImediately = false)
    //{
    //    if(!moveImediately)
    //    {

    //    }
    //    return base.MoveToField(field, moveImediately);
    //}

    public override ChessFieldKeyBehaviuor[] PossibleFields()
    {
        List<ChessFieldKeyBehaviuor> fields = new List<ChessFieldKeyBehaviuor>();
        int x = currentField.X;
        int y = currentField.Y;

        var grid = TurnManager.Instance.GridOfFields;
        ChessFieldKeyBehaviuor field;

        if (side == ChessSide.White)
        {
            field = grid[y][x + 1];
            if (field != null && field.chessPieceHere == null)
                fields.Add(grid[y][x + 1]);

            if(y + 1 < grid.Length)
            {
                field = grid[y + 1][x + 1];
                if (field != null && field.chessPieceHere != null && field.chessPieceHere.side != ChessSide.White)
                    fields.Add(grid[y + 1][x + 1]);
            }
            if (y - 1 >= 0)
            {
                field = grid[y - 1][x + 1];
                if (field != null && field.chessPieceHere != null && field.chessPieceHere.side != ChessSide.White)
                    fields.Add(grid[y - 1][x + 1]);
            }
        }
        else
        {
            field = grid[y][x - 1];
            if (field != null && field.chessPieceHere == null)
                fields.Add(grid[y][x - 1]);

            if (y + 1 < grid.Length)
            {
                field = grid[y + 1][x - 1];
                if (field != null && field.chessPieceHere != null && field.chessPieceHere.side != ChessSide.Black)
                    fields.Add(grid[y + 1][x - 1]);
            }
            if (y - 1 >= 0)
            {
                field = grid[y - 1][x - 1];
                if (field != null && field.chessPieceHere != null && field.chessPieceHere.side != ChessSide.Black)
                    fields.Add(grid[y - 1][x - 1]);
            }
        }

        return fields.ToArray();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingBehavior : Chessman
{
    public override bool [,] PossibleMove ()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        // Top side
        i = currentX - 1;
        j = currentY + 1;
        if (currentY != 7)
        {
            for(int k = 0; k < 3; k++)
            {
                if(i >= 0 || i < 8)
                {
                    c = ChessBoardController.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;    
            }
        }

        //down side
        i = currentX - 1;
        j = currentY - 1;
        if (currentY != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = ChessBoardController.Instance.Chessmans[i, j];
                    if (c == null)
                        r[i, j] = true;
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        //middle left
        if (currentX != 0)
        {
            c = ChessBoardController.Instance.Chessmans[currentX - 1, currentY];
            if (c == null)
                r[currentX - 1, currentY] = true;
            else if (isWhite != c.isWhite)
                r[currentX - 1, currentY] = true;
        }
        
        //middle Right
        if (currentX != 7)
        {
            c = ChessBoardController.Instance.Chessmans[currentX + 1, currentY];
            if (c == null)
                r[currentX + 1, currentY] = true;
            else if (isWhite != c.isWhite)
                r[currentX + 1, currentY] = true;
        }
        return r;
    }
}

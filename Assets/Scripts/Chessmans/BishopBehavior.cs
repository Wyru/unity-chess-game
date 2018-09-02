using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BishopBehavior : Chessman {

	public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8,8];
        Chessman c;
        int i, j;

        //up left
        for (i = currentX-1, j = currentY+1; i >= 0 && j < 8; i--, j++)
        {
            c = ChessBoardController.Instance.Chessmans[i, j];
            if(c == null)
                r[i,j] = true;
            else
            {
                if(isWhite != c.isWhite)
                    r[i,j] = true;
                break;
            }
            
        } 


        //up right
        for (i = currentX+1, j = currentY+1; i < 8 && j < 8; i++, j++)
        {
            c = ChessBoardController.Instance.Chessmans[i, j];
            if(c == null)
                r[i,j] = true;
            else
            {
                if(isWhite != c.isWhite)
                    r[i,j] = true;
                break;
            }
        }

        //down left
        for (i = currentX-1, j = currentY-1; i >=0 && j >= 0; i--, j--)
        {
            c = ChessBoardController.Instance.Chessmans[i, j];
            if(c == null)
                r[i,j] = true;
            else
            {
                if(isWhite != c.isWhite)
                    r[i,j] = true;
                break;
            }
        }  

        //down right
        for (i = currentX+1, j = currentY-1; i < 8 && j >= 0; i++, j--)
        {
            c = ChessBoardController.Instance.Chessmans[i, j];
            if(c == null)
                r[i,j] = true;
            else
            {
                if(isWhite != c.isWhite)
                    r[i,j] = true;
                break;
            }
        }  

        return r;
    }
}

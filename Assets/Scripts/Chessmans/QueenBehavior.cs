using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenBehavior : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        i = currentX;

        //right
        while (true)
        {
            i++;
            if (i > 7)
                break;
            c = ChessBoardController.Instace.Chessmans[i, currentY];
            if (c == null)
                r[i, currentY] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, currentY] = true;
                break;
            }
        }

        //left
        i = currentX;

        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = ChessBoardController.Instace.Chessmans[i, currentY];
            if (c == null)
                r[i, currentY] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, currentY] = true;

                break;
            }
        }

        //top
        i = currentY;

        while (true)
        {
            i++;
            if (i > 7)
                break;
            c = ChessBoardController.Instace.Chessmans[currentX, i];
            if (c == null)
                r[currentX, i] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[currentX, i] = true;

                break;
            }
        }

        //bootom
        i = currentY;

        while (true)
        {
            i--;
            if (i < 0)
                break;
            c = ChessBoardController.Instace.Chessmans[currentX, i];
            if (c == null)
                r[currentX, i] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[currentX, i] = true;

                break;
            }
        }

        //up left
        for (i = currentX - 1, j = currentY + 1; i >= 0 && j < 8; i--, j++)
        {
            c = ChessBoardController.Instace.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }

        }


        //up right
        for (i = currentX + 1, j = currentY + 1; i < 8 && j < 8; i++, j++)
        {
            c = ChessBoardController.Instace.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }

        //down left
        for (i = currentX - 1, j = currentY - 1; i >= 0 && j >= 0; i--, j--)
        {
            c = ChessBoardController.Instace.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }

        //down right
        for (i = currentX + 1, j = currentY - 1; i < 8 && j >= 0; i++, j--)
        {
            c = ChessBoardController.Instace.Chessmans[i, j];
            if (c == null)
                r[i, j] = true;
            else
            {
                if (isWhite != c.isWhite)
                    r[i, j] = true;
                break;
            }
        }

        return r;
    }
}

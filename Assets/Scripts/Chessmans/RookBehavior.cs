using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookBehavior : Chessman {

	public override bool[,] PossibleMove()
    {
        bool[,] r  = new bool[8,8];
        Chessman c;
        int i =  currentX;

        //right
        while (true)
        {
            i= i + 1;
            if(i > 7)
                break;
            c = ChessBoardController.Instace.Chessmans[i,currentY];
            if(c == null)
                r[i, currentY] = true;
            else{
                if(isWhite != c.isWhite)
                    r[i, currentY] = true;
                break;
            }
        }

        //left
        i =  currentX;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            c = ChessBoardController.Instace.Chessmans[i,currentY];
            if(c == null)
                r[i, currentY] = true;
            else{
                if(isWhite != c.isWhite)
                    r[i, currentY] = true;

                break;
            }
        }

        //top
        i =  currentY;

        while (true)
        {
            i++;
            if(i > 7)
                break;
            c = ChessBoardController.Instace.Chessmans[currentX,i];
            if(c == null)
                r[currentX, i] = true;
            else{
                if(isWhite != c.isWhite)
                    r[currentX, i] = true;

                break;
            }
        }

        //bootom
        i =  currentY;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            c = ChessBoardController.Instace.Chessmans[currentX,i];
            if(c == null)
                r[currentX, i] = true;
            else{
                if(isWhite != c.isWhite)
                    r[currentX, i] = true;

                break;
            }
        }
        return  r;
    }
}

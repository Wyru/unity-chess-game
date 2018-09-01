using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBehavior : Chessman {
	

    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8,8];
        //up lef
        knightMove(currentX-1, currentY+2, ref r);
        //up right
        knightMove(currentX+1, currentY+2, ref r);

        //left up
        knightMove(currentX-2, currentY+1, ref r);
        //left down
        knightMove(currentX-2, currentY-1, ref r);

        //right up
        knightMove(currentX+2, currentY+1, ref r);
        //right down
        knightMove(currentX+2, currentY-1, ref r);

        //down left
        knightMove(currentX-1, currentY-2, ref r);
        //down right
        knightMove(currentX+1, currentY-2, ref r);
        return r;
    }

    private void knightMove(int x, int y, ref bool[,] r)
    {
        Chessman c;
        if(x >= 0 && x < 8 && y >= 0 && y < 8){
            c = ChessBoardController.Instance.Chessmans[x,y];
            if(c == null)
                r[x,y] = true;
            else if(isWhite != c.isWhite)
                r[x,y] = true;
        }
    }

}

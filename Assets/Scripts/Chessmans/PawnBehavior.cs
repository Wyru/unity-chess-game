using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBehavior : Chessman {

	public override bool[,] PossibleMove()
    {
        bool[,] r  = new bool[8,8];
        
        Chessman aux, aux2;

        if(isWhite)
        {
            // move diagonally  left
            if(currentX != 0 && currentY != 7)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX-1, currentY+1];
                if(aux != null && !aux.isWhite)
                    r[currentX-1, currentY+1] =  true;
            }

            // move diagonally  right
            if(currentX != 7 && currentY != 7)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX+1, currentY+1];
                if(aux != null && !aux.isWhite)
                    r[currentX+1, currentY+1] =  true;
            }

            // move foward
            if(currentY != 7)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX, currentY+1];
                if(aux == null)
                    r[currentX, currentY+1] =  true;
            }

            // move special 
            if(currentY == 1)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX, currentY+1];
                aux2 = ChessBoardController.Instace.Chessmans[currentX, currentY+2];

                if(aux == null && aux2 == null)
                    r[currentX, currentY+2] =  true;
            }
        }
        else
        {
            // move diagonally  left
            if(currentX != 0 && currentY != 0)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX-1, currentY-1];
                if(aux != null && !aux.isWhite)
                    r[currentX-1, currentY-1] =  true;
            }

            // move diagonally  right
            if(currentX != 7 && currentY != 0)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX+1, currentY-1];
                if(aux != null && !aux.isWhite)
                    r[currentX+1, currentY-1] =  true;
            }

            // move foward
            if(currentY != 0)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX, currentY-1];
                if(aux == null)
                    r[currentX, currentY-1] =  true;
            }

            // move special 
            if(currentY == 6)
            {
                aux = ChessBoardController.Instace.Chessmans[currentX, currentY-1];
                aux2 = ChessBoardController.Instace.Chessmans[currentX, currentY-2];

                if(aux == null && aux2 == null)
                    r[currentX, currentY-2] =  true;
            }
        }
        return r;
    }
}

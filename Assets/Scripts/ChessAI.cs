using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessAI  {


    public int max_deep = 1;

    public int qtd_movimentos = 0;

    private enum ChessmansEnum
    {
        wking, wqueen, wbishop, wknight, wrook, wpawn, 
        bking, bqueen, bbishop, bknight, brook, bpawn 
    }

    private int[] chessmans_points =  new int[]{
        0, -900, -90, -30, -30, -50, -10,
        900, 90, 30, 30, 50, 10
    };

    public int[] StartIA()
    {
        int [,] board = GetCurrentBoard();
        qtd_movimentos = 0;
        return ChooseMove(board);
    }

    private int[,] GetCurrentBoard()
    {
        int[,] newChessboard = new int[8,8];
        Chessman[,] chessmans = ChessBoardController.Instance.Chessmans;
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                newChessboard[i,j] = ChessmanObjToEnum(chessmans[i,j]);
        
        return newChessboard;
    }

    private int ChessmanObjToEnum(Chessman c)
    {
        int aux = -1;

        if(c == null)
            return aux;

        if(c.GetType() == typeof(KingBehavior))
            aux = (int)ChessmansEnum.wking;
        else if(c.GetType() == typeof(QueenBehavior))
            aux = (int)ChessmansEnum.wqueen;
        else if(c.GetType() == typeof(BishopBehavior))
            aux = (int)ChessmansEnum.wbishop;
        else if(c.GetType() == typeof(KnightBehavior))
            aux = (int)ChessmansEnum.wknight;
        else if(c.GetType() == typeof(RookBehavior))
            aux = (int)ChessmansEnum.wrook;
        else if(c.GetType() == typeof(PawnBehavior))
            aux = (int)ChessmansEnum.wpawn;

        if(!c.isWhite)
            return aux+6;
        return aux;
    }  

    private int[] ChooseMove(int[,] board)
    {
        return MinMax(board, 1, true);
    }

    private int[] MinMax(int[,] board, int deep, bool iaTurn)
    {
        //x peça, y peça, x movimento, y movimento, peso movimento
        int[] best_move = new int[5]{-1,-1,-1,-1,iaTurn?-9000:9000};
            
        List<int[]> moves = new List<int[]>();

        
        //se profundidade máxima ou fim de jogo
        if(deep > max_deep)
        {
            best_move[4] = BoardToScore(board);
            return best_move;
        }
        
        //Para cada peça os possíveis movimentos
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                //pega os possíveis movimentos
                if(iaTurn && board[x,y] > 5){
                    GetChessmanMoves(board, x, y, iaTurn, moves);
                }
                else if(!iaTurn && board[x,y] < 6 && board[x,y] >-1){
                    GetChessmanMoves(board, x, y, iaTurn, moves);
                }
            }
        }
        deep++;
        foreach (int[] move in moves)
        {
            qtd_movimentos++;
            
            move[4] = MinMax(GenerateBoardForMove((int[,])board.Clone(),move[0],move[1],move[2],move[3]), deep,!iaTurn)[4];
            if(iaTurn)
            {
                if(move[4] > best_move[4])
                    best_move = move;
            }
            else
            {
                if(move[4] < best_move[4])
                    best_move = move;
            }
        }

        return best_move;
    }

    private void  GetChessmanMoves(int[,] board, int x, int y, bool iaTurn, List<int[]> moves)
    {

        if(board[x,y] == (int)ChessmansEnum.wking)
            PawnMoves(board, x, y, moves);
        else if(board[x,y] == (int)ChessmansEnum.wking || board[x,y] == (int)ChessmansEnum.bking)
            PawnMoves(board, x, y, moves);
        else if(board[x,y] == (int)ChessmansEnum.wqueen || board[x,y] == (int)ChessmansEnum.bqueen)
            KingMoves(board, x, y, moves);
        else if(board[x,y] == (int)ChessmansEnum.wbishop || board[x,y] == (int)ChessmansEnum.bbishop)
            BishopMoves(board, x, y, moves);
        else if(board[x,y] == (int)ChessmansEnum.wknight || board[x,y] == (int)ChessmansEnum.bknight)
            KnightMoves(board, x, y, moves);
        else if(board[x,y] == (int)ChessmansEnum.wrook || board[x,y] == (int)ChessmansEnum.brook)
            RookMoves(board, x, y, moves);
        else 
            PawnMoves(board, x, y, moves);
    }

    private int[,] GenerateBoardForMove(int[,] board ,int x, int y, int move_x, int move_y)
    {
        board[move_x, move_y] = board[x, y];
        board[x, y] = -1;
        return board;
    }

    private void PawnMoves(int[,] board, int x, int y,  List<int[]> moves)
    {
        int aux, aux2;

        // White 
        if(board[x, y] < 6)
        {

            // move diagonally  left
            if(x != 0 && y != 7)
            {
                aux = board[x-1, y+1];
                if(aux != -1 && aux > 5)
                    moves.Add(new int[]{x, y, x-1, y+1, 0});
            }

            // move diagonally  right
            if(x != 7 && y != 7)
            {
                aux = board[x+1, y+1];
                if(aux != -1 && aux > 5)
                    moves.Add(new int[]{x, y, x+1, y+1, 0});
            }

            // move foward
            if(y != 7)
            {
                aux = board[x, y+1];
                if(aux == -1)
                    moves.Add(new int[]{x, y, x ,y+1, 0});
            }

            // move special 
            if(y == 1)
            {
                aux = board[x, y+1];
                aux2 = board[x, y+2];

                if(aux == -1 && aux2 == -1)
                    moves.Add(new int[]{x, y, x ,y+2, 0});
            }

        }
        //black
        else
        {
            // move diagonally  left
            if(x != 0 && y != 0)
            {
                aux = board[x-1, y-1];
                if(aux != -1 && aux < 6)
                    moves.Add(new int[]{x, y, x-1,y-1, 0});
            }

            // move diagonally  right
            if(x != 7 && y != 0)
            {
                aux = board[x+1, y-1];
                if(aux != -1 && aux < 6)
                    moves.Add(new int[]{x, y, x+1,y-1, 0});
            }

            // move foward
            if(y != 0)
            {
                aux = board[x, y-1];
                if(aux == -1)
                    moves.Add(new int[]{x, y, x ,y-1, 0});
            }

            // move special 
            if(y == 6)
            {
                aux = board[x, y-1];
                aux2 = board[x, y-2];

                if(aux == -1 && aux2 == -1)
                    moves.Add(new int[]{x, y, x ,y-2, 0});
            }
        }
    }

    private void RookMoves(int[,] board, int x, int y,  List<int[]> moves)
    {
        //right
        int i =  x;
        int aux;

        while (true)
        {
            i = i + 1;
            if(i > 7)
                break;
            aux = board[i,y];
            if(aux == -1)
                moves.Add(new int[]{x, y, i, y, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, i, y, 0});
                break;
            }
        }

        //left
        i =  x;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            aux = board[i,y];
            if(aux == -1)
                moves.Add(new int[]{x, y, i, y, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, i, y, 0});
                break;
            }
        }

        //top
        i =  y;

        while (true)
        {
            i++;
            if(i > 7)
                break;
            aux = board[x,i];
            if(aux == -1)
                moves.Add(new int[]{x, y, x, i, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, x, i, 0});
                break;
            }
        }

        //bootom
        i =  y;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            aux = board[x,i];
            if(aux == -1)
                moves.Add(new int[]{x, y, x, i, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, x, i, 0});
                break;
            }
        }
    }

    public void KnightMoves(int[,] board, int x, int y,  List<int[]> moves)
    {
        //up lef
        knightMoveAux(x, y, x-1, y+2, board, moves);
        //up right
        knightMoveAux(x, y, x+1, y+2, board, moves);

        //left up
        knightMoveAux(x, y, x-2, y+1, board, moves);
        //left down
        knightMoveAux(x, y, x-2, y-1, board, moves);

        //right up
        knightMoveAux(x, y, x+2, y+1, board, moves);
        //right down
        knightMoveAux(x, y, x+2, y-1, board, moves);

        //down left
        knightMoveAux(x, y, x-1, y-2, board, moves);
        //down right
        knightMoveAux(x, y, x+1, y-2, board, moves);
    }

    private void knightMoveAux(int x, int y, int x_move, int y_move, int [,] board, List<int[]> moves)
    {
        int c;
        if(x_move >= 0 && x_move < 8 && y_move >= 0 && y_move < 8){
            c = board[x_move,y_move];
            if(c == -1)
                moves.Add(new int[]{x, y, x_move, y_move, 0});
            else if(board[x, y] < 6 && board[x_move,y_move] >= 6 || board[x, y] >= 6 && board[x_move, y_move] < 6)
                moves.Add(new int[]{x, y, x_move, y_move, 0});
        }
    }

    private void BishopMoves(int[,] board, int x, int y, List<int[]> moves)
    {
        int i, j, c;

        //up left
        for (i = x-1, j = y+1; i >= 0 && j < 8; i--, j++)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
            
        } 


        //up right
        for (i = x+1, j = y+1; i < 8 && j < 8; i++, j++)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }

        //down left
        for (i = x-1, j = y-1; i >=0 && j >= 0; i--, j--)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }  

        //down right
        for (i = x+1, j = y-1; i < 8 && j >= 0; i++, j--)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }  

    }

    private void KingMoves(int [,] board, int x, int y,  List<int[]> moves)
    {
        int i, j, c;

        // Top side
        i = x - 1;
        j = y + 1;
        if (y != 7)
        {
            for(int k = 0; k < 3; k++)
            {
                if(i >= 0 || i < 8)
                {
                    c = board[i,j];
                    if (c == -1)
                        moves.Add(new int[]{x, y, i, j, 0});
                        
                    if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                        moves.Add(new int[]{x, y, i, j, 0});
                }
                i++;    
            }
        }

        //down side
        i = x - 1;
        j = y - 1;
        if (y != 0)
        {
            for (int k = 0; k < 3; k++)
            {
                if (i >= 0 || i < 8)
                {
                    c = board[i,j];
                    if (c == -1)
                        moves.Add(new int[]{x, y, i, j, 0});
                    if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                        moves.Add(new int[]{x, y, i, j, 0});
                }
                i++;
            }
        }

        //middle left
        if (x != 0)
        {
            c = board[x-1,y];
            if (c == -1)
                moves.Add(new int[]{x, y, x-1, y, 0});
            else if(board[x, y] < 6 && board[x-1,y] >= 6 || board[x, y] >= 6 && board[x-1, y] < 6)
                moves.Add(new int[]{x, y, x-1, y, 0});
        }
        
        //middle Right
        if (x != 7)
        {
            c = board[x-1,y];
            if (c == -1)
                moves.Add(new int[]{x, y, x+1, y, 0});
            else if(board[x, y] < 6 && board[x+1,y] >= 6 || board[x, y] >= 6 && board[x+1, y] < 6)
                moves.Add(new int[]{x, y, x+1, y, 0});
        }
    }

    private void QueenMoves(int [,] board, int x, int y,  List<int[]> moves)
    {
        int i, j, c;

        //up left
        for (i = x-1, j = y+1; i >= 0 && j < 8; i--, j++)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
            
        } 


        //up right
        for (i = x+1, j = y+1; i < 8 && j < 8; i++, j++)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }

        //down left
        for (i = x-1, j = y-1; i >=0 && j >= 0; i--, j--)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }  

        //down right
        for (i = x+1, j = y-1; i < 8 && j >= 0; i++, j--)
        {
            c = board[x,y];
            if(c == -1)
                moves.Add(new int[]{x, y, i, j, 0});
            else
            {
                if(board[x, y] < 6 && board[i,j] >= 6 || board[x, y] >= 6 && board[i, j] < 6)
                    moves.Add(new int[]{x, y, i, j, 0});
                break;
            }
        }  

        //right
        i =  x;
        int aux;

        while (true)
        {
            i = i + 1;
            if(i > 7)
                break;
            aux = board[i,y];
            if(aux == -1)
                moves.Add(new int[]{x, y, i, y, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, i, y, 0});
                break;
            }
        }

        //left
        i =  x;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            aux = board[i,y];
            if(aux == -1)
                moves.Add(new int[]{x, y, i, y, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, i, y, 0});
                break;
            }
        }

        //top
        i =  y;

        while (true)
        {
            i++;
            if(i > 7)
                break;
            aux = board[x,i];
            if(aux == -1)
                moves.Add(new int[]{x, y, x, i, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, x, i, 0});
                break;
            }
        }

        //bootom
        i =  y;

        while (true)
        {
            i--;
            if(i < 0)
                break;
            aux = board[x,i];
            if(aux == -1)
                moves.Add(new int[]{x, y, x, i, 0});
            else{
                if(board[x, y] < 6 && board[i,x] >= 6 || board[x, y] >= 6 && board[i,x] < 6)
                    moves.Add(new int[]{x, y, x, i, 0});
                break;
            }
        }
    }
    private int BoardToScore(int[,] board)
    {
        int score = 0;
        for (int x = 0; x < 8; x++)
            for (int y = 0; y < 8; y++)
                score+=chessmans_points[board[x,y]+1];
        return score;
    }

}
using UnityEngine;
using AI;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public class Chessman
    {
        public Vector2Int position{get;set;}

        public enum Color
        {
            white,
            black
        }

        public enum Type
        {
            king,
            queen,
            bishop,
            knight,
            rook,
            pawn
        }
        
        public Color color;
        public Type type;


        static List<float[,]> evalWhite = ChessmanEval.getEvalWhite();
        static List<float[,]> evalBlack = ChessmanEval.getEvalBlack();

        static int[] score = new int[]{
            900,
            90,
            30,
            30,
            50,
            10
        };

        public Chessman(Type type, Color color, Vector2Int position)
        {
            this.type  = type;
            this.color  = color;
            this.position = position;
        }


        public float getScore()
        {
            if(color == Color.white)
                return score[(int)type] + (evalWhite[((int)type)][position.x, position.y]);
            return (score[(int)type] + (evalBlack[((int)type)][position.x, position.y]));
        }


        public List<Move> getMoves(ref List<Move> moves)
        {
            if(type == Type.pawn)
            {
                Chessman aux, aux2;

                // White 
                if(color == Color.white)
                {

                    // move diagonally  left
                    if(position.x != 0 && position.y != 7)
                    {
                        aux = Board.Instance.board[position.x-1, position.y+1];
                        if(aux != null && aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x-1,position.y+1),0));
                    }

                    // move diagonally  right
                    if(position.x != 7 && position.y != 7)
                    {
                        aux = Board.Instance.board[position.x+1, position.y+1];
                        if(aux != null && aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x+1,position.y+1),0));
                    }

                    // move foward
                    if(position.y != 7)
                    {
                        aux = Board.Instance.board[position.x, position.y+1];
                        if(aux == null)
                            moves.Add(new Move(position, new Vector2Int(position.x,position.y+1),0));
                    }

                    // move special 
                    if(position.y == 1)
                    {
                        aux = Board.Instance.board[position.x, position.y+1];
                        aux2 = Board.Instance.board[position.x, position.y+2];

                        if(aux == null && aux2 == null)
                            moves.Add(new Move(position, new Vector2Int(position.x,position.y+2),0));
                    }

                }
                //black
                else
                {
                    // move diagonally  left
                    if(position.x != 0 && position.y != 0)
                    {
                        aux = Board.Instance.board[position.x-1, position.y-1];
                        if(aux != null && aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x-1,position.y-1),0));
                    }

                    // move diagonally  right
                    if(position.x != 7 && position.y != 0)
                    {
                        aux = Board.Instance.board[position.x+1, position.y-1];
                        if(aux != null && aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x+1,position.y-1),0));
                    }

                    // move foward
                    if(position.y != 0)
                    {
                        aux = Board.Instance.board[position.x, position.y-1];
                        if(aux == null)
                            moves.Add(new Move(position, new Vector2Int(position.x,position.y-1),0));
                    }

                    // move special 
                    if(position.y == 6)
                    {
                        aux = Board.Instance.board[position.x, position.y-1];
                        aux2 = Board.Instance.board[position.x, position.y-2];

                        if(aux == null && aux2 == null)
                            moves.Add(new Move(position, new Vector2Int(position.x,position.y-2),0));
                    }
                }
            }
            else if(type == Type.rook)
            {
                //right
                int i =  position.x;
                Chessman aux;

                while (true)
                {
                    i = i + 1;
                    if(i > 7)
                        break;
                    aux = Board.Instance.board[i,position.y];
                    if(aux == null)
                        moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                    else{
                        if(aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                        break;
                    }
                }

                //left
                i =  position.x;

                while (true)
                {
                    i--;
                    if(i < 0)
                        break;
                    aux = Board.Instance.board[i,position.y];
                    if(aux == null)
                        moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                    else{
                        if(aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                        break;
                    }
                }

                //top
                i =  position.y;

                while (true)
                {
                    i++;
                    if(i > 7)
                        break;
                    aux = Board.Instance.board[position.x,i];
                    if(aux == null)
                        moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                    else{
                        if(aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                        break;
                    }
                }

                //bootom
                i =  position.y;

                while (true)
                {
                    i--;
                    if(i < 0)
                        break;
                    aux = Board.Instance.board[position.x,i];
                    if(aux == null)
                        moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                    else{
                        if(aux.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                        break;
                    }
                }
            }
            else if(type == Type.knight)
            {
                Chessman c;
                int[,] possibleMoves = new int[,]{
                    {position.x-1, position.y+2},
                    {position.x+1, position.y+2},
                    {position.x-2, position.y+1},
                    {position.x-2, position.y-1},
                    {position.x+2, position.y+1},
                    {position.x+2, position.y-1},
                    {position.x-1, position.y-2},
                    {position.x+1, position.y-2},
                };

                for (int i = 0; i < 8; i++)
                {
                    if(possibleMoves[i,0] >= 0 && possibleMoves[i,0] < 8 && possibleMoves[i,1] >= 0 && possibleMoves[i,1] < 8){
                        c = Board.Instance.board[possibleMoves[i,0],possibleMoves[i,1]];
                        if(c == null)
                            moves.Add(new Move(position, new Vector2Int(possibleMoves[i,0],possibleMoves[i,1]),0));
                        else if(color != c.color)
                            moves.Add(new Move(position, new Vector2Int(possibleMoves[i,0],possibleMoves[i,1]),0));
                    }
                }
            }
            else if(type == Type.bishop)
            {
                int i, j;
                Chessman c;
                //up left
                for (i = position.x-1, j = position.y+1; i >= 0 && j < 8; i--, j++)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 


                //up right
                for (i = position.x+1, j = position.y+1; i < 8 && j < 8; i++, j++)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 

                //down left
                for (i = position.x-1, j = position.y-1; i >= 0 && j >= 0; i--, j--)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 

                //down right
                for (i = position.x+1, j = position.y-1; i < 8 && j >= 0; i++, j--)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 
            }
            else if(type == Type.queen)
            {
                int i, j;
                Chessman c;
                //up left
                for (i = position.x-1, j = position.y+1; i >= 0 && j < 8; i--, j++)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 


                //up right
                for (i = position.x+1, j = position.y+1; i < 8 && j < 8; i++, j++)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 

                
                //down left
                for (i = position.x-1, j = position.y-1; i >= 0 && j >=0; i--, j--)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                } 

                //down right
                for (i = position.x+1, j = position.y-1; i < 8 && j >=0; i++, j--)
                {
                    c = Board.Instance.board[i,j];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,j),0));
                    else
                    {
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,j),0));
                        break;
                    }
                }

                //right
                i =  position.x;

                while (true)
                {
                    i = i + 1;
                    if(i > 7)
                        break;
                    c = Board.Instance.board[i,position.y];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                    else{
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                        break;
                    }
                }

                //left
                i =  position.x;

                while (true)
                {
                    i--;
                    if(i < 0)
                        break;
                    c = Board.Instance.board[i,position.y];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                    else{
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(i,position.y),0));
                        break;
                    }
                }

                //top
                i =  position.y;

                while (true)
                {
                    i++;
                    if(i > 7)
                        break;
                    c = Board.Instance.board[position.x,i];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                    else{
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                        break;
                    }
                }

                //bootom
                i =  position.y;

                while (true)
                {
                    i--;
                    if(i < 0)
                        break;
                    c = Board.Instance.board[position.x,i];
                    if(c == null)
                        moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                    else{
                        if(c.color != color)
                            moves.Add(new Move(position, new Vector2Int(position.x, i),0));
                        break;
                    }
                }
            }
            else if(type == Type.king)
            {
                int i, j;
                Chessman c;
                // Top side
                i = position.x - 1;
                j = position.y + 1;
                if (position.y < 7)
                {
                    for(int k = 0; k < 3; k++)
                    {
                        if(i >= 0 && i < 8)
                        {
                            c = Board.Instance.board[i,j];
                            if (c == null)
                                moves.Add(new Move(position, new Vector2Int(i,j),0));
                            else if(c.color != color)
                                moves.Add(new Move(position, new Vector2Int(i,j),0));
                        }
                        i++;    
                    }
                }

                //down side
                i = position.x - 1;
                j = position.y - 1;
                if (position.y > 0)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (i >= 0 && i < 8)
                        {
                            c = Board.Instance.board[i,j];
                            if (c == null)
                                moves.Add(new Move(position, new Vector2Int(i,j),0));
                            else if(c.color != color)
                                moves.Add(new Move(position, new Vector2Int(i,j),0));
                        }
                        i++;
                    }
                }

                //middle left
                if (position.x > 0)
                {
                    c = Board.Instance.board[position.x-1,position.y];
                    if (c == null)
                        moves.Add(new Move(position, new Vector2Int(position.x-1,position.y),0));
                    else if(c.color != color)
                        moves.Add(new Move(position, new Vector2Int(position.x-1,position.y),0));
                }
                
                //middle Right
                if (position.x < 7)
                {
                    c = Board.Instance.board[position.x+1,position.y];
                    if (c == null)
                        moves.Add(new Move(position, new Vector2Int(position.x+1,position.y),0));
                    else if(c.color != color)
                        moves.Add(new Move(position, new Vector2Int(position.x+1,position.y),0));
                }
            }

            return moves;
        }

    }
}
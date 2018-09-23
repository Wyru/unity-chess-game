using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
namespace AI
{
    class Board
    {
        
        public static Board Instance;

        public AI.Chessman[,] board;
        List<AI.Chessman> whiteChessmans;
        List<AI.Chessman> blackChessmans;
        Stack<AI.Chessman> killedChessmans;
        Stack<Move> movehistory;
        bool isWhiteTurn;

        public Board(bool isWhiteTurn)
        {
            board = new AI.Chessman[8,8];
            whiteChessmans = new List<Chessman>();
            blackChessmans = new List<Chessman>();
            movehistory =  new Stack<Move>();
            killedChessmans =  new Stack<Chessman>();
            this.isWhiteTurn = isWhiteTurn;
            Game.Chessman[,] chessmans = ChessBoardController.Instance.Chessmans;
            AI.Chessman.Type type;

            for (int x = 0; x < 8; x++)
                for (int y = 0; y < 8; y++){
                    if(chessmans[x,y] == null){
                        board[x,y] = null;
                        continue;
                    }

                    if(chessmans[x,y].GetType() == typeof(KingBehavior))
                        type = AI.Chessman.Type.king;
                    
                    else if(chessmans[x,y].GetType() == typeof(QueenBehavior))
                        type = AI.Chessman.Type.queen;
                    
                    else if(chessmans[x,y].GetType() == typeof(BishopBehavior))
                        type = AI.Chessman.Type.bishop;
                    
                    else if(chessmans[x,y].GetType() == typeof(KnightBehavior))
                        type = AI.Chessman.Type.knight;
                    
                    else if(chessmans[x,y].GetType() == typeof(RookBehavior))
                        type = AI.Chessman.Type.rook;

                    else
                        type = AI.Chessman.Type.pawn;
                    
                    board[x,y] = new AI.Chessman(type, chessmans[x,y].isWhite?AI.Chessman.Color.white:AI.Chessman.Color.black, new Vector2Int(x,y));
                    if(chessmans[x,y].isWhite)
                        whiteChessmans.Add(board[x,y]);
                    else
                        blackChessmans.Add(board[x,y]);
                    
                }
                    
            Instance =  this;
        }

        public void doMove(Move move)
        {
            movehistory.Push(move);
            if(board[move.destiny.x, move.destiny.y] != null){
                if(board[move.destiny.x, move.destiny.y].color == Chessman.Color.white)
                    whiteChessmans.Remove(board[move.destiny.x, move.destiny.y]);
                else
                    blackChessmans.Remove(board[move.destiny.x, move.destiny.y]);
            }
            killedChessmans.Push(board[move.destiny.x, move.destiny.y]);
            board[move.destiny.x, move.destiny.y] = board[move.origin.x, move.origin.y];
            board[move.origin.x, move.origin.y] = null;
            if(isWhiteTurn)
            {
                foreach (Chessman c in whiteChessmans)
                {
                    if(c.position.x == move.origin.x && c.position.y == move.origin.y)
                    {
                        c.position = move.destiny;
                    }
                }
            }
            else
            {
                foreach (Chessman c in blackChessmans)
                {
                    if(c.position.x == move.origin.x && c.position.y == move.origin.y)
                    {
                        c.position = move.destiny;
                    }
                }
            }
            isWhiteTurn = !isWhiteTurn;
        }

        public void undoMove()
        {
            Move move = movehistory.Pop();
            board[move.origin.x, move.origin.y] = board[move.destiny.x, move.destiny.y];
            board[move.destiny.x, move.destiny.y] = killedChessmans.Pop();
            if(board[move.destiny.x, move.destiny.y] != null){
                if(board[move.destiny.x, move.destiny.y].color == Chessman.Color.white)
                    whiteChessmans.Add(board[move.destiny.x, move.destiny.y]);
                else
                    blackChessmans.Add(board[move.destiny.x, move.destiny.y]);
            }
            if(isWhiteTurn)
            {
                foreach (Chessman c in whiteChessmans)
                {
                    if(c.position.x == move.destiny.x && c.position.y == move.destiny.y)
                    {
                        c.position = move.origin;
                    }
                }
            }
            else
            {
                foreach (Chessman c in blackChessmans)
                {
                    if(c.position.x == move.destiny.x && c.position.y == move.destiny.y)
                    {
                        c.position = move.origin;
                    }
                }
            }
            isWhiteTurn = !isWhiteTurn;
        }

        public float getScore()
        {
            float score = 0;
            foreach (Chessman c in whiteChessmans)
                score -= c.getScore();
            foreach (Chessman c in blackChessmans)
                score += c.getScore();

            return score;
        }

        public List<Move> getWhiteMoves()
        {
            List<Move> moves =  new List<Move>();
            foreach (Chessman chessman in whiteChessmans)
            {
                chessman.getMoves(ref moves);
            }

            return moves;
        }

        public List<Move> getBlackMoves()
        {
            List<Move> moves =  new List<Move>();
            foreach (Chessman chessman in blackChessmans)
            {
                chessman.getMoves(ref moves);
            }

            return moves;
        }

        public List<Move> getMoves()
        {
            if(isWhiteTurn)
                return getWhiteMoves();
            return getBlackMoves();
        }

    }
}
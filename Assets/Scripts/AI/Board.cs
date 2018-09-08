using System.Collections;
using System.Collections.Generic;
using Game;
namespace AI
{
    class Board
    {
        
        AI.Chessman[,] board;
        List<AI.Chessman> whiteChessmans;
        List<AI.Chessman> blackChessmans;

        bool isWhiteTurn;

        public Board(bool isWhiteTurn)
        {
            board = new AI.Chessman[8,8];
            whiteChessmans = new List<Chessman>();
            blackChessmans = new List<Chessman>();
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
                    

                    board[x,y] = new AI.Chessman(type, chessmans[x,y].isWhite?AI.Chessman.Color.white:AI.Chessman.Color.black, new UnityEngine.Vector2(x,y));
                    if(chessmans[x,y].isWhite)
                        whiteChessmans.Add(board[x,y]);
                    else
                        whiteChessmans.Add(board[x,y]);
                    
                }
                    

        }

        public void doMove(Move move)
        {

        }

        public void undoMove(Move move)
        {

        }

        public float getScore()
        {

            return 1;
        }

        public List<Move> getWhiteMoves()
        {

        }

        public List<Move> getBlackMoves()
        {

        }

        public List<Move> getMoves()
        {
            if(isWhiteTurn)
                return getWhiteMoves();
            return getBlackMoves();
        }

    }
}
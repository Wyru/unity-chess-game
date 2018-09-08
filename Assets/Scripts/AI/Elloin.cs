using Unity.Collections;

namespace AI{
    public class Elloin
    {
        Board board;
        bool isWhite;
        int maxDeep;

        public Elloin(bool isWhite, int deep)
        {
            this.isWhite = isWhite;
            this.maxDeep = deep;
        }

        public Move chooseBestMove()
        {
            board =  new Board(this.isWhite);
            Move bestMove = minMax(maxDeep,-99999, 99999,true);
        }


        private Move minMax(int deep, float alpha, float beta, bool maximizingPlayer)
        {
            Move bestMove = new Move();

            if(deep == 0)
            {
                bestMove.score = board.getScore();
                return bestMove;
            }
            if(maximizingPlayer)
            {
                bestMove.score = -9999999;

                foreach (Move move in board.getMoves())
                {
                    board.doMove(move);
                    move.score = minMax(deep-1,alpha, beta, false).score;
                    board.undoMove(move);

                    if(bestMove.score < move.score)
                        bestMove = move;

                    if(alpha < move.score)
                        alpha = move.score;

                    if(beta <= alpha)
                        break;
                }
                return bestMove;
            }
            else
            {
                bestMove.score = +9999999;

                foreach (Move move in board.getMoves())
                {
                    board.doMove(move);
                    move.score = minMax(deep-1,alpha, beta, true).score;
                    board.undoMove(move);
                    if(bestMove.score > move.score){
                        bestMove = move;
                    }

                    if(beta > move.score)
                        beta = move.score;

                    if(beta <= alpha)
                        break;
                }
                    return bestMove;
            }
        }
        
    }
}


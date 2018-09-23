using System.Collections;
using System.Collections.Generic;

namespace AI
{
    class ChessmanEval
    {
        public static float[,] pawnEvalWhite = new float[,]{
            {0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f},
            {5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f},
            {1.0f,  1.0f,  2.0f,  3.0f,  3.0f,  2.0f,  1.0f,  1.0f},
            {0.5f,  0.5f,  1.0f,  2.5f,  2.5f,  1.0f,  0.5f,  0.5f},
            {0.0f,  0.0f,  0.0f,  2.0f,  2.0f,  0.0f,  0.0f,  0.0f},
            {0.5f, -0.5f, -1.0f,  0.0f,  0.0f, -1.0f, -0.5f,  0.5f},
            {0.5f,  1.0f, 1.0f,  -2.0f, -2.0f,  1.0f,  1.0f,  0.5f},
            {0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f}
        };
        
        public static float[,] pawnEvalBlack = new float[,]{
            {0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f},
            {0.5f,  1.0f, 1.0f,  -2.0f, -2.0f,  1.0f,  1.0f,  0.5f},
            {0.5f, -0.5f, -1.0f,  0.0f,  0.0f, -1.0f, -0.5f,  0.5f},
            {0.0f,  0.0f,  0.0f,  2.0f,  2.0f,  0.0f,  0.0f,  0.0f},
            {0.5f,  0.5f,  1.0f,  2.5f,  2.5f,  1.0f,  0.5f,  0.5f},
            {1.0f,  1.0f,  2.0f,  3.0f,  3.0f,  2.0f,  1.0f,  1.0f},
            {5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f,  5.0f},
            {0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f},
        };


        public static float[,] rookEvalWhite = new float[,]{
            { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f},
            { 0.5f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            { 0.0f,   0.0f, 0.0f,  0.5f,  0.5f,  0.0f,  0.0f,  0.0f}
        };
        
        public static float[,] rookEvalBlack = new float[,]{
            { 0.0f,   0.0f, 0.0f,  0.5f,  0.5f,  0.0f,  0.0f,  0.0f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -0.5f},
            { 0.5f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.5f},
            { 0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f}
        };

        public static float[,] knightEvalWhite = new float[,]{
            {-5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f},
            {-4.0f, -2.0f,  0.0f,  0.0f,  0.0f,  0.0f, -2.0f, -4.0f},
            {-3.0f,  0.0f,  1.0f,  1.5f,  1.5f,  1.0f,  0.0f, -3.0f},
            {-3.0f,  0.5f,  1.5f,  2.0f,  2.0f,  1.5f,  0.5f, -3.0f},
            {-3.0f,  0.0f,  1.5f,  2.0f,  2.0f,  1.5f,  0.0f, -3.0f},
            {-3.0f,  0.5f,  1.0f,  1.5f,  1.5f,  1.0f,  0.5f, -3.0f},
            {-4.0f, -2.0f,  0.0f,  0.5f,  0.5f,  0.0f, -2.0f, -4.0f},
            {-5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f}
        };
        
        public static float[,] knightEvalBlack = new float[,]{
            {-5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f},
            {-4.0f, -2.0f,  0.0f,  0.5f,  0.5f,  0.0f, -2.0f, -4.0f},
            {-3.0f,  0.5f,  1.0f,  1.5f,  1.5f,  1.0f,  0.5f, -3.0f},
            {-3.0f,  0.0f,  1.5f,  2.0f,  2.0f,  1.5f,  0.0f, -3.0f},
            {-3.0f,  0.5f,  1.5f,  2.0f,  2.0f,  1.5f,  0.5f, -3.0f},
            {-3.0f,  0.0f,  1.0f,  1.5f,  1.5f,  1.0f,  0.0f, -3.0f},
            {-4.0f, -2.0f,  0.0f,  0.0f,  0.0f,  0.0f, -2.0f, -4.0f},
            {-5.0f, -4.0f, -3.0f, -3.0f, -3.0f, -3.0f, -4.0f, -5.0f},
        };


        public static float[,] bishopEvalWhite = new float[,]{
            {-2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f},
            {-1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-1.0f,  0.0f,  0.5f,  1.0f,  1.0f,  0.5f,  0.0f, -1.0f},
            {-1.0f,  0.5f,  0.5f,  1.0f,  1.0f,  0.5f,  0.5f, -1.0f},
            {-1.0f,  0.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.0f, -1.0f},
            {-1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f, -1.0f},
            {-1.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.5f, -1.0f},
            {-2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f}
        };
        
        public static float[,] bishopEvalBlack = new float[,]{
            {-2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f},
            {-1.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f,  0.5f, -1.0f},
            {-1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f,  1.0f, -1.0f},
            {-1.0f,  0.0f,  1.0f,  1.0f,  1.0f,  1.0f,  0.0f, -1.0f},
            {-1.0f,  0.5f,  0.5f,  1.0f,  1.0f,  0.5f,  0.5f, -1.0f},
            {-1.0f,  0.0f,  0.5f,  1.0f,  1.0f,  0.5f,  0.0f, -1.0f},
            {-1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-2.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -1.0f, -2.0f}
        };

        public static float[,] kingEvalWhite = new float[,]{
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-2.0f, -3.0f, -3.0f, -4.0f, -4.0f, -3.0f, -3.0f, -2.0f},
            {-1.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -1.0f},
            { 2.0f,  2.0f,  0.0f,  0.0f,  0.0f,  0.0f,  2.0f,  2.0f},
            { 2.0f,  3.0f,  1.0f,  0.0f,  0.0f,  1.0f,  3.0f,  2.0f}
        };
        
        public static float[,] kingEvalBlack = new float[,]{
            { 2.0f,  3.0f,  1.0f,  0.0f,  0.0f,  1.0f,  3.0f,  2.0f},
            { 2.0f,  2.0f,  0.0f,  0.0f,  0.0f,  0.0f,  2.0f,  2.0f},
            {-1.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -2.0f, -1.0f},
            {-2.0f, -3.0f, -3.0f, -4.0f, -4.0f, -3.0f, -3.0f, -2.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f},
            {-3.0f, -4.0f, -4.0f, -5.0f, -5.0f, -4.0f, -4.0f, -3.0f}
        };

        public static float[,] queenEvalWhite = new float[,]{
            {-2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f},
            {-1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-1.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f},
            {-0.5f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f},
            { 0.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f},
            {-1.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f},
            {-1.0f,  0.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f}
        };
        
        public static float[,] queenEvalBlack = new float[,]{
            {-2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f},
            {-1.0f,  0.0f,  0.5f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-1.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f},
            { 0.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f},
            {-0.5f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -0.5f},
            {-1.0f,  0.0f,  0.5f,  0.5f,  0.5f,  0.5f,  0.0f, -1.0f},
            {-1.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f,  0.0f, -1.0f},
            {-2.0f, -1.0f, -1.0f, -0.5f, -0.5f, -1.0f, -1.0f, -2.0f}
        };


        public static  List<float[,]> getEvalWhite()
        {
            List<float[,]> list = new List<float[,]>();

            list.Add(kingEvalWhite);
            list.Add(queenEvalWhite);
            list.Add(bishopEvalWhite);
            list.Add(knightEvalWhite);
            list.Add(rookEvalWhite);
            list.Add(pawnEvalWhite);
            
            return list;
        }


        public static  List<float[,]> getEvalBlack()
        {
            List<float[,]> list = new List<float[,]>();

            list.Add(kingEvalBlack);
            list.Add(queenEvalBlack);
            list.Add(bishopEvalBlack);
            list.Add(knightEvalBlack);
            list.Add(rookEvalBlack);
            list.Add(pawnEvalBlack);
            
            return list;
        }
    }
}
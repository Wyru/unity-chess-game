using UnityEngine;
using AI;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    class Chessman
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
                return score[(int)type] + (evalWhite[((int)type)][(int)position.x, (int)position.y]);
            return (score[(int)type] + (evalWhite[((int)type)][(int)position.x, (int)position.y]))*-1;
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AI
{
    public class Move 
    {
        public float score;
        public Vector2Int origin;
        public Vector2Int destiny;

        public Move()
        {
            this.origin = new Vector2Int(0,0);
            this.destiny = new Vector2Int(0,0);
        }

        public Move(Vector2Int origin, Vector2Int destiny, float score)
        {
            this.origin = origin;
            this.destiny = destiny;
            this.score = score;
        }
    }
}


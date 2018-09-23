using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
	public abstract class Chessman : MonoBehaviour {

		public int currentX{set;get;}
		public int currentY{set;get;}
		public bool isWhite;

		public void setPostion(int x, int y)
		{
			this.currentX = x;
			this.currentY = y;
		}

		public virtual bool[,] PossibleMove()
		{
			return new bool[8,8];
		}		
	}

}

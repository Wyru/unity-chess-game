﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardController : MonoBehaviour {

	private const float TILE_SIZE = 1.0F;
	private const float TILE_OFFSET = .5f;

	private float selectionX = -1;
	private float selectionY = -1;

	public List<GameObject> chessPieces;
	private List<GameObject> activeChessPieces;

	private Quaternion orientation  = Quaternion.Euler(-90,180,0);
	// Use this for initialization
	void Start () 
	{
		SpawAllPieces();
	}
	
	// Update is called once per frame
	void Update () 
	{
		DrawBoard();
		UpdateSelection();
	}

	void DrawBoard()
	{
		Vector3 widthLine = Vector3.right*8;
		Vector3 heightLine = Vector3.forward*8;

		for (int i = 0; i <= 8; i++)
		{
			Vector3 start = Vector3.forward*i;
			Debug.DrawLine(start, start+widthLine, Color.blue);
			for (int j = 0; j <= 8; j++)
			{
				start = Vector3.right*i;
				Debug.DrawLine(start, start+heightLine, Color.blue);
			}
		}

		if(selectionX >=0 && selectionY >=0){
			Debug.DrawLine(
				Vector3.forward*selectionY + Vector3.right*selectionX,
				Vector3.forward*(selectionY+1) + Vector3.right*(selectionX+1),
				Color.green
			);

			Debug.DrawLine(
				Vector3.forward*(selectionY+1) + Vector3.right*selectionX,
				Vector3.forward*selectionY + Vector3.right*(selectionX+1),
				Color.green
			);
		}
	}

	private void UpdateSelection()
	{
		if(!Camera.main)
			return;
		
		RaycastHit hit;

		if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("chessboard"))){
			selectionX = (int) hit.point.x;
			selectionY = (int) hit.point.z;

		}
		else{
			selectionX = -1;
			selectionY = -1;
		}
	} 

	private void SpawnPiece(int index, Vector3 position)
	{
		GameObject go = Instantiate(chessPieces[index],position,orientation) as GameObject;
		go.transform.SetParent(this.transform);
		activeChessPieces.Add(go);
	}

	private void SpawAllPieces()
	{
		activeChessPieces =  new List<GameObject>();
		//white pieces
		
		//king
		SpawnPiece(0, GetTileCenter(3,0));
		//Queen
		SpawnPiece(1, GetTileCenter(4,0));
		//bishps
		SpawnPiece(2, GetTileCenter(2,0));
		SpawnPiece(2, GetTileCenter(5,0));
		//kights
		SpawnPiece(3, GetTileCenter(1,0));
		SpawnPiece(3, GetTileCenter(6,0));
		//Rook
		SpawnPiece(4, GetTileCenter(0,0));
		SpawnPiece(4, GetTileCenter(7,0));
		for (int i = 0; i < 8; i++)
			SpawnPiece(5, GetTileCenter(i,1));
		
		//Black
		//king
		SpawnPiece(6, GetTileCenter(4,7));
		//Queen
		SpawnPiece(7, GetTileCenter(3,7));
		//bishps
		SpawnPiece(8, GetTileCenter(2,7));
		SpawnPiece(8, GetTileCenter(5,7));
		//kights
		SpawnPiece(9, GetTileCenter(1,7));
		SpawnPiece(9, GetTileCenter(6,7));
		//Rook
		SpawnPiece(10, GetTileCenter(0,7));
		SpawnPiece(10, GetTileCenter(7,7));
		for (int i = 0; i < 8; i++)
			SpawnPiece(11, GetTileCenter(i,6));
	}

	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE*x)+TILE_OFFSET;
		origin.z += (TILE_SIZE*y)+TILE_OFFSET;

		return origin;
	}
}

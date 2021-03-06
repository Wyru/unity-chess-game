﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour {

	public static  BoardHighlights Instance {get;set;}

	public GameObject hightlightPrefab;

	public List<GameObject> hightlights;


	private void Start() 
	{
		Instance = this;
		hightlights = new List<GameObject> ();
	}

	private GameObject GetHighlightObject()
	{
		GameObject go = hightlights.Find(g => !g.activeSelf);

		if(go == null)
		{
			go = Instantiate(hightlightPrefab);
			hightlights.Add(go);
		}
		
		return go;
	}

	public void HighlightAllowedMoves(bool [,] moves )
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if(moves[i,j])
				{
					GameObject go  = GetHighlightObject ();
					go.SetActive(true);
					go.transform.position = new Vector3(i+0.5f, 0.01f, j+0.5f);
				}
			}
		}
	}

	public void HideHighlights()
	{
		foreach (GameObject go in hightlights)
			go.SetActive(false);
	}

}

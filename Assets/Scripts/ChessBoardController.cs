using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Game;
using AI;
public class ChessBoardController : MonoBehaviour {
	
	public int deep;


	public static ChessBoardController Instance{get;set;}
	private bool[,] allowedMoves{set;get;}

	public Game.Chessman[,] Chessmans{get;set;} 
	private Game.Chessman selectedChessman;

	private const float TILE_SIZE = 1.0F;
	private const float TILE_OFFSET = .5f;

	private int selectionX = -1;
	private int selectionY = -1;

	public List<GameObject> chessman;
	private List<GameObject> activeChessman;

	private Quaternion orientation  = Quaternion.Euler(-90,180,90);

	public bool isWhiteTurn  = true;

	public bool gameOver = false;
	// private ChessAI ia; 
	private Elloin elloin;

	public TextMeshProUGUI gameOverText;
	public GameObject gameOverMessage;

	public bool coolDown = true;

	public AudioClip moveSound;
	public AudioClip killSound;
	public AudioClip VictorySound;
	public AudioClip DefeatSound;
	public AudioSource audioSource;
	// Use this for initialization
	void Start () 
	{
		
		//Return the current Active Scene in order to get the current Scene's name
        Scene scene = SceneManager.GetActiveScene();

        //Check if the current Active Scene's name is your first Scene
        if (scene.name == "StoneScene")
			orientation = Quaternion.Euler(0,0,0);

		Instance = this;
		SpawAllPieces();

		audioSource = this.GetComponent<AudioSource>();
		// ia = new ChessAI();
		elloin = new Elloin(false, deep);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gameOver) return;

		DrawBoard();
		UpdateSelection();
		if(isWhiteTurn)
		{

			#if UNITY_EDITOR || UNITY_STANDALONE	
				if(Input.GetMouseButtonDown(0))
			#else
				if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			#endif
			{
				if(selectedChessman == null)
				{
					//select the piece
					Debug.Log("try select a chessman");
					SelectChessman(selectionX, selectionY);
				}
				else{
					//try move the piece
					Debug.Log("try move a chessman");
					MoveChessman(selectionX, selectionY);
					StartCoroutine("CoolDownNextMove",.5f);
				}
			}
		}
		else
		{
			if(coolDown)
				return;
			// int[] move = ia.StartIA();
			// Debug.Log("Quantidade de movimentos analisados: "+ia.qtd_movimentos);
			// SelectChessman(move[0], move[1]);
			// MoveChessman(move[2], move[3]);

			Move move = elloin.chooseBestMove();
			SelectChessman(move.origin.x, move.origin.y);
			MoveChessman(move.destiny.x, move.destiny.y);
		}

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
		
		if(!isWhiteTurn)
			return;

		RaycastHit hit;

		
		#if UNITY_EDITOR || UNITY_STANDALONE
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit,100.0f, LayerMask.GetMask("chessboard")))
		#else
			if(!(Input.touchCount > 0))
				return;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 10000000.0f, LayerMask.GetMask("chessboard")))
		#endif
		{
			selectionX = (int) hit.point.x;
			selectionY = (int) hit.point.z;
		}
		else{
			selectionX = -1;
			selectionY = -1;
		}
	} 


	private void SelectChessman(int x, int y)
	{
		if(Chessmans[x,y] == null)
			return;

		if(Chessmans[x,y].isWhite != isWhiteTurn)
			return;

		allowedMoves = Chessmans[x,y].PossibleMove();
		selectedChessman = Chessmans[x,y];
		BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);
	}

	private void MoveChessman(int x, int y)
	{
		if(allowedMoves[x,y])
		{
			Game.Chessman c = Chessmans[x,y];
			if(c!= null && c.isWhite != isWhiteTurn)
			{
				//capture the piece

				if(c.GetType() == typeof(KingBehavior))
				{
					EndGame(c);
					return;
				}

				activeChessman.Remove(c.gameObject);
				Destroy(c.gameObject);
				audioSource.clip = killSound;
				audioSource.Play();
			}
			else{
				audioSource.clip = moveSound;
				audioSource.Play();
			}
			Chessmans[selectedChessman.currentX,selectedChessman.currentY] = null;
			selectedChessman.transform.position = GetTileCenter(x,y);
			Chessmans[x, y] = selectedChessman;
			selectedChessman.setPostion(x,y);
			isWhiteTurn= !isWhiteTurn;
		}
		BoardHighlights.Instance.HideHighlights();
		selectedChessman = null;
		// PrintChessmans();
	}


	private void SpawnPiece(int index, int x, int y)
	{
		GameObject go = Instantiate(chessman[index],GetTileCenter(x,y),orientation) as GameObject;
		Chessmans[x,y] = go.GetComponent<Game.Chessman>();
		Chessmans[x,y].setPostion(x,y);
		go.transform.SetParent(this.transform);
		activeChessman.Add(go);
	}

	private void SpawAllPieces()
	{
		activeChessman =  new List<GameObject>();
		Chessmans =  new Game.Chessman[8,8];
		//white pieces
		
		//king
		SpawnPiece(0, 4,0);
		//Queen
		SpawnPiece(1, 3,0);
		//bishops
		SpawnPiece(2, 2,0);
		SpawnPiece(2, 5,0);
		// // //kights
		SpawnPiece(3, 1,0);
		SpawnPiece(3, 6,0);
		// //Rook
		SpawnPiece(4, 0,0);
		SpawnPiece(4, 7,0);
		for (int i = 0; i < 8; i++)
			SpawnPiece(5, i,1);

		//Black
		//king
		SpawnPiece(6, 4,7);
		//Queen
		SpawnPiece(7, 3,7);
		//bishops
		SpawnPiece(8, 2,7);
		SpawnPiece(8, 5,7);
		//kights
		SpawnPiece(9, 1,7);
		SpawnPiece(9, 6,7);
		//Rook
		SpawnPiece(10, 0,7);
		SpawnPiece(10, 7,7);
		for (int i = 0; i < 8; i++)
			SpawnPiece(11, i,6);
	}

	private Vector3 GetTileCenter(int x, int y)
	{
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE*x)+TILE_OFFSET;
		origin.z += (TILE_SIZE*y)+TILE_OFFSET;

		return origin;
	}

	private void PrintChessmans()
	{
		string text = "";
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				text+=(Chessmans[j, i]!=null)+" | ";
			}
			text+='\n';
		}
		Debug.Log(text);
	}

	private void EndGame(Game.Chessman king)
	{
		if(king.isWhite){
			gameOverText.text = "Você perdeu :(";
			audioSource.clip = DefeatSound;
		}
		else{
			audioSource.clip = VictorySound;
			gameOverText.text = "Você venceu :)";
		}
		
		audioSource.Play();

		this.gameOver = true;
		this.gameOverMessage.SetActive(true);
	}



	private IEnumerator CoolDownNextMove(float waitTime)
    {
		coolDown = true;
		yield return new WaitForSeconds(waitTime);
		coolDown = false;
    }
}

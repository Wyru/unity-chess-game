using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameOptions : MonoBehaviour {

	public void reloadScene()
	{
		Debug.Log("reload scene");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void loadMenu()
	{
		SceneManager.LoadScene(1);
	}
}

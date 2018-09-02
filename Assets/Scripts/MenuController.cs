using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	

	public void play()
	{
		SceneManager.LoadScene(1);
	}

	public void challenges()
	{

	}

	public void options()
	{
		
	}

	public void credits()
	{

	}

	public void exit()
	{
		Application.Quit();
	}

}

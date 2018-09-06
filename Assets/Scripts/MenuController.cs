using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {


	public GameObject creditsGO;

	public GameObject CommingSoonGO;

	public GameObject blackEffectGO;

	public void play()
	{
		SceneManager.LoadScene(1);
	}

	public void challenges(bool open)
	{
		

	}

	public void options(bool open)
	{
		blackEffectGO.SetActive(open);
		CommingSoonGO.SetActive(open);
	}

	public void credits(bool open)
	{
		blackEffectGO.SetActive(open);
		creditsGO.SetActive(open);
	}

	public void exit()
	{
		Application.Quit();
	}

}

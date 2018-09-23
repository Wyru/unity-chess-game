using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opções : MonoBehaviour {
	public GameObject BGM;
	public int x = 1;
	public void musica(bool open)
	{
		if (x == 1) {
			BGM.SetActive (open);
			x = 0;
		} else {
			BGM.SetActive (false);
			x = 1;
		}
	}

}

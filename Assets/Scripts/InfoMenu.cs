using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoMenu : MonoBehaviour 
{
	[SerializeField]
	GameObject startMenu;

	public void PressBack()
	{
		gameObject.SetActive(false);

		startMenu.SetActive(true);
	}
}

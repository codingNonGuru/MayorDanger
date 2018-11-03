using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour 
{
	[SerializeField]
	CanvasGroup canvasGroup = null;

	[SerializeField]
	GameObject vremuriGrele = null;

	[SerializeField]
	GameObject saTraiti = null;

	[SerializeField]
	GameObject startMenu = null;

    public AudioSource EndGameWin;
    
    public void Setup () 
	{
		SpawnManager.OnGameOver += HandleGameOver;
	}

	public void Press()
	{
		SpawnManager.Restart();

		gameObject.SetActive(false);
	}

	public void PressMenu()
	{
		gameObject.SetActive(false);

		startMenu.SetActive(true);
	}

	void HandleGameOver()
	{
		if(SpawnManager.IsAlive)
		{
			vremuriGrele.SetActive(false);
			saTraiti.SetActive(true);
            EndGameWin.Play();
		}
		else
		{
			vremuriGrele.SetActive(true);
			saTraiti.SetActive(false);
		}

		gameObject.SetActive(true);
	}
}

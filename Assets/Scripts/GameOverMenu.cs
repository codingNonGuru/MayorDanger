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
    // Use this for initialization
    void Start () 
	{
		SpawnManager.OnGameOver += HandleGameOver;
	}

	public void Press()
	{
		SpawnManager.Restart();

		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;
	}

	public void PressMenu()
	{
		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;

		startMenu.SetActive(true);
	}

	void HandleGameOver()
	{
		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = true;

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
	}
}

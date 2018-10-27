using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour 
{
	[SerializeField]
	CanvasGroup canvasGroup = null;

	[SerializeField]
	Text label = null;

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

	void HandleGameOver()
	{
		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = true;

		if(SpawnManager.IsAlive)
		{
			label.text = "Sa traiti,\ndom' Primar!";
            EndGameWin.Play();
		}
		else
		{
			label.text = "Vremuri grele,\nce sa facem?!";
		}
	}
}

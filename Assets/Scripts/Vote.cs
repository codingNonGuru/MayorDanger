using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vote : MonoBehaviour 
{
	Animator animator = null;

	// Use this for initialization
	void Start () 
	{
		animator = GetComponent<Animator>();	
	}
	
	public void FadeOut()
	{
		animator.Play("FadeOut", 0);
	}

	public void Enable()
	{
		gameObject.SetActive(true);
	}

	public void Disable()
	{
		gameObject.SetActive(false);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBatch : MonoBehaviour 
{
	Animator animator = null;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public void FadeOut()
	{
		animator.Play("FadeOut", 0, 0.0f);
	}

	public void StayIdle()
	{
		animator.Play("Idle", 0, 0.0f);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour 
{
	public Vector3 Direction;

	float timer = 0.0f;

	const float lifeTime = 10.0f;

	// Use this for initialization
	void Start () 
	{
		SpawnManager.OnGameRestarted += Die;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += Direction * Time.deltaTime * 3.0f;	

		timer += Time.deltaTime;

		if(timer > lifeTime)
		{
			Die();
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		var negoita = collider.gameObject.GetComponent<Negoita>();
		if(negoita == null)
			return;

		SpawnManager.Damage();

		Die();
	}

	void Die()
	{
		SpawnManager.OnGameRestarted -= Die;

		Destroy(gameObject);
	}
}

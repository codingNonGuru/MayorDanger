using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour 
{
	public GameObject Camp = null;

	public Vector3 Direction;

    public AudioSource ImpactParrot;

    [SerializeField]
	GameObject signPrefab = null;

	Material material = null;

	float timer = 0.0f;

	const float cooldown = 2.0f;

	int textureIndex = 0;

	bool isInside = false;

	// Use this for initialization
	void Start () 
	{
		material = GetComponent<MeshRenderer>().material;

		SpawnManager.OnGameRestarted += HandleGameRestarted;

		timer = UnityEngine.Random.Range(0.0f, cooldown);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += Direction * Time.deltaTime * 0.7f;

		if(isInside)
		{
			if(timer > cooldown)
			{
				var signObject = Instantiate(signPrefab);
				signObject.transform.position = transform.position;

				var sign = signObject.GetComponent<Sign>();
				var direction = SpawnManager.Negoita.transform.position - transform.position;
				direction.y = 0.0f;
				direction.Normalize();

				sign.Direction = direction;

				timer = 0.0f;
			}

			timer += Time.deltaTime;
		}

		material.mainTexture = SpawnManager.CitizenTextures[textureIndex];
		textureIndex++;

		if(textureIndex == SpawnManager.CitizenTextures.Count)
			textureIndex = 0;
	}

	public void Die()
	{
		SpawnManager.OnGameRestarted -= HandleGameRestarted;

		Destroy(gameObject);
            
	}

	void OnTriggerEnter(Collider collider)
	{
		var terrain = collider.gameObject.GetComponent<Terrain>();
		if(terrain != null)
		{
			isInside = true;
		}

		var camp = collider.gameObject.GetComponent<PicketCamp>();
		if(camp == null)
			return;

		SpawnManager.KillCitizen(true);

		Die();
	}

	void HandleGameRestarted()
	{
		Die();
	}
}

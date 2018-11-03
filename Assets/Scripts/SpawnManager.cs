using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CitizenGroupData
{
	public int Count;
	public float StartPositionFactor;
	public int Depth;
}

[Serializable]
public class CitizenWaveData
{
	public List <CitizenGroupData> Groups;
}

public class SpawnManager : MonoBehaviour 
{
	public static SpawnManager instance = null;
    public AudioSource ImpactParrot;
    public static event Action OnWaveSpawned;
	public static event Action OnWaveEnded;
	public static event Action OnGameOver;
	public static event Action OnGameRestarted;
	public static event Action OnCitizenFinished;

	[SerializeField]
	List <CitizenWaveData> waveDatas = null;

	[SerializeField]
	GameObject citizenPrefab = null;

	[SerializeField]
	GameObject picketCamp = null;

	[SerializeField]
	List <TreeBatch> treeBatches = null;

	[SerializeField]
	List <GameObject> houseBatches = null;

	[SerializeField]
	GameObject negoita = null;

	[SerializeField]
	List <Texture> citizenTextures = null;

	[SerializeField]
	List <GameObject> pavements = null;

	[SerializeField]
	GameOverMenu gameOverMenu = null;

	int currentWaveIndex = 0;

	int currentGroupIndex = 0;

	CitizenWaveData currentWave = null;

	int activeCitizenCount = 0;

	int hitpointCount = 10;

	bool isPlaying = false;

	public static List <Texture> CitizenTextures
	{
		get {return instance.citizenTextures;}
	}

	public static bool IsAlive
	{
		get {return instance.hitpointCount > 0;}
	}

	public static bool HasWavesLeft
	{
		get {return instance.currentWaveIndex < instance.waveDatas.Count;}
	}

	public static int HitpointCount
	{
		get {return instance.hitpointCount;}
	}

	public static bool IsPlaying
	{
		get {return instance.isPlaying;}
	}

	public static GameObject Negoita
	{
		get {return instance.negoita;}
	}

	public static int CurrentWaveIndex
	{
		get {return instance.currentWaveIndex;}
	}

	void Awake()
	{
		instance = this;

		gameOverMenu.Setup();
	}

	public static void SpawnWave()
	{
		instance.isPlaying = true;

		instance.currentGroupIndex = 0;

		instance.currentWave = instance.waveDatas[instance.currentWaveIndex];

		instance.currentWaveIndex++;

		instance.activeCitizenCount = 0;
		for(int i = 0; i < instance.currentWave.Groups.Count; ++i)
		{
			instance.activeCitizenCount += instance.currentWave.Groups[i].Count;
		}
		
		instance.StartCoroutine(instance.LaunchWaveCoroutine());

		if(OnWaveSpawned != null)
		{
			OnWaveSpawned.Invoke();
		}
	}

	public static void KillCitizen(bool hasFinished)
	{
		if(!IsAlive)
			return;

		instance.activeCitizenCount--;

		if(hasFinished)
		{
			instance.hitpointCount--;

			if(OnCitizenFinished != null)
			{
				OnCitizenFinished.Invoke();
			}

			if(instance.hitpointCount == 0)
			{
				instance.isPlaying = false;

				if(OnGameOver != null)
				{
					OnGameOver.Invoke();
				}

				return;
			}
		}

		if(instance.activeCitizenCount == 0)
		{
			instance.isPlaying = false;

			instance.treeBatches[instance.currentWaveIndex - 1].FadeOut();

			instance.houseBatches[instance.currentWaveIndex - 1].SetActive(true);

			instance.pavements[instance.currentWaveIndex - 1].SetActive(true);

			if(OnWaveEnded != null)
			{
				OnWaveEnded.Invoke();
			}

			if(!HasWavesLeft)
			{
				if(OnGameOver != null)
				{
					OnGameOver.Invoke();
				}
			}
		}
	}

	public static void Damage()
	{
		if(!IsAlive)
			return;

		instance.hitpointCount--;

		if(OnCitizenFinished != null)
		{
			OnCitizenFinished.Invoke();
		}

		if(instance.hitpointCount == 0)
		{
			instance.isPlaying = false;

			if(OnGameOver != null)
			{
				OnGameOver.Invoke();
			}
		}
	}

	public static void Restart()
	{
		instance.currentWaveIndex = 0;

		instance.hitpointCount = 10;

		foreach(var batch in instance.treeBatches)
		{
			batch.StayIdle();
		}

		foreach(var batch in instance.houseBatches)
		{
			batch.SetActive(false);
		}

		foreach(var pave in instance.pavements)
		{
			pave.SetActive(false);
		}

		if(OnGameRestarted != null)
		{
			OnGameRestarted.Invoke();
		}
	}

	IEnumerator LaunchWaveCoroutine()
	{
		for(int i = 0; i < currentWave.Groups.Count; ++i)
		{
			SpawnGroup();

			yield return new WaitForSeconds(5.0f);
		}
	}

	void SpawnGroup()
	{
		var group = currentWave.Groups[currentGroupIndex];

		var sourcePosition = new Vector3(9.3f, 0.0f, 0.0f);

		float offset = (float)(group.Count / group.Depth - 1) / 2.0f;
		for(int i = 0; i < group.Count / group.Depth; ++i)
		{
			for(int j = 0; j < group.Depth; ++j)
			{
				float radius = UnityEngine.Random.Range(0.0f, 0.3f);
				float angle = UnityEngine.Random.Range(0.0f, 6.2831f);

				var currentOffset = (float)i - offset;
				currentOffset *= 1.3f;
				var citizenObject = Instantiate(citizenPrefab);
				citizenObject.transform.position = new Vector3(
					sourcePosition.x + (float)j + Mathf.Cos(angle) * radius,
					citizenObject.transform.position.y, 
					sourcePosition.z + group.StartPositionFactor * 5.5f + currentOffset + Mathf.Sin(angle) * radius);

				var citizen = citizenObject.GetComponent<Citizen>();
				citizen.Camp = picketCamp;
			}
		}

		currentGroupIndex++;
	}
}

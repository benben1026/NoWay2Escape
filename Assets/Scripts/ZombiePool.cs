using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour {

	public static ZombiePool SharedInstance;
	
	public int zombiePoolSize = 5;
	public float spawnRate = 4f;
	private GameObject[] zombies;
	public GameObject zombiePrefab;
	public int threadHold = 5;
	private Vector2 objectPoolPosition;
	private int startedge = 20;
	private int endedge = 60;


	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	private System.Random rnd;

	// Use this for initialization
	void Start () {

		zombies = new GameObject[zombiePoolSize];
		rnd = new System.Random();
		for (int i = 0; i < zombiePoolSize; i++) {
			objectPoolPosition = new Vector2 (rnd.Next(startedge,endedge), rnd.Next(startedge,endedge));
			zombies [i] = (GameObject)Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);
			zombies [i].transform.position = objectPoolPosition;
			
//			zombies [i].SetActive (false);

	}

	// Update is called once per frame
	
	}
//	public GameObject GetPooledObject(){
//		for (int i = 0; i < pooledObjects.Count; i++) {
//			if (!pooledObjects [i].activeInHierarchy) {
//				return pooledObjects [i];
//			}
//		}
//		return null;
//	}
}

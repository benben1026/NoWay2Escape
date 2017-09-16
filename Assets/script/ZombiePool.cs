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


	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	private System.Random rnd;

	void Awake(){
		SharedInstance = this;
	}
	// Use this for initialization
	void Start () {

		pooledObjects = new List<GameObject> ();
		for (int i = 0; i < amountToPool; i++) {
			GameObject obj = (GameObject) Instantiate (objectToPool);
			obj.SetActive (false);
			pooledObjects.Add (obj);
		}
//		zombies = new GameObject[zombiePoolSize];
//		rnd = new System.Random();
//		for (int i = 0; i < zombiePoolSize; i++) {
//			objectPoolPosition = new Vector2 (rnd.Next(-5,5), rnd.Next(-5,5));
//			zombies [i] = (GameObject)Instantiate (zombiePrefab,objectPoolPosition, Quaternion.identity);
//			zombies [i].transform.position = objectPoolPosition;
//			
//			zombies [i].SetActive (false);

	}

	// Update is called once per frame
	void Update () {
//		for (int i = 0; i < zombiePoolSize; i++) {
//			if (rnd.Next (0, 11) > threadHold) {
//				zombies[i].
//			}
//		}
	}
	public GameObject GetPooledObject(){
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy) {
				return pooledObjects [i];
			}
		}
		return null;
	}
}

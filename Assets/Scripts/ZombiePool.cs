using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour {

	public static ZombiePool SharedInstance;

	public int zombiePoolSize = 1;
	public float spawnRate = 4f;
	//private GameObject[] zombies;
	public GameObject zombiePrefab;
	//private GameObject[] expzombies;
	public GameObject expzombiePrefab;
	//private GameObject[] helzombies;
	public GameObject helzombiePrefab;


	public int threadHold = 5;
	private Vector2 objectPoolPosition;
	//private int radius = 2;



	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	private System.Random rnd;

	// Use this for initialization
	void Start () {
		this.zombiePoolSize = 30;
		Vector3 flagPositon = DestinationFlag.instance.transform.position;
		Vector3 carPosition = Car.instance.transform.position;
		float uniformDistance = (flagPositon - carPosition).magnitude;
		//print (flagPositon);
//		int x = (int)flagPositon.x;
//		int y = (int)flagPositon.y;
//		zombies = new GameObject[zombiePoolSize];
//		expzombies = new GameObject[zombiePoolSize];
//		helzombies = new GameObject[zombiePoolSize];
		rnd = new System.Random();
		int generated = 0;
		while (generated < zombiePoolSize) {
			float pos_x = (float)rnd.NextDouble () * 20 + 2.5f;
			float pos_y = (float)rnd.NextDouble () * 15 + 2.5f;
			float distanceToCar = (new Vector3 (pos_x, pos_y, 0) - carPosition).magnitude;
			if (distanceToCar < 0.3 * uniformDistance) {
				continue;
			}
			float probability = (distanceToCar / uniformDistance >= 1) ? 0.99f : distanceToCar / uniformDistance;
			if (rnd.NextDouble () < probability) {
				int typeDetermine = rnd.Next (0, 10);
				objectPoolPosition = new Vector2(pos_x, pos_y);
				GameObject tmp;
				if (typeDetermine <= 3) {
					tmp = (GameObject)Instantiate (zombiePrefab, objectPoolPosition, gameObject.transform.rotation);
				} else if (typeDetermine > 3 && typeDetermine <= 6) {
					tmp = (GameObject)Instantiate (expzombiePrefab, objectPoolPosition, gameObject.transform.rotation);
				} else {
					tmp = (GameObject)Instantiate (helzombiePrefab, objectPoolPosition, gameObject.transform.rotation);
				}
				tmp.transform.position = objectPoolPosition;
				generated++;	
			}
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

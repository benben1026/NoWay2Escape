﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePool : MonoBehaviour {

	public static ZombiePool SharedInstance;

	public int zombiePoolSize = 5;
	public float spawnRate = 4f;
	private GameObject[] zombies;
	public GameObject zombiePrefab;
	private GameObject[] expzombies;
	public GameObject expzombiePrefab;
	private GameObject[] helzombies;
	public GameObject helzombiePrefab;


	public int threadHold = 5;
	private Vector2 objectPoolPosition;
	private int startedge = 0;
	private int endedge = 15;


	public List<GameObject> pooledObjects;
	public GameObject objectToPool;
	public int amountToPool;

	private System.Random rnd;

	// Use this for initialization
	void Start () {

		zombies = new GameObject[zombiePoolSize];
		expzombies = new GameObject[zombiePoolSize];
		helzombies = new GameObject[zombiePoolSize];
		rnd = new System.Random();
		for (int i = 0; i < zombiePoolSize; i++) {
			objectPoolPosition = new Vector2 (rnd.Next(startedge,endedge), rnd.Next(startedge,endedge));
			zombies [i] = (GameObject)Instantiate (zombiePrefab,objectPoolPosition, gameObject.transform.rotation);
			zombies [i].transform.position = objectPoolPosition;

			objectPoolPosition = new Vector2 (rnd.Next(startedge,endedge), rnd.Next(startedge,endedge));
			expzombies [i] = (GameObject)Instantiate (expzombiePrefab,objectPoolPosition, gameObject.transform.rotation);
			expzombies [i].transform.position = objectPoolPosition;

			objectPoolPosition = new Vector2 (rnd.Next(startedge,endedge), rnd.Next(startedge,endedge));
			helzombies [i] = (GameObject)Instantiate (helzombiePrefab,objectPoolPosition, gameObject.transform.rotation);
			helzombies [i].transform.position = objectPoolPosition;


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

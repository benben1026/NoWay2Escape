using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationFlag : MonoBehaviour {
	public static DestinationFlag instance;
//	public static Vector3 postion;

	// Use this for initialization
	private void Awake()
	{
		
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (instance);
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

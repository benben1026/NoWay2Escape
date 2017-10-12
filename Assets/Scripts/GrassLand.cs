using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassLand : MonoBehaviour {


	private Car carone;


	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

private void OnTriggerEnter2D(Collider2D other){
	
	if (other.GetComponent<Car> () != null) {
		
		//Car.instance.updateVeclocity (0.5f);
        Car.instance.SetLandType(Car.LandType.Grass);



	}
}
private void OnTriggerStay2D(Collider2D other){
	
	//if (other.GetComponent<Car> () != null) {
		
	//	Car.instance.updateVeclocity (1f);

	
	//}
}
private void OnTriggerExit2D(Collider2D other){

	if (other.GetComponent<Car> () != null) {

            //Car.instance.updateVeclocity (2f);
            Car.instance.SetLandType(Car.LandType.Road);

	}
}
}

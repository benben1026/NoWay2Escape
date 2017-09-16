using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour {
    public float turningAngle;
    private Rigidbody2D rigi;
    
    private void Awake()
    {
        rigi = GetComponent<Rigidbody2D>();
    }
    
    // Use this for initialization
    void Start () {
        this.rigi.velocity = new Vector2(0f, 1.5f);
        this.turningAngle = 0.02f; // Radian
    }
	
	// Update is called once per frame
	void Update () {
        float move = Input.GetAxis("Horizontal");
        print(rigi.velocity.magnitude);

        if (move == 1)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = this.turningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
        }
        else if (move == -1)
        {
            float x = this.rigi.velocity.x;
            float y = this.rigi.velocity.y;
            float angle = (-1) * this.turningAngle;
            this.rigi.velocity = new Vector2(x * Mathf.Cos(angle) + y * Mathf.Sin(angle), (-1) * x * Mathf.Sin(angle) + y * Mathf.Cos(angle));
            transform.Rotate(0, 0, (-1) * angle * 180 / Mathf.PI);
        }
        
    }
}

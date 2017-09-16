using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

   
    public float smoothing = 0.1f;
    public float restTime = 0.64f;
    public float restTimer = 0;
    private BoxCollider2D collider;

    private Vector2 targetPos = new Vector2(1, 1);
    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        bool grassYN =MapManager.check(targetPos);
        if (grassYN) {
            rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPos, smoothing * Time.deltaTime));

        }
        else {
            rigidbody.MovePosition(Vector2.Lerp(transform.position, targetPos, 8* Time.deltaTime));
        }
        
        restTimer += Time.deltaTime;
        if (restTimer < restTime) return;

        

        if (h != 0|| v != 0){

               RaycastHit2D hit;
          
                collider.enabled = false;
                hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
                collider.enabled = true;
           
            
            if (hit.transform == null)
            {
                              
                    
                    targetPos += new Vector2(h, v);
                  
                    
                    restTimer = 0;
                
            }
            else {
                switch (hit.collider.tag) {
                    

                    case "wall":
                        break;
                        



                }
            }

            

        }
        
	}


  
}

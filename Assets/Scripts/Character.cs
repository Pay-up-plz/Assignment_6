using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    Rigidbody2D charRB;

    [Header("CHARACTER MOVEMENT")]

    float hVelocity; //store the directions pressed from -1 to 1.
    [Range(0.01f,5.0f)]
    public float hScale = .05f; //our scaling factor for horizontal movement
    [Header("horizontal")]
    float vVelocity;//stores the vertical velocity

    [Tooltip("The height of the jump")]
    [Range(0.5f, 15.0f)]
    public float jumpVal = 1.0f; //the jump velocity when applied
    [Range(0.5f, 15.0f)]
    public float secondJumpVal = 1.5f;//second jump velocity
    [Tooltip("Let's us know when character is on ground")]
    public bool onGround; //let's us know when characer is on  the ground
    public float interJumpTime = .25f;
    float jumpStartTime;
    public int jumps;

	// Use this for initialization
	void Start () {
        charRB = gameObject.GetComponent<Rigidbody2D>();
        jumps = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
        getHorizontal();
        getJump();
        move();
	}

    void getHorizontal() {
        hVelocity = Input.GetAxis("Horizontal") * hScale * Time.deltaTime;
    }

    void move(){
        charRB.transform.position = new Vector2(hVelocity + charRB.transform.position.x, charRB.transform.position.y);
        charRB.velocity += (Vector2.up * vVelocity);
    }

    void getJump(){
        if (Input.GetKeyDown(KeyCode.Space)){
            if (jumps == 1 && ((Time.time - jumpStartTime) > interJumpTime)){
                vVelocity = secondJumpVal;
                jumps++;
            }

            if (onGround) {
                vVelocity = jumpVal;
                jumpStartTime = Time.time;
                jumps++;
            }
        } else {
            vVelocity = 0;
        }
    }
   void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Ground")) { 
            onGround = true;
            jumps = 0;
        }
    }
    void OnTriggerExit2D(Collider2D coll){
        if (coll.CompareTag("Ground")){
            onGround = false;
            
        }
    }
}

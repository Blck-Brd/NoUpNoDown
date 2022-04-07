using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsJumpControll : MonoBehaviour
{
    public GameObject player;
    public Rigidbody playerRb;

 

    public GameObject currentObjWalkedOn;




        //ANIM 
    public Animator animatorPlayer; 
    public bool animGrounded;
    public float forward;
    public float turn;
    public bool jumping;
    public bool isCrouched;

    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
   public float jumpStrenght = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // jump code - jump to wall or simple jump
        if (jumping) return; // abort Update while jumping to a wall

        if(Input.GetKeyDown(KeyCode.Space) && currentObjWalkedOn!=null)
        {
            playerRb.AddForce(Camera.main.transform.forward * jumpStrenght,ForceMode.Impulse);
        }




    }

    private void FixedUpdate()
    {

        

       





    }


     //AnimColissions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("static"))
        {

            currentObjWalkedOn = collision.gameObject;
        }
    }

    void OnCollisionStay(Collision other)
    {
                if (other.gameObject.CompareTag("static"))
        {

            currentObjWalkedOn = other.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("static"))
        {
            currentObjWalkedOn = null;
        }
    }




}
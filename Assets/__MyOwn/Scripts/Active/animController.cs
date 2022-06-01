using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CMF;

public class animController : MonoBehaviour
{

 public Animator animatorPlayer; 

 public Camera cam;
    public bool animGrounded;
    public bool animGrabbed;
    public int animActiveWpn;
    public float forward;
    public float turn;
    public bool jumping;
    public bool isCrouched;

    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)

   //For feeding calculated anims from other scripts
    public Mover mover;
   public pickupV2 pickupScript;
   public equipManager equipScript;

   



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                //Crouch Toggle
        if(Input.GetKeyDown(KeyCode.C))
        {
            isCrouched = !isCrouched;
        }

        forward = Input.GetAxis("Vertical") * moveSpeed;
        turn = Input.GetAxis("Mouse X") * (turnSpeed /15);

        //Script checker

   animGrounded = mover.isGrounded;
   animGrabbed = pickupScript.objectAttached;
   animActiveWpn = equipScript.ActiveWeapon;





              

       
        AnimFeed(forward, turn, animGrounded, isCrouched, animGrabbed, animActiveWpn);

        

    }

    void AnimFeed(float Forward, float Turn, bool animGrounded, bool isCrouched, bool animGrabbed, int animActiveWpn)
    {

        animatorPlayer.SetFloat("Forward", Forward);
        animatorPlayer.SetFloat("Turn", Turn);
        animatorPlayer.SetBool("OnGround", animGrounded);
        animatorPlayer.SetBool("Crouch", isCrouched);
        animatorPlayer.SetBool("HandGrab", animGrabbed);
        animatorPlayer.SetInteger("Armed", animActiveWpn);

    }





}

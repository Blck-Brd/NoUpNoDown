using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{

    // Prequisites: create an empty GameObject, attach to it a Rigidbody w/ UseGravity unchecked
    // To empty GO also add BoxCollider and this script. Makes this the parent of the Player
    // Size BoxCollider to fit around Player model.

    private float moveSpeed = 6; // move speed
    private float turnSpeed = 90; // turning speed (degrees/second)
    private float lerpSpeed = 10; // smoothing speed
    private float gravity = 10; // gravity acceleration
    private bool isGrounded;
    private float deltaGround = 0.2f; // character is grounded up to this distance
    private float jumpSpeed = 10; // vertical jump initial speed
    private float jumpRange = 1000; // range to detect target wall
    private Vector3 surfaceNormal; // current surface normal
    private Vector3 myNormal; // character normal
    private float distGround; // distance from character position to ground
    private bool jumping = false; // flag &quot;I'm jumping to wall&quot;
    
    public float jumpSpeedModifier;

    private Transform myTransform;
    public BoxCollider boxCollider; // drag BoxCollider ref in editor
    private Rigidbody rigidbody;


    public float distanceToImpact;

    //ANIM
    Animator animatorTorso;
    Animator animatorLeftHand;
    Animator animatorRightHand;
    public GameObject playerTorso;
    
    public GameObject rightHandEGO;
    public GameObject leftHandEGO;

    public GameObject rightHand;
    public GameObject leftHand;


 

    public bool animGrounded;


    // Start is called before the first frame update
    void Start()
    {
        myNormal = transform.up; // normal starts as character up direction
        myTransform = transform;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true; // disable physics rotation
                                         // distance from transform.position to ground
        distGround = boxCollider.size.y - boxCollider.center.y;


        animatorTorso = playerTorso.GetComponent<Animator>();
        animatorLeftHand = leftHand.GetComponent<Animator>();
        animatorRightHand = rightHand.GetComponent<Animator>();

       



}


    //AnimColissions
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walkable"))
        {
            animGrounded = true;
        }
    }



    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walkable"))
        {
            animGrounded = false;
        }
    }


    private void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        rigidbody.AddForce(-gravity * rigidbody.mass * myNormal);

        //ANIM VRS
        float Forward;
        float Turn;
        
        Forward = Input.GetAxis("Vertical") * moveSpeed;
        Turn = Input.GetAxis("Horizontal") * (turnSpeed /15);


        
        AnimFeed(Forward, Turn, animGrounded, jumping);




    }

    // Update is called once per frame
    void Update()
    {
      
       


       // Debug.Log(Camera.main.transform.forward);
        // jump code - jump to wall or simple jump
        if (jumping) return; // abort Update while jumping to a wall

        

        Ray ray;
        RaycastHit hit;
        

       

        if (Input.GetButtonDown("Jump"))
        { // jump pressed:
            ray = new Ray(myTransform.position, /*myTransform.forward*/Camera.main.transform.forward);   //Look at target wall
          
            if (Physics.Raycast(ray, out hit, jumpRange) && hit.transform.CompareTag("Walkable"))                //Is wall walkable?
            { // wall ahead?
                distanceToImpact = hit.distance;
                JumpToWall(hit.point, hit.normal); // yes: jump to the wall

            }
            else if (isGrounded)
            { // no: if grounded, jump up
            rigidbody.velocity += jumpSpeed * ray.direction;
               // rigidbody.AddForce(ray.direction * jumpSpeed, ForceMode.Impulse);
            }
        }

        // movement code - turn left/right with Horizontal axis:
        myTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        // update surface normal and isGrounded:
        ray = new Ray(myTransform.position, -myNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit))
        { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distGround + deltaGround;
            surfaceNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            surfaceNormal = Vector3.up;
        }
        myNormal = Vector3.Lerp(myNormal, surfaceNormal, lerpSpeed * Time.deltaTime);
        // find forward direction with new myNormal:
        Vector3 myForward = Vector3.Cross(myTransform.right, myNormal);
        // align character to the new myNormal while keeping the forward direction:
        Quaternion targetRot = Quaternion.LookRotation(myForward, myNormal);
        myTransform.rotation = Quaternion.Lerp(myTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);
        // move the character forth/back with Vertical axis:
        myTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);





    }

    private void JumpToWall(Vector3 point, Vector3 normal)
    {
        // jump to wall
        jumping = true; // signal it's jumping to wall
        rigidbody.isKinematic = true; // disable physics while jumping
        Vector3 orgPos = myTransform.position;
        Quaternion orgRot = myTransform.rotation;
        Vector3 dstPos = point + normal * (distGround + 0.5f); // will jump to 0.5 above wall
        Vector3 myForward = Vector3.Cross(myTransform.right, normal);
        Quaternion dstRot = Quaternion.LookRotation(myForward, normal);

        StartCoroutine(jumpTime(orgPos, orgRot, dstPos, dstRot, normal));
        //jumptime
    }


    /*WORKING
    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t +=  Time.deltaTime;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        rigidbody.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }
    */

    private IEnumerator jumpTime(Vector3 orgPos, Quaternion orgRot, Vector3 dstPos, Quaternion dstRot, Vector3 normal)
    {
        for (float t = 0.0f; t < 1.0f;)
        {
            t += Time.deltaTime / distanceToImpact * jumpSpeedModifier;
            myTransform.position = Vector3.Lerp(orgPos, dstPos, t);
            myTransform.rotation = Quaternion.Slerp(orgRot, dstRot, t);
            yield return null; // return here next frame
        }
        myNormal = normal; // update myNormal
        rigidbody.isKinematic = false; // enable physics
        jumping = false; // jumping to wall finished

    }





    void AnimFeed(float Forward, float Turn, bool animGrounded, bool jumping)
    {

        animatorTorso.SetFloat("Forward", Forward);
        animatorTorso.SetFloat("Turn", Turn);
        animatorTorso.SetBool("OnGround", animGrounded);

        animatorLeftHand.SetFloat("Forward", Forward);
        animatorLeftHand.SetFloat("Turn", Turn);
        animatorLeftHand.SetBool("OnGround", animGrounded);

        animatorRightHand.SetFloat("Forward", Forward);
        animatorRightHand.SetFloat("Turn", Turn);
        animatorRightHand.SetBool("OnGround", animGrounded);
    }

}

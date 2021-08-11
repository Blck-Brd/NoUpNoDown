using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnim : MonoBehaviour
{
    public GameObject aiAgent;

    public Animator aiAnimator;
    public Rigidbody aiRb;

    public bool animGrounded;
    public float forward;
    public float turn;
    public Vector3 oldRot;
 
    // Start is called before the first frame update
    void Start()
    {
        aiAnimator = aiAgent.GetComponent<Animator>();
        aiRb = aiAgent.GetComponent<Rigidbody>();
    
        
    }

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

    // Update is called once per frame
    void Update()
    {





    }

    void AnimFeed(float Forward, float Turn, bool animGrounded)
    {

        aiAnimator.SetFloat("Forward", Forward);
        aiAnimator.SetFloat("Turn", Turn);
        aiAnimator.SetBool("OnGround", animGrounded);



    }

    private void FixedUpdate()
    {
        forward = aiRb.velocity.magnitude * 6;


        turn = Vector3.Angle(aiAgent.transform.forward, oldRot) * 6;
        oldRot = aiAgent.transform.forward;



        AnimFeed(forward, 0, animGrounded);
    }
}

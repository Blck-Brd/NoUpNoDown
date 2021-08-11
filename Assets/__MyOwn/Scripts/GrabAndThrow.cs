using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndThrow : MonoBehaviour
{
    public GameObject player;
    public GameObject grabedObject;
    public Rigidbody modedRb;
    public GameObject leftPalmEGO;
    public Vector3 leftPalmPos;

    public bool isBeingAtracted;
    public bool isGrabed;

    public float gravmodStrenght = 10f;
    public float gravmodMultiplier = 10f;
    public float gravModRange = 100f;

    public Ray gravModRay;
    public RaycastHit gravModRaycast;


    public palmGrabTrigger palmScript;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        leftPalmPos = leftPalmEGO.transform.position;


        if (isBeingAtracted && !isGrabed)
        {
            Atract(modedRb, leftPalmPos);
        }
        else { }
    }




    public void Atract(Rigidbody rb, Vector3 handPos)
    {
       

        Vector3 dir;
        dir = handPos - rb.position;
        float strenght = gravmodStrenght / rb.mass;
        rb.useGravity = false;
        rb.AddForce(dir * strenght, ForceMode.Impulse);
        Debug.Log("Atract fired");
    }

    public void Grab(GameObject objectToGrab)
    {
        grabedObject = objectToGrab;
        isBeingAtracted = false;
        Rigidbody rb;
        rb = objectToGrab.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Physics.IgnoreCollision(palmScript.leftPalmTrigger, objectToGrab.GetComponent<Collider>(), true);
        Physics.IgnoreCollision(objectToGrab.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
        objectToGrab.transform.position = leftPalmPos;
        objectToGrab.transform.SetParent(leftPalmEGO.transform);
        isGrabed = true;
        Debug.Log("grab fired");
    }

    public void Throw(GameObject ob)
    {
        
        Rigidbody rb;
        rb = ob.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        float strenght;
        strenght = gravmodStrenght / rb.mass;
        ob.transform.parent = null;
        rb.AddForce(Camera.main.transform.forward * 500, ForceMode.Impulse);
        isGrabed = false;
        //invoke
        Invoke("TriggerEnable", 2);
        Debug.Log("Throw fired");

    }

    public void TriggerEnable()
    {
        palmScript.leftPalmTrigger.enabled = true;
        Debug.Log("TriggerEnable fired");
        Physics.IgnoreCollision(palmScript.leftPalmTrigger, grabedObject.GetComponent<Collider>(), false);
        Physics.IgnoreCollision(grabedObject.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gravModRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            if(Physics.Raycast(gravModRay, out gravModRaycast, gravModRange)
               && gravModRaycast.collider.CompareTag("gravModable")
               && gravModRaycast.rigidbody.mass < gravmodStrenght
               && !isGrabed)
            {
                isBeingAtracted = true;
                modedRb = gravModRaycast.rigidbody;
                grabedObject = gravModRaycast.rigidbody.gameObject;


            }

        }

        if (Input.GetMouseButtonDown(0) && isGrabed)
        {
            Throw(grabedObject);
        }



    }




}

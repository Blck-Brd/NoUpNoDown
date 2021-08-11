
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    //Objects
    public GameObject aiAgent;
    public GameObject player;

    //Components
    public Animator aiAnimator;

    //SCAN Vars
    public bool enemyDetected;
    public float aiViewDistance = 500;


    //Behaviors


    //idle
    public void Idle()
    {
        aiAnimator.SetBool("isIdle", true);

    } 

    public void Combat()
    {

    }

    public void ScanFOV(GameObject aiAgent)
    {
      
        UnityEngine.Vector3 targetDir = player.transform.position - transform.position;
        float angleToPlayer = (Vector3.Angle(targetDir, transform.forward));
        Ray sightRay;
        RaycastHit sightCast;

        if (angleToPlayer >= -55 && angleToPlayer <= 55) // 110� FOV
        {
            sightRay = new Ray((aiAgent.transform.position + new Vector3(0, 0, 2)), ((player.transform.position + new Vector3(0, 0, 2)) - aiAgent.transform.position));
            
            if (
                Physics.Raycast(sightRay, out sightCast, aiViewDistance)
                &&
                sightCast.collider.CompareTag("Player")
                )
            {
                enemyDetected = true;
                Debug.Log("Player in sight!");
            }


        }
        else
        {
            Debug.Log("All clear");
        }
  
    }

    public void RunTo(GameObject who, GameObject where)
    {
        Vector3 dir;
        dir = (where.transform.position - who.transform.position);

        if (dir.magnitude > 5)
        {
            who.transform.LookAt(where.transform);
            who.transform.Translate(dir.normalized * 10 * Time.deltaTime);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScanFOV(aiAgent);

        if(enemyDetected)
        {
            RunTo(aiAgent, player);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControll : MonoBehaviour
{

        public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
        public RotationAxes axes = RotationAxes.MouseXAndY;
        public float sensitivityX = 15F;
        public float sensitivityY = 15F;

        public float minimumX = -90F;
        public float maximumX = 90F;

        public float minimumY = -60F;
        public float maximumY = 60F;

        float rotationY = 0F;
    

    //edit:
    float rotationX = 0F;

    float handRotationX = 0F;
    float handRotationY = 0F;



    //hands
    public GameObject rightHandEGO;
    public GameObject leftHandEGO;



    void Update()
        {
        //CAM CODE
            if (axes == RotationAxes.MouseXAndY)
            {
            //float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
            //edit
            rotationX  += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
         

                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.green);

        //HANDS CODE
        handRotationX += Input.GetAxis("Mouse X") * sensitivityX;
        handRotationX = Mathf.Clamp(handRotationX, -40, 40);


        handRotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        handRotationY = Mathf.Clamp(handRotationY, -10, 80);

        leftHandEGO.transform.localEulerAngles = new Vector3(-handRotationY, handRotationX, 0);
        rightHandEGO.transform.localEulerAngles = new Vector3(-handRotationY, handRotationX, 0);



    }

        void Start()
        {
            //if(!networkView.isMine)
            //enabled = false;

            // Make the rigid body not change rotation
            //if (rigidbody)
            //rigidbody.freezeRotation = true;
        }
    }
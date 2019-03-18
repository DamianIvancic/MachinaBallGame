using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour {

    public float Speed = 0;
    public Camera MainCamera;

    [HideInInspector]
    public Rigidbody Rb;
    private ClimbScript ClimbScript;
   
    [HideInInspector]
    public bool Active = false;
    private bool CheckPoint = false;
    private Vector3 RespawnLocation;

    void Start()
    {
        Rb = GetComponent<Rigidbody>();
        ClimbScript = GetComponent<ClimbScript>();
        RespawnLocation = transform.position;
    }

    void Update()
    {
          if(Rb && Active && MainCamera)
          {    
            if (!ClimbScript.IsClimbing)
            {
                Rb.useGravity = true;

                float MoveXLocal = Input.GetAxisRaw("Horizontal");
                float MoveZLocal = Input.GetAxisRaw("Vertical");

                Vector3 XLocalDir = Vector3.zero;      //3D movement is based on the camera
                Vector3 ZLocalDir = Vector3.zero;

                 if (MoveXLocal < 0)
                     XLocalDir = MainCamera.transform.right * -1;
                 else if (MoveXLocal > 0)
                     XLocalDir = MainCamera.transform.right;

                 if (MoveZLocal < 0)
                     ZLocalDir = MainCamera.transform.forward* -1;
                 else if (MoveZLocal > 0)
                     ZLocalDir = MainCamera.transform.forward;

                Vector3 Direction = XLocalDir + ZLocalDir;

                Rb.AddForce(Direction * Speed);
            }
            if (ClimbScript.IsClimbing)  
            {
                Rb.useGravity = false;
        
                float moveHorizontal = Input.GetAxisRaw("Horizontal");
                float moveVertical = Input.GetAxisRaw("Vertical");

                Vector3 Vertical = Vector3.up * moveVertical;
                Vector3 Horizontal = Vector3.zero;

                int Adjustment;

                if (ClimbScript.InvertOrientation == true)
                    Adjustment = -1;
                else
                    Adjustment = 1;

                if (ClimbScript.RelativeAxis == ClimbScript.Horizontal.X)
                {
                    Horizontal = Vector3.right * moveHorizontal * Adjustment;
                    Vector3 Direction = Vertical + Horizontal;
                    Direction.Normalize();  
                    Rb.AddForce(Direction * Speed);

                    Vector3 Velocity = Rb.velocity;
                    Rb.angularVelocity += new Vector3(Velocity.y * 2 * Adjustment, Velocity.x*2, 0);
                }
                else if (ClimbScript.RelativeAxis == ClimbScript.Horizontal.Z)
                {
                    Horizontal = Vector3.forward * moveHorizontal * Adjustment;
                    Vector3 Direction = Vertical + Horizontal;
                    Direction.Normalize();
                    Rb.AddForce(Direction * Speed);
             
                    Vector3 Velocity = Rb.velocity;
                    Rb.angularVelocity += new Vector3(0, Velocity.z*2, Velocity.y*2 * Adjustment);
                }

                if (!(Input.GetButton("Horizontal") || Input.GetButton("Vertical")))
                {
                    Rb.velocity = Vector3.zero;
                    Rb.angularVelocity = Vector3.zero;
                }
                    
            }
        }
    }
   
    public void SetSpawn(Vector3 location)
    {
        RespawnLocation = location;
        CheckPoint = true;
    }

    public void Respawn()
    {
        Active = false;     
        transform.position = RespawnLocation;
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;
        GameManager.GM.CamController.ResetCamera();       
    }

}

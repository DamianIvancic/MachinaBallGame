using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {

    public GameObject AttachTo;
    
    public float Height = 0;
    public float ZoomSpeed = 0;

    [HideInInspector]
    public Vector3 Direction;
    [HideInInspector]
    public Vector3 DirectionBackup;
    [HideInInspector]
    public Quaternion RotationBackup;

    private Vector3 Zoom;
    private float AdjustPosZ = 0;
    private float IntroAngle = 0;

    private Vector2 MouseLook, MouseLookPrev;

	void Start () {

        if (gameObject.tag == "MainCamera")
        {
            Height = 19;
            AdjustPosZ = -19; // -19/19 since 9 is the difference from -10/10 and that's nicely divisible by 360

            Vector3 Pivot = AttachTo.transform.position;
            Vector3 Pos = new Vector3(Pivot.x, Pivot.y + Height, Pivot.z + AdjustPosZ);
            transform.position = Pos;

            MouseLook = Vector2.zero;
            MouseLookPrev = Vector2.zero;
        }
    }
	

	void Update ()
    { 
        PositionCamera();          
	}

    void PositionCamera()
    {      
        if (IntroAngle < 360)
            IntroRotation();
        else
        {
            Vector3 Pivot = AttachTo.transform.position;
            transform.position = Pivot - Direction; //Direction is a global variable to it keeps the values assigned during IntroRotation and is based on that to work properly
            MouseRotate();                   
        }
    }

    void MouseRotate()
    {
        Transform Target = AttachTo.transform;
        Vector3 TargetPos = Target.position;
   
        Vector2 MouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        MouseLook += MouseDelta;
        MouseLook.y = Mathf.Clamp(MouseLook.y, -45, 45);
    
        transform.RotateAround(TargetPos, Vector3.up, MouseLook.x - MouseLookPrev.x);
        transform.RotateAround(TargetPos, transform.right, MouseLook.y - MouseLookPrev.y); // since this dimension is made of both X and Z components, the axis we're rotating around has to be dynamic
        MouseLookPrev = MouseLook;                                                            
         
        Direction = Target.position - transform.position;
    }
   
    void IntroRotation() // returns Vector between the pivot and the position where the camera is at the end of intro
    {
        Vector3 Pivot = AttachTo.transform.position;
        transform.RotateAround(Pivot, Vector3.up, 2);
        Direction = Pivot - transform.position;
        Direction /= 180;
        transform.position += Direction;
      
        IntroAngle+= 2;
        if (IntroAngle == 360)
        {
            GameManager.GM.MM.Display("START!");
            GameManager.GM.PM.Active = true;
        }

        Direction = Pivot - transform.position;
        DirectionBackup = Direction;
        RotationBackup = transform.rotation;
    }

    public void ResetCamera()
    {
        Direction = Quaternion.Euler(0, -90, 0) * DirectionBackup;
        transform.rotation = Quaternion.Euler(0, -90, 0) * RotationBackup;
        MouseLook = Vector2.zero;
        MouseLookPrev = Vector2.zero;
        
    }
}

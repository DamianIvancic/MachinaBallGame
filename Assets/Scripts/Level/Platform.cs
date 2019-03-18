using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
   
    public FloorPlatformRegulator Regulator;

    private AudioSource MovementSound;

    public Transform OpenPos;
    private Vector3 ClosedPos;
    private Vector3 TargetPos;

    [HideInInspector]
    public bool Moving = false;
    private float Speed = 0.01f;

    void Start()
    {
        if (!Regulator)
            Regulator = GetComponentInParent<FloorPlatformRegulator>();

        ClosedPos = transform.position;
        TargetPos = OpenPos.position;

        MovementSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Regulator.MoveBack == true)
            TargetPos = ClosedPos;
        else
            TargetPos = OpenPos.position;

        if(Moving == true && transform.position.x != TargetPos.x)
        {
           Vector3 Direction = TargetPos - transform.position;
           Direction.Normalize();
           Direction *= 0.05f;

           transform.position += Direction;       
        }  
    }

    public void StartMovement()
    {     
        Moving = true;
        MovementSound.Play();
    }
}

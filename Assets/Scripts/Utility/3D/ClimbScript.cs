using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbScript : MonoBehaviour {

    [HideInInspector]
    public enum Horizontal //stores whether the horizontal axis while climbing corresponds to x or z
    {                      //stupid solution but works on limited basis... can't use the player object's transform for orientation because it's a ball so it's constantly rotating
        X,
        Z
    }

    [HideInInspector]
    public Horizontal RelativeAxis;
    [HideInInspector]
    public GameObject ClimbedObject;
    [HideInInspector]
    public bool IsClimbing = false;
    [HideInInspector]
    public bool InvertOrientation = false;
  
    void OnCollisionEnter(Collision Coll)
    {
        if (Coll.gameObject.tag == "Climbable X" || Coll.gameObject.tag == "Climbable X Inverted" || Coll.gameObject.tag == "Climbable Z" || Coll.gameObject.tag == "Climbable Z Inverted")
        {
            IsClimbing = true;
            ClimbedObject = Coll.gameObject;
        }
    }

    void OnCollisionStay(Collision Coll)
    {
        string ObjectTag = Coll.gameObject.tag;

        if (ObjectTag == "Climbable X Inverted")
        {
           RelativeAxis = Horizontal.X;
           InvertOrientation = true;

           if (Input.GetKeyDown(KeyCode.LeftShift))
               IsClimbing = (!IsClimbing);
        }
        else if (ObjectTag == "Climbable X")
        {
           RelativeAxis = Horizontal.X;
           InvertOrientation = false;

           if (Input.GetKeyDown(KeyCode.LeftShift))
               IsClimbing = (!IsClimbing);
        }
        else if (ObjectTag == "Climbable Z Inverted")
        {
           RelativeAxis = Horizontal.Z;
           InvertOrientation = true;

           if (Input.GetKeyDown(KeyCode.LeftShift))
               IsClimbing = (!IsClimbing);
        }
        else if (ObjectTag == "Climbable Z")
        {
           RelativeAxis = Horizontal.Z;
           InvertOrientation = false;

           if (Input.GetKeyDown(KeyCode.LeftShift))
               IsClimbing = (!IsClimbing);
        }     
    }

    void OnCollisionExit(Collision Coll)
    {
        if (Coll.gameObject.tag == "Climbable X" || Coll.gameObject.tag == "Climbable X Inverted" || Coll.gameObject.tag == "Climbable Z" || Coll.gameObject.tag == "Climbable Z Inverted")
        {
            IsClimbing = false;
            GameManager.GM.PM.Rb.velocity = Vector3.zero;            
        }
    }
}

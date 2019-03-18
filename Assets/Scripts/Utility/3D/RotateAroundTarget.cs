using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTarget : MonoBehaviour {

    public int Rotation;
    public GameObject target;

	void Update () {

        if(gameObject.tag == "RotatingTrap")
        {
            DrawLaser TrapScript = gameObject.GetComponent<DrawLaser>();

            if(TrapScript.Engaged)
                transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), Rotation);
        }
        else
            transform.RotateAround(target.transform.position, new Vector3(0, 1, 0), Rotation);
	}
}

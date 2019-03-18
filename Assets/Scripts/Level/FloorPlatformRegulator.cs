using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlatformRegulator : MonoBehaviour {

    public Transform Player;
    [HideInInspector]
    public bool MoveBack;

	void Update ()
    {
        if (Player.position.y > transform.position.y)
            MoveBack = true;
        else if (Player.position.y < transform.position.y)
            MoveBack = false;
	}

}

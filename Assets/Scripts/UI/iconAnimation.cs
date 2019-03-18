using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconAnimation : MonoBehaviour {

    private Animator Animator;

	void Start () {

        Animator = GetComponent<Animator>();
	}
	
    public void playAnimation()
    {
        Animator.Play("LifeLost");
    }
}

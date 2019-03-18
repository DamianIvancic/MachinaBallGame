using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLaser : MonoBehaviour {

    public Transform Other; //the other point that defines the laser line
    public Switch CoinSwitch;

    private LineRenderer LRenderer;
    private AudioSource SoundEffect;

    [HideInInspector]
    public bool Engaged = false;

    void Start()
    {
        if (Other)
        {
            LRenderer = gameObject.AddComponent<LineRenderer>();
            SoundEffect = GetComponent<AudioSource>();
        }
    }

	void Update ()
    {

        if((Other && CoinSwitch.Pressed == false) || (Other && CoinSwitch.Pressed == true && CoinSwitch.CoinsLeft == 0))
        {
            LRenderer.enabled = false;
            Engaged = false;
        }
        else if (Other && CoinSwitch.Pressed)
        {
            DrawLaserLine();
            DetectLaserTrigger();       
        }
    }

    void DrawLaserLine()
    {
        LRenderer.enabled = true;

        if (gameObject.tag == "RotatingTrap")
        {
            LRenderer.SetPosition(0, transform.position);
            LRenderer.SetPosition(1, Other.transform.position);

            Engaged = true;
        }
        else if (Engaged == false)
        {
            LRenderer.SetPosition(0, transform.position);
            LRenderer.SetPosition(1, Other.transform.position);

            Engaged = true;
        }
    }

    void DetectLaserTrigger()
    {
        Vector3 Start = transform.position;
        Vector3 End = Other.transform.position;
        Vector3 Direction = End - Start;

        RaycastHit Hit;

        if (Physics.Raycast(Start, Direction, out Hit, Direction.magnitude))
        {
            if (Hit.transform.gameObject.tag == "Player")
            {
                SoundEffect.Play();
                GameManager.GM.MM.WarningMessage();
                CoinSwitch.ResetOnTrigger();
            }
        }
    }
}

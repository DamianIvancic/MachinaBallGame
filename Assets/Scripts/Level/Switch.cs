using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public Platform MoveablePlatform;

    private AudioSource SoundEffect;

    [HideInInspector]
    public int CoinsLeft = 0;
    [HideInInspector]
    public bool Pressed = false;
    private float Stopwatch = 0;
    private float Period = 20;

    void Start()
    {
        SoundEffect = GetComponent<AudioSource>();
    }

    void Update()
    { 
        if (MoveablePlatform != null)
        {
            if (Pressed && CoinsLeft == 0 && MoveablePlatform.Moving == false)
                MoveablePlatform.StartMovement();
        } 
    }

    void OnCollisionStay(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Vector3 PlayerPos = coll.gameObject.transform.position;

            if (PlayerPos.y > transform.position.y && Pressed == false)
            {
                GameManager.GM.MM.RefreshPeriod = 0;
                GameManager.GM.MM.Display("Press Space!");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SoundEffect.Play();

                    Pressed = true;

                    Transform[] Children = gameObject.GetComponentsInChildren<Transform>();

                    foreach(Transform Child in Children)
                    {
                        if(Child.gameObject.tag == "CoinSpawn")
                        {
                            Spawner CoinSpawner = Child.GetComponent<Spawner>();
                            CoinSpawner.SpawnObject();
                            CoinsLeft++;
                        }
                        else if(Child.gameObject.tag == "BlueCoinSpawn")
                        {
                            Spawner CoinSpawner = Child.GetComponent<Spawner>();
                            CoinSpawner.SpawnObject();
                        }
                    }
                }
            }
        }
    }

    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
            GameManager.GM.MM.Clear();
    }

    public void ResetOnTrigger() //called when lasers get triggered
    {
        Pressed = false;

        Transform[] Children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform Child in Children)
        {
            if (Child.gameObject.tag == "CoinSpawn")
            {
                Coin CoinScript = Child.GetComponentInChildren<Coin>();
                if (CoinScript != null)
                {
                    Destroy(CoinScript.CoinObject());
                    CoinsLeft--;
                }   
            }
            else if (Child.gameObject.tag == "BlueCoinSpawn")
            {
                Coin CoinScript = Child.GetComponentInChildren<Coin>();
                if (CoinScript != null)
                    Destroy(CoinScript.CoinObject());
            }
        }
    }

    public void OnCoinPickup()
    {
        CoinsLeft--;

        if (CoinsLeft == 0 && gameObject.name == "Switch F1")
        {
            GameManager.GM.MM.RefreshPeriod = 2;
            GameManager.GM.MM.Display("Go Up!");
        } 
    }
}

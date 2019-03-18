using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private AudioSource SoundEffect;
    private Switch SwitchScript;

    private float Stopwatch = 0;
    private float Period = 20;

    void Start () {

        SoundEffect = GetComponent<AudioSource>();
        SwitchScript = GetComponentInParent<Switch>();    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StaticAudio.PlaySound();

            if (gameObject.tag == "YellowCoin")
                SwitchScript.OnCoinPickup();
            else if (gameObject.tag == "BlueCoin")
            {
                GameManager.GM.MM.Display("Congratulations!");
                GameManager.GM.MM.RefreshPeriod = 0;
            }

            Destroy(gameObject);
        }
    }

    public GameObject CoinObject()
    {
        return gameObject;
    }
}

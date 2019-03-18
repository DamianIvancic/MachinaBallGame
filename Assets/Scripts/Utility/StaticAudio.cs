using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAudio : MonoBehaviour {
   
    private static StaticAudio AudioScript;
    private static AudioSource SoundEffect;

	void Start () {

        if (AudioScript == null)
            AudioScript = this;

        if (SoundEffect == null)
            SoundEffect = GetComponent<AudioSource>();
	}

    public static void PlaySound()
    {
        SoundEffect.Play();
    }
}

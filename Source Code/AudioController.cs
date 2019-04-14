using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public AudioClip[] music;

    public AudioClip gong;

    AudioSource audS;

	void Start ()
    {
        audS = GetComponent<AudioSource>();
	}
	
	
	void Update ()
    {
		
	}

    public void ChangeSong()
    {
        audS.clip = music[1];
        audS.Play();
    }

    public void GongSound()
    {
        audS.PlayOneShot(gong);
    }
}

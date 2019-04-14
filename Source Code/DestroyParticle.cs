using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{

    public float time;

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject, time);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

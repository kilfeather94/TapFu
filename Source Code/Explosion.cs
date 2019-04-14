using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Player;
    public float power = 10.0f;
    public float radius = 20.0f;
    public float upforce = 1.0f;



	void Start()
    {
        
	}
	

	void Update ()
    {
		
	}

    public void Blast()
    {
        Vector3 explosionPosition = Player.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null && rb.gameObject.CompareTag("Enemy"))
            {
                // rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
                rb.GetComponent<RagdollManager>().force = 20.0f;
                rb.GetComponent<RagdollManager>().RagdollCharacter();
                rb.GetComponent<RagdollManager>().Explosion_Force();


            }

        }
    }

  
}

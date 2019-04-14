using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

   private ScoreManager sM;

    
    public float fbSpeed;

    public GameObject impactParticle;
	
	void Start ()
    {
        sM = FindObjectOfType<ScoreManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<CapsuleCollider>() && col.gameObject.transform.root.GetComponent<EnemyController>().hit == false)
        {
            col.gameObject.transform.root.GetComponent<EnemyController>().hit = true;
          
            // col.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * force);
            col.gameObject.transform.root.GetComponent<CapsuleCollider>().enabled = false;
            
            col.gameObject.transform.root.GetComponent<EnemyController>().flames[FindObjectOfType<BarScript>().fireballType].SetActive(true);
            col.gameObject.transform.root.GetComponent<Animator>().enabled = false;
            col.gameObject.transform.root.GetComponent<RagdollManager>().force = 8.0f;
            col.gameObject.transform.root.GetComponent<RagdollManager>().RagdollCharacter();
            sM.score++;
            Instantiate(impactParticle, col.gameObject.transform.root.GetComponent<EnemyController>().explodeTransform.position, Quaternion.identity);

            if (FindObjectOfType<BarScript>().fireballType == 0)
            {
             
                Destroy(this.gameObject);
            }
            
            //add speed increase logic

        }
    }
}

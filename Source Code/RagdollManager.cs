using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollManager : MonoBehaviour {

    public Collider[] cols;
    public Rigidbody[] rigids;

    private EnemyController eC;
    private Animator anim;

    public float force;

    bool goRagdoll;

	// Use this for initialization
	void Start ()
    {
        eC = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();

        rigids = GetComponentsInChildren<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();

        foreach(Rigidbody rig in rigids)
        {
            if(rig.gameObject.layer == 9)
            {
                rig.isKinematic = true;
            }
        }

        foreach (Collider col in cols)
        {
            if (col.gameObject.layer == 9)
            {
                col.isTrigger = true;
            }
        }
    }

    public void RagdollCharacter()
    {
         if(!goRagdoll)
        {
            eC.isDead = true;
            eC.StartCoroutine(eC.DieWait()); //newly added
            anim.enabled = false;

            foreach (Rigidbody rig in rigids)
            {
                if (rig.gameObject.layer == 9)
                {
                    rig.isKinematic = false;
                    //rig.AddForce(-transform.forward * force); 
                    rig.AddForce(-transform.forward * force, ForceMode.Impulse);
                }
            }

            foreach (Collider col in cols)
            {
                if (col.gameObject.layer == 9)
                {
                    col.isTrigger = false;
                }
            }


            goRagdoll = true;
        }
    }

    public void Explosion_Force()
    {
        foreach (Rigidbody rig in rigids)
        {
            if (rig.gameObject.layer == 9)
            {
                rig.isKinematic = false;
               rig.AddForce(transform.up + -transform.right * force, ForceMode.Impulse); // was 550f
              
            }
        }
    }

   
     
}

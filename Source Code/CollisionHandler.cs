using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour {

    public float force;

    public AudioClip[] hitSounds;

    private AudioSource audS;

    public GameObject impactParticle;

    private bool hit;

    private GameController gC;
    private ScoreManager sM;
    public BarScript bS;
    private PlayerController pC;

    private bool wasHit;

    float targetValue;


	// Use this for initialization
	void Start ()
    {
		audS = GetComponent<AudioSource>();
        gC = FindObjectOfType<GameController>();
        sM = FindObjectOfType<ScoreManager>();
        pC = FindObjectOfType<PlayerController>();
       
	}
	
	
	void Update ()
    {

       /*
            if (wasHit)
            {

                if (sC.slider.value != targetValue)
                {

                    SliderIncrease(5.0f);
                }

                else
                {
                    // wasHit = false;
                }

                
                //if (sC.slider.value == targetValue)
               // {
               //     wasHit = false;
               // }
              
            }
       */
		
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<CapsuleCollider>() && col.gameObject.transform.root.GetComponent<EnemyController>().hit == false) 
        {
            col.gameObject.transform.root.GetComponent<EnemyController>().hit = true;           
            ContactPoint contact = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
            Vector3 pos = contact.point;         
            Instantiate(impactParticle, pos, rot);

         
          
            int soundIndex = Random.Range(0, hitSounds.Length);
            audS.PlayOneShot(hitSounds[soundIndex], 0.5f);
            col.gameObject.transform.root.GetComponent<CapsuleCollider>().enabled = false;
            col.gameObject.transform.root.GetComponent<Animator>().enabled = false;
            col.gameObject.transform.root.GetComponent<RagdollManager>().force = 8.0f;
            col.gameObject.transform.root.GetComponent<RagdollManager>().RagdollCharacter();

          //  targetValue = sC.slider.value + 5.0f;
            wasHit = true;
            pC.GetComponent<Animator>().SetBool("canHit", true);

           // gC.hitCount++;
            sM.score++;
            //   sM.textmeshPro.SetText()
            bS.SliderIncrease(0.1f);
           
         //   int tmp = gC.hitCount % 20;

         //   if(tmp == 0 || tmp == 10)
           // {
             //   gC.SpeedIncrease();
            //}
         
            
           
         //   col.gameObject.transform.root.GetComponent<Rigidbody>().AddForce(-transform.forward * 300f); // was 550f


        }
    }

   

  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    Rigidbody rb;

    public float force;

    public GameObject player;
    public GameObject[] explosions;
    public GameObject[] flames;

    public Transform explodeTransform;

    public bool isDead = false;
    public bool hit = false;

    Vector3 explodePos;
    Quaternion explodeRot;

    Animator anim;

    private GameController gC;


	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        gC = FindObjectOfType<GameController>();

        if(anim != null && anim.enabled)
        {
            anim.SetFloat("MoveSpeed", gC.enemySpeed);
        }
    }


	void Update ()
    {
        /*
        if(isDead)
        {
            StartCoroutine(DieWait());
        }
        */
    
	}



    public IEnumerator DieWait()
    {
        yield return new WaitForSeconds(3.5f);
   //     explodePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        explodeRot = Quaternion.Euler(-90f, -360f, 0f);
        Instantiate(explosions[0], explodeTransform.position, explodeRot);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.GetComponent<CapsuleCollider>() && col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<Animator>().SetTrigger("hit");
            explodePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            explodeRot = Quaternion.Euler(-90f, -360f, 0f);
            Instantiate(explosions[0], explodePos, explodeRot);
            Instantiate(explosions[1], col.gameObject.transform.position, explodeRot);
            col.gameObject.SetActive(false);
            FindObjectOfType<PlayAd>().playAdv = true;
           // StartCoroutine(FindObjectOfType<PlayAd>().WaitAd());
            gC.ClearEnemies();
            gC.CancelCoroutines();
            gC.gameOver = true;
            UIController uiC = FindObjectOfType<UIController>();
            uiC.DisableUIObjects();
            ScoreManager sM = FindObjectOfType<ScoreManager>();
            sM.SubmitNewPlayerScore(sM.score);
          //  Destroy(this.gameObject);
        
           
        }
    }


    public void InstantKill()
    {
        explodePos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        explodeRot = Quaternion.Euler(-90f, -360f, 0f);
        Instantiate(explosions[0], explodePos, explodeRot);
        Destroy(this.gameObject);
    }

}

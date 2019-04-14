using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{

    Animator anim;
  
    Explosion expl;
    CameraShaker camshk;
    UIController uiC;

    private bool facingRight = true;
    public bool ready;

    private int randomAttack;

    public BoxCollider[] hitboxes;

    public GameObject[] fireballs;
    public Image[] fistIcons;
    public GameObject magic;
    public Transform fb_spawnPos;
    public float fb_speed;

    public int health = 3;
    public float atckTime = 1.0f;

    Vector3 stationaryPos = new Vector3(0f, 0f, 0f);

    Quaternion right = Quaternion.Euler(0, 90, 0);
    Quaternion left = Quaternion.Euler(0, -90, 0);

    //  Quaternion playerRot;


        /// <summary>
        /// Start Fight Position = X: 0, Y: 7.038, Z: 0
        /// Start Fight Rotation = 0, 90, 0
        /// </summary>

    void Start ()
    {
        anim = GetComponent<Animator>();
        expl = GetComponent<Explosion>();
        camshk = Camera.main.GetComponent<CameraShaker>();
        uiC = FindObjectOfType<UIController>();
      
     //   playerRot = transform.rotation;
	}
	

	void Update ()
    {

        AttackTimer();

        if (ready)
        {
          
            //SwipeAction();

            if (Input.GetKeyDown(KeyCode.F)) //fireball
            {
                anim.SetTrigger("fireball");
            }

            if (Input.GetKeyDown(KeyCode.T)) //quake
            {
                anim.SetTrigger("blast");
            }


            //    transform.position = stationaryPos;



            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                facingRight = false;
                anim.SetInteger("moveInt", 2);
                anim.SetTrigger("attack");
                DisableFistIcon(0);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                facingRight = true;
                anim.SetInteger("moveInt", 2);
                anim.SetTrigger("attack");
                DisableFistIcon(1);
            }




            if (facingRight)
            {
                transform.rotation = right;
                // transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                transform.rotation = left;
                // transform.rotation = Quaternion.Euler(0, -90, 0);
            }



            if (Input.touchCount == 1)
            {

                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                {
                   // Debug.Log("Touched UI");
                }

                else
                {

                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        var touch = Input.touches[0];

                        if (touch.position.x < Screen.width / 2)
                        {
                            facingRight = false;
                            anim.SetInteger("moveInt", 2);
                            // MovePick();
                            anim.SetTrigger("attack");

                            DisableFistIcon(0);

                        }
                        else if (touch.position.x > Screen.width / 2)
                        {
                            facingRight = true;
                            anim.SetInteger("moveInt", 2);
                            // MovePick();
                            anim.SetTrigger("attack");

                            DisableFistIcon(1);
                        }
                    }
                }

            }

        }

     
	}

    private void DisableFistIcon(int iconIndex)
    {
        if (fistIcons[iconIndex].IsActive() && !fistIcons[iconIndex].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Fist_FadeOut"))
        {
            fistIcons[iconIndex].GetComponent<Animator>().SetTrigger("fade");
        }
    }

    private void AttackTimer()
    {
        if(anim.GetBool("canHit") == true)
        {
            atckTime -= Time.deltaTime * 15f; // was 20
        }

        if(atckTime <= 0f)
        {
            anim.SetBool("canHit", false);
            atckTime = 1.0f;
        }
    }


    private void MovePick()
    {
        int randomNum = Random.Range(1, 3);
        anim.SetInteger("moveInt", randomNum);
    }

 
    /*
   public void OnTouchDown(bool directionFace)
    {
        facingRight = directionFace;
        anim.SetInteger("moveInt", 2);
        // MovePick();
        anim.SetTrigger("attack");
    }
    */
    

    private void DisableColliders()
    {
        foreach (BoxCollider cols in hitboxes)
        {
            cols.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    public void MyAnimationEventHandler (AnimationEvent animEvent)
    {
        string stringParam = animEvent.stringParameter;
        int intParam = animEvent.intParameter;
       

        switch (stringParam)
        {
            case "Right Foot":  // right foot col
                if (intParam == 1) // true
                {
                    hitboxes[0].gameObject.GetComponent<BoxCollider>().enabled = true;
                    hitboxes[1].gameObject.GetComponent<BoxCollider>().enabled = false;
                    hitboxes[2].gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else if(intParam == 0) // false
                {
                    DisableColliders();
                }
                break;
            case "Left Foot": // left foot col
                if (intParam == 1)
                {
                    hitboxes[1].gameObject.GetComponent<BoxCollider>().enabled = true;
                    hitboxes[0].gameObject.GetComponent<BoxCollider>().enabled = false;
                    hitboxes[2].gameObject.GetComponent<BoxCollider>().enabled = false;
                    
                }
                else if (intParam == 0)
                {
                    DisableColliders();
                   
                }
                break;
            case "Right Hand": // right hand col
                if (intParam == 1)
                {
                    hitboxes[2].gameObject.GetComponent<BoxCollider>().enabled = true;
                    hitboxes[1].gameObject.GetComponent<BoxCollider>().enabled = false;
                    hitboxes[0].gameObject.GetComponent<BoxCollider>().enabled = false;
               //     print("Right hand enabled");
                }
                else if (intParam == 0)
                {
                    DisableColliders();
                 //   print("Right hand disabled");
                }
                break;


        }
    }


   
    public void Fireball_Event()
    {
        BarScript bS = FindObjectOfType<BarScript>();

        GameObject projectile = Instantiate(fireballs[bS.fireballType], fb_spawnPos.position, Quaternion.Euler(transform.right)) as GameObject;
        
        // fireball.GetComponent<Rigidbody>().AddForce(fireball.transform.right * fb_speed);
        if (facingRight)
        {
           projectile.GetComponent<Rigidbody>().AddForce(fireballs[bS.fireballType].transform.right * projectile.GetComponent<Fireball>().fbSpeed);
        }
        else
        {
            projectile.GetComponent<Rigidbody>().AddForce(-fireballs[bS.fireballType].transform.right * projectile.GetComponent<Fireball>().fbSpeed);
        }
    }

    public void Quake_Event()
    {
        GameObject blast = Instantiate(magic, transform.position, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
        expl.Blast();
        camshk.shouldShake = true;

    }


    /*
    void SwipeAction()
    {

        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0  && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Up;
                    anim.SetTrigger("blast");
                    // Swipe down
                } else if (currentSwipe.y < 0  && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                    swipeDirection = Swipe.Down;
                    // Swipe left
                } else if (currentSwipe.x < 0 &&  currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Left;
                    // Swipe right
                } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                    swipeDirection = Swipe.Right;
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }

       
    }
    */

   
}

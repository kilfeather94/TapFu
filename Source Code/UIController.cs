using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour

{

    Animator anim;

    public GameObject FistsImg;
    public GameObject SliderObj;
    public GameObject ScoreTextObj;

    public GameObject gameOverMenu;



	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	void Update ()
    {
		
	}

    public void Reactivate()
    {

    }


    public void CloseMenu()
    {
        if (anim != null)
        {
            anim.SetTrigger("action");
        }
        FistsImg.SetActive(true);
        
        SliderObj.SetActive(true);
        ScoreTextObj.SetActive(true);

        if(gameOverMenu != null && gameOverMenu.activeSelf)
        {
            gameOverMenu.SetActive(false);
        }
    }

    public void DisableUIObjects()
    {
        if(FistsImg.activeSelf)
        {
            FistsImg.SetActive(false);
        }

        if(SliderObj.activeSelf)
        {
            SliderObj.SetActive(false);
        }

        if(ScoreTextObj.activeSelf)
        {
            ScoreTextObj.SetActive(false);
        }
    }

  
}

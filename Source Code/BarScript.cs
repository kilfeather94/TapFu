using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    //[SerializeField]
    public float fillAmount;

    [SerializeField]
    private float lerpSpeed;

    [SerializeField]
    private Image content;

    public Button[] sliderButtons;

    public int fireballType;


    void Start ()
    {
		
	}

	void Update ()
    {
        HandleBar();

        ButtonCheck();
	}

   private void HandleBar()
    {
        if(fillAmount != content.fillAmount)
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }

    }


    public void SliderIncrease(float sliderVal)
    {
        fillAmount += sliderVal;
    }

    public void DrainBar()
    {
        if (fillAmount > 0f)
        {
            fillAmount -= fillAmount;  // Empty whole bar if any ability is used

            foreach (Button btn in sliderButtons)
            {
                btn.interactable = false; //every button disabled
            }
        }
    }

    void ButtonCheck()
    {
        if(fillAmount >= 0.29f)
        {
            sliderButtons[0].interactable = true; // blue fireball button active
        }
      

        if(fillAmount >= 0.59f)
        {
            sliderButtons[1].interactable = true; // green fireball button active
        }
       

        if (fillAmount >= 1f)
        {
            sliderButtons[2].interactable = true; // quake button active
        }
       
    }

    public void FireBall(int fbType)
    {
        fireballType = fbType;
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetTrigger("fireball");
        DrainBar();
    }

    public void Quake()
    {
        FindObjectOfType<PlayerController>().GetComponent<Animator>().SetTrigger("blast");
        DrainBar();
    }


}

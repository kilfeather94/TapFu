using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{


   public Slider slider;

    public int sliderPoints;
    public int SliderLevel;

    public enum AbilityLevel { fireBall_small = 1, fireBall_large = 2, Quake = 3 };

    public AbilityLevel Levels;


    private int variable, actualVariable = 5;


    public float targetValue;

    
	
	void Start ()
    {
        slider = GetComponent<Slider>();
	}

	void Update ()
    {
        //  SliderIncrease();
      

      
	}

    
    public void SliderIncrease(float sliderVal)
    {
     
      
    }
    


}

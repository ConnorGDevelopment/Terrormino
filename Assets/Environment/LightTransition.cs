using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTransition : MonoBehaviour
{

    public Light MoonLight;

    public float TimeTillBright = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        AdjustingMoonlight();
    }



    // Update is called once per frame
    void Update()
    {
        
    }


    private void AdjustingMoonlight()
    {
        while(MoonLight.intensity < 0.2)
        {
            MoonLight.intensity = Time.deltaTime * TimeTillBright;
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGrabbables : MonoBehaviour
{

    public GameObject HandheldReset;
    public GameObject FlashlightReset;

   

    public Collider DetectionBox;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnTriggerExit(Collider other)
    {
        if (other == DetectionBox)
        {
            if(this.CompareTag("Handheld"))
            {
                this.transform.position = HandheldReset.transform.position;
            }
            else if (this.CompareTag("Flashlight"))
            {
                this.transform.position = FlashlightReset.transform.position;
            }
        }
    }


}

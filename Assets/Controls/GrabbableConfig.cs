using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableConfig : MonoBehaviour
{


    public XRGrabInteractable XRGrabRef;
    public Transform LeftAttach;
    public Transform RightAttach;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void GrabConsole(InputAction inputAction)
    {

        int Grabbed = Helpers.Math.RoundNearestNonZeroInt(inputAction.ReadValue<float>());

        XRGrabRef.attachTransform = LeftAttach;
        XRGrabRef.secondaryAttachTransform = RightAttach;

        if(Grabbed <= 0)
        {
            XRGrabRef.attachTransform = RightAttach;
            XRGrabRef.secondaryAttachTransform = LeftAttach;
        }
        
        
        

    }



    


    








}

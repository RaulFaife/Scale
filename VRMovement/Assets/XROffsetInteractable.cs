using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetInteractable : XRGrabInteractable
{
    //this script inherits from grab interactable. the point is that it gets rid of the snapping and just
    //makes the grabable stick to where you grabbed it
    //to use it, replace default XR Toolkit XRGrabInteractable on a grabable object  with this script
    private Vector3 initialAttachLocalPos;
    private Quaternion initialAttachLocalRot;
    void Start()
    {
        if(!attachTransform){
            GameObject grab = new GameObject("Grab Pivot");
            grab.transform.SetParent(transform,false);
            attachTransform = grab.transform;
        }
        initialAttachLocalPos = attachTransform.localPosition;
        initialAttachLocalRot = attachTransform.localRotation;
    }

    // we overriding the script called when we touch an object lol
    protected override void OnSelectEnter(XRBaseInteractor interactor){
        if(interactor is XRDirectInteractor){
            attachTransform.position = interactor.transform.position;
            attachTransform.rotation = interactor.transform.rotation;

        }else{
            attachTransform.localPosition = initialAttachLocalPos;
            attachTransform.localRotation = initialAttachLocalRot;
        }
        base.OnSelectEnter(interactor);
    }
}

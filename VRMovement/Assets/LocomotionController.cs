using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{

    public XRController leftTeleportRay;
    public XRController rightTeleportRay;
    public InputHelpers.Button teleportActivationButton;
    public float activationThreshold = 0.1f;

    //as shown in a YT comment, to avoid the area being deactivated as I teleport if i let go.
    private XRRayInteractor rightRayInteractor;
    private XRRayInteractor leftRayInteractor;

    //to avoid teleporting when i am making contact with UI
    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;

    
    //so that teleport is only possible if nothing is held.
    public bool EnableLeftTeleport {get; set;} = true;
    public bool EnableRightTeleport {get; set;} = true;

    private void Start()
    {
        

        if(rightTeleportRay)
            rightRayInteractor = rightTeleportRay.gameObject.GetComponent<XRRayInteractor>();
        if(leftTeleportRay)
            leftRayInteractor = leftTeleportRay.gameObject.GetComponent<XRRayInteractor>();
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 pos = new Vector3();
        Vector3 norm = new Vector3();
        int index = 0;
        bool validTarget = false;

        if(leftTeleportRay){
            //ref means variable will be changed in function.
            //isLeftInteractorRayHovering to know if we are hovering over UI.
            bool isLeftInteractorRayHovering = leftInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            leftRayInteractor.allowSelect = CheckIfActivated(leftTeleportRay);
            leftTeleportRay.gameObject.SetActive(EnableLeftTeleport && CheckIfActivated(leftTeleportRay) && !isLeftInteractorRayHovering);
        }
        if(rightTeleportRay){
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(ref pos, ref norm, ref index, ref validTarget);
            rightRayInteractor.allowSelect = CheckIfActivated(rightTeleportRay);
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }
    public bool CheckIfActivated(XRController controller){
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}

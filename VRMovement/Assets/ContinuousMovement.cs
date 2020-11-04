using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    //controller
    public XRNode inputSource;
    public float speed = 1;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight;
    
    private float fallingSpeed = -9.81f;
    private Vector2 inputAxis;
    private CharacterController character;
    private XRRig rig;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        //get vector 2d from trackpad.
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        //print(inputAxis);
    }
    private void FixedUpdate() {

        CapsuleFollowHeadset();

        //getting rotation of the camera and applying movement.
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y,0 );
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * Time.fixedDeltaTime * speed);

        //falling with acceleration, should it be disabled in space?
        //uses a Spherecast in checkifGrounded function.
        bool isGrounded = CheckIfGrounded();
        if (isGrounded){
            fallingSpeed = 0;
        }else{
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        fallingSpeed += gravity * Time.fixedDeltaTime;
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }
    bool CheckIfGrounded(){
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }

    //this function makes it so that character controller moves along with the headset moving in space.
    //allows for crouching too since it changes height.
    void CapsuleFollowHeadset(){
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height/2 + character.skinWidth ,capsuleCenter.z);
    }
}

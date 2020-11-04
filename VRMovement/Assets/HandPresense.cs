using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresense : MonoBehaviour
{
    
    public bool showController = false;
    //to hold all possible controllers
    public List<GameObject> controllerPrefabs;
    public InputDeviceCharacteristics controllerCharacteristics;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModels;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start(){
        TryInitialize();
    }

    void TryInitialize(){

         //creates a list of input devices. fills it in with devices that meet this criteria, why no left though? 
        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller; 
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices); 
        foreach (var item in devices) { 
            Debug.Log(item.name + item.characteristics); 
        } 
        //picks first one, then searches through our list of prefab models, for the controller taht matches the device!!!
        if(devices.Count > 0) { 
            targetDevice = devices[0]; 
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab){
                spawnedController = Instantiate(prefab, transform);
            }else{
                Debug.Log("Didn't find corresponding controller model.");
                //picks default.
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
            spawnedHandModels = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModels.GetComponent<Animator>();
        } 
    }

    //reminder that "out" creates a variable on the parameter that is populated in the method and it can be used outside with its new assigned value.
    void UpdateHandAnimation(){
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)){
            //this refers to Trigger in the Animator value
            handAnimator.SetFloat("Trigger", triggerValue);
        }else{
            handAnimator.SetFloat("Trigger", 0);
        }
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float gripValue)){
            handAnimator.SetFloat("Grip", gripValue);
        }else{
            handAnimator.SetFloat("Grip", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue) 
        //    Debug.Log("Pressing Primary Button"); 
        //if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        //    Debug.Log("Trigger pressed" + triggerValue); 
        //if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        //    Debug.Log("Primary Touchpad " + primary2DAxisValue); 

        if(!targetDevice.isValid){
            TryInitialize();
            //we do this to always check, in case the device dies.

        }else{

            if(showController){
                spawnedHandModels.SetActive(false);
                spawnedController.SetActive(true);
            }else{
                spawnedHandModels.SetActive(true);
                spawnedController.SetActive(false);
                //yuo can add more inputs and animations!!!
                UpdateHandAnimation();
            }
        }

        
    }
}

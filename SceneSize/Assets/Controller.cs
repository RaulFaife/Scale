using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private Controls controls;
    //public PlayerInput playerinput;

    public int upperlimit;
    public int lowerlimit;
    public int byUnits; 

    private void Awake() {
        controls = new Controls();
        //playerinput = GetComponent<PlayerInput>();

        //print(playerinput.currentActionMap);
    }
    private void Start() {
        controls.Player.Shrink.started += _ => OnShrink();
        controls.Player.Grow.started += _ => OnGrow();
    }
    void OnShrink(){
        //print("shrink");
        //if (transform.localScale.x > 1){
        //    transform.localScale = new Vector3(transform.localScale.x-byUnits,transform.localScale.y-byUnits,transform.localScale.z-byUnits);
        //}
        byUnits = SceneSwitchingManager.instance.changeSize(false);
        if(byUnits>0){
            transform.localScale = new Vector3(transform.localScale.x-byUnits,transform.localScale.y-byUnits,transform.localScale.z-byUnits);
        }else if(byUnits == 0 ){
            //request max value of new level of sceneswitchmanager
            byUnits = SceneSwitchingManager.instance.getLevelMaxScale();
            transform.localScale = new Vector3(byUnits,byUnits,byUnits);

        }
    }
    void OnGrow(){
        //print("grow");
        //if (transform.localScale.x < 100){
        //    transform.localScale = new Vector3(transform.localScale.x+byUnits,transform.localScale.y+byUnits,transform.localScale.z+byUnits);
        //}
        byUnits = SceneSwitchingManager.instance.changeSize(true);
        if(byUnits>0){
            transform.localScale = new Vector3(transform.localScale.x+byUnits,transform.localScale.y+byUnits,transform.localScale.z+byUnits);
        }else if(byUnits == 0 ){
            transform.localScale = new Vector3(1,1,1);
        }
    }
    void sceneChangeTeleport(Transform position){
        
    }

}

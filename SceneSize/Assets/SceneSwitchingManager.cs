using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneSwitchingManager : MonoBehaviour
{
    public SceneDatabase scenes;
    public int CurrentLevelIndex = 0;
    public int CurrentStepIndex = 0;

    public static SceneSwitchingManager instance;
    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
    }

    //private void Start() {
    //    foreach (Level scene in scenes.allLevels){
    //        scene.startingPosition = 
    //    }
    //}
    //Load a scene with a given index
    public void LoadLevelWithIndex(int index)
    {
        if (index <= scenes.allLevels.Count)
        {
            //Load Gameplay scene for the level
            //SceneManager.LoadSceneAsync("Gameplay" + index.ToString());
            //Load first part of the level in additive mode
            //SceneManager.LoadSceneAsync("Level" + index.ToString() + "Part1", LoadSceneMode.Additive);

            LoadScene(scenes.allLevels[index].sceneName);
            

        }
        //reset the index if we have no more levels
        else CurrentLevelIndex =1;
    }
    //Start next level


    //make the decision for the player if they will move to a different scene or not
    //returns next scaling number , or 0 meaning changing scene up, or -1 meaning no change since it would go out of bounds!
    public int changeSize(bool growth){
        int step;
        if (growth){
            if (CurrentStepIndex  == scenes.allLevels[CurrentLevelIndex].steps.Count - 1){
                
                if(CurrentLevelIndex == scenes.allLevels.Count - 1){
                    //will be going out of bounds! did NOT change level
                    return -1;
                }
                //do load next level and unload this level.
                NextLevel();
                
                //reset step
                CurrentStepIndex = 0;
                //return to signal a reset of scale
                step = 0;
            }else{
                //or just increase step
                CurrentStepIndex++;
                //return scaling number
                step = scenes.allLevels[CurrentLevelIndex].steps[CurrentStepIndex];
            }
        }else{
            
            //set max step for new level
            
            if (CurrentStepIndex  == 0){

                if(CurrentLevelIndex == 0){
                    //will be going out of bounds! did NOT change level
                    return -1;
                }
                //do load previuos level
                PreviousLevel();

                //reset step to last possible
                CurrentStepIndex = scenes.allLevels[CurrentLevelIndex].steps.Count - 1;
                //return to signal a reset of scale
                step = 0; //scenes.allLevels[CurrentLevelIndex].maxScale-1;
            }else{
                //return scaling number
                step = scenes.allLevels[CurrentLevelIndex].steps[CurrentStepIndex];
                //or just decrease step
                CurrentStepIndex--;
            }
            
        }
        return step;
    }
    public int getLevelMaxScale(){
        return scenes.allLevels[CurrentLevelIndex].maxScale;
    }
    public Transform getLevelSpawnPoint(){
        return scenes.allLevels[CurrentLevelIndex].startingPosition;
    }

    public void NextLevel()
    {
        CurrentLevelIndex++;
        LoadLevelWithIndex(CurrentLevelIndex);
        UnLoadScene(scenes.allLevels[CurrentLevelIndex-1].sceneName);
    }
    public void PreviousLevel()
    {
        CurrentLevelIndex--;
        LoadLevelWithIndex(CurrentLevelIndex);
        UnLoadScene(scenes.allLevels[CurrentLevelIndex+1].sceneName);
    }
    //Restart current level
    public void RestartLevel()
    {
        LoadLevelWithIndex(CurrentLevelIndex);
    }
    //New game, load level 1
    public void NewGame()
    {
        LoadLevelWithIndex(1);
    }
    

     /*
     * Menus
     
    //Load main Menu
    public void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Main_Menu].sceneName);
    }
    //Load Pause Menu
    public void LoadPauseMenu()
    {
        SceneManager.LoadSceneAsync(menus[(int)Type.Pause_Menu].sceneName);
    }
    */
 
    //from the video https://blogs.unity3d.com/2020/07/01/achieve-better-scene-workflow-with-scriptableobjects/
    //should be in a trigger, but what if I just have it here??? S
    //how do i know if the scene i am on is loaded or not?
    //CEHCK OUT THE MANUAL, Get the active scene name and then check isLoaded() Easy! Might have some overhead though until you have a more elegant solution.
    //SceneManager.GetSceneByName(string).isLoaded
    //that's literally how the wolf video guy does it lmao.
    
    public void LoadScene(string sceneName)
    {
        //Scene checking = SceneManager.GetSceneByName(sceneName);
        //if(checking.isLoaded){
        if(!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
    }    
        
    public void UnLoadScene(string sceneName)
    {    
        //Scene checking = SceneManager.GetSceneByName(sceneName);
        //if(checking.isLoaded){
        if(SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
        
    }

}

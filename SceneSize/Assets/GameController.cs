using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{

    public static GameController Instance { get; private set; }
    private void Awake() {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //Load the player character prefab and the first level
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

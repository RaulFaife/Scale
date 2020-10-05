using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //By adding this line of code we can now use all the SceneManagement related Functions

public class Test : MonoBehaviour
{
    //To set up autocomplete with Unity:
    //1) go to unity3d -> go to edit -> go to preferences -> go to external tools -> Make sure external script editor is visual studio
    //2) then go to visual studio -> tools -> options -> tools for unity -> Miscellaneous -> Access to Project Properties (set it to true) -> Restart visual studio and you all good :)

    //Void just means the function doesn't return any useable value
    //Putting Private on a variable or function make debugging a lot easier. For when you put public you will see the variable on the Unity Engine
    //A float is a variable that can contain whole numbers AND fractions


    Rigidbody rb;
    public GameObject winText;
    float xInput;
    float zInput;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(gameObject);
        //Destroy(gameObject, 3f);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Destroy(gameObject);

            //rb.AddForce(Vector3.up * 500);
        }

        //rb.velocity = Vector3.forward * 20f;

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Level2");
        }

        xInput = Input.GetAxis("Horizontal");
        zInput = Input.GetAxis("Vertical");

        //AddForce(x,y,z);
        rb.AddForce(xInput * speed, 0, zInput * speed);
    }

    /* Below is to destroy the block on mouse down
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy") //Make sure to add another gameObject and give it a tag name of "Enemy"
        {
            //Destroy(gameObject);
            //Destroy(collision.gameObject);

            winText.SetActive(true);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetColorRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }

    public void SetColorBlue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}

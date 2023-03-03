using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMachine : MonoBehaviour
{
    private string orangeJuice;
    private string grapeJuice;
    private string strawberryJuice;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("You buy " + orangeJuice);
        }
    }
}

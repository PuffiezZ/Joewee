using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPositon;
    private void Update()
    {
        transform.position = cameraPositon.position;
    }
}

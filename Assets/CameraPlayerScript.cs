using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerScript : MonoBehaviour
{
    public Transform cameraPos;

    public void Start(){
        transform.position = cameraPos.position;
    }

    public void Update()
    {
        transform.position = cameraPos.position;
    }
}

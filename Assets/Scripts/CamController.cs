using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

   public GameObject cameraTarget = null;

    void Start()
    {
        cameraTarget = FindObjectOfType<PlayerController>().gameObject;
    }

}

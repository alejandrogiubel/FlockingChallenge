using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAway : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("LB Joystick")) {
            transform.GetComponent<Camera> ().orthographicSize = 15;
        }
        if (Input.GetButtonUp("LB Joystick")) {
            transform.GetComponent<Camera> ().orthographicSize = 10;
        }
    }
}

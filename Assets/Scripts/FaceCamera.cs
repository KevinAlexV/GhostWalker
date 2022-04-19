using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    [SerializeField] private Camera cam;
    void Update() {
        try {
            transform.LookAt(cam.transform);
        } catch (System.Exception e) { Debug.Log("Camera Look Error: " + e.Message + "/n" + e.StackTrace); }
    }
}

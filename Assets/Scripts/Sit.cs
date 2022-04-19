using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour {
    void Start() {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isSitting", true);
    }
    
}

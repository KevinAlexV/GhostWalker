using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAdvertisement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(60f + Random.Range(-1f, 1f), 0, -60f);
    }
}

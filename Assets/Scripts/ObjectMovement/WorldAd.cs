using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldAd : MonoBehaviour
{
    private float RotateSpeed = 5f;
    private float Radius = 0.1f;

    private Vector3 _centre;
    private float _angle;

    private void Start()
    {
        _centre = this.transform.position;
    }

    private void Update()
    {

        _angle += RotateSpeed * Time.deltaTime;

        var offset = new Vector3(Mathf.Sin(_angle) * Radius, Mathf.Cos(_angle) * Radius,  0) ;
        this.transform.position = _centre + offset;
    }
}
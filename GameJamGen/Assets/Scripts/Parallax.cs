using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float parallax;
    public Camera cam;
    public float startZ;
    public Transform subject;
    Vector2 startPosition;
    Vector2 travel =>(Vector2)cam.transform.position - startPosition;
    Vector2 parallaxFactor;

    void Start()
    {

        startPosition = transform.position;
        startZ = transform.position.z;

    }


    void FixedUpdate()
    {
        
        transform.position = startPosition  + travel * parallax;

    }
}

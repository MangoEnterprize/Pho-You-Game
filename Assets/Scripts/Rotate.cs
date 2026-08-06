using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //  VARIABLES GO HERE (At the class level)
    [SerializeField] private float speedX = 100f;
    [SerializeField] private float speedY = 0f;
    [SerializeField] private float speedZ = 0f;

    void Update()
    {
        //  USE THEM HERE (Inside the methods)
        transform.Rotate(new Vector3(speedX, speedY, speedZ) * Time.deltaTime);
    }
}
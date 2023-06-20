using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollorOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collosion)
    {
        Color rsColor = RCSelector();
        GetComponent<Renderer>().material.color = rsColor;
    }

    private Color RCSelector()
    {
        return new Color( r: Random.Range(0.0f,1.0f), g: Random.Range(0.0f,1.0f),b: Random.Range(0.0f,1.0f));
    }
}

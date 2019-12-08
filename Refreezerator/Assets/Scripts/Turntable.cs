using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        this.transform.Rotate(new Vector3(0,Time.deltaTime * speed,0));
    }
}

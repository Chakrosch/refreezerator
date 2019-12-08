using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public Vector3 speed = new Vector3(0f,1f,0f);

    void Update()
    {
        this.transform.Rotate(Time.deltaTime*speed);
    }
}

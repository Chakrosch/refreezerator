using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviour
{
    public Rigidbody rb;
    public float temperature;
    public bool inFridge;
    public float timeToChangeState;
    public float maxFreezeTime;
    public float currenFreezeTime;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setTemperature();
    }

    public void setInvisible(bool invisible)
    {
        GetComponent<MeshRenderer>().enabled = !invisible;
    }

    public void setRigidbody(bool on)
    {
        GetComponent<Collider>().enabled = on;
        GetComponent<Rigidbody>().useGravity = on;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    protected void setTemperature()
    {
        if (inFridge)
        {
            if (temperature < 1)
            {
                temperature += Time.deltaTime / timeToChangeState;
            }
            else
            {
                currenFreezeTime = maxFreezeTime;
            }
        }
        else
        {
            if (currenFreezeTime <= 0)
            {
                if (temperature > 0)
                {
                    temperature -= Time.deltaTime / timeToChangeState;
                }
            }
            else
            {
                currenFreezeTime -= Time.deltaTime;
            }
        }
    }
}

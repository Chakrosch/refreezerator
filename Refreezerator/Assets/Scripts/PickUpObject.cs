using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObject : MonoBehaviour
{
    public Rigidbody rb;
    public float temperature = 0;
    public bool inFridge;
    public float timeToChangeState;
    public float maxFreezeTime;
    public float currentFreezeTime;
    public bool isFrozen;
    public new SpriteRenderer renderer;
    public GameObject ice;

    private float distance = 0.6f;

    
    private bool isFlying;

    // Start is called before the first frame update
    public void Start()
    {
        setUpIce();
        setFreeze(false);
        isFrozen = false;
    }

    // Update is called once per frame

    void Update()
    {
        setTemperature();

        setFlying();
    }

    public void setInvisible(bool invisible)
    {
        renderer.enabled = !invisible;
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
            if (temperature > 0)
            {
                temperature -= Time.deltaTime / timeToChangeState;
            }
            else if (temperature < 0 && !isFrozen)
            {
                temperature = 0;
                currentFreezeTime = maxFreezeTime;
                isFrozen = true;
            }
        }
        else
        {
            if (currentFreezeTime <= 0)
            {
                if (isFrozen)
                {    
                    setFreeze(false);
                    isFrozen = false;
                }
                if (temperature < 1)
                {
                    temperature += Time.deltaTime / timeToChangeState;
                }
            }
            else
            {
                currentFreezeTime -= Time.deltaTime;
            }
        }
    }

    private void setUpIce()
    {
        ice = Instantiate(ice, Vector3.zero, Quaternion.identity);
        ice.transform.SetParent(this.transform);
        ice.transform.localPosition = Vector3.zero;
        ice.transform.localRotation = Quaternion.Euler(45.0f, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        print(isFrozen);
        if (isFrozen && other.CompareTag("enemy"))
        {
            Stun stun = other.GetComponent<Stun>();
            print(stun);
            if (stun)
            {
                stun.stun();
            }
        }
    }


    public void setFlying()
    {
        if (isFlying)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, Vector3.down, Color.blue, 0.2f);
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.collider.CompareTag("floor") && hit.distance < distance)
                {
                    gameObject.layer = 8;
                    isFlying = false;
                }
            }
        }
    }

    public void setFreeze(bool freezed)
    {
        foreach (var render in ice.GetComponentsInChildren<SpriteRenderer>())
        {
            render.enabled = freezed;
        }
    }
    
    
    public void throwObject()
    {
        gameObject.layer = 11;
        isFlying = true;
    }
}

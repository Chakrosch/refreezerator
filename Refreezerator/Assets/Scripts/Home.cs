using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    private static int veggieCount = 0;

    public GameObject levelBlock1;
    public GameObject levelBlock1a;
    public GameObject levelBlock2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int getVeggie()
    {
        return veggieCount;
        
    }

    public static void resetVeggieCount()
    {
        veggieCount = 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        Vegetable veggie = other.gameObject.GetComponent<Vegetable>();
        if (veggie)
        {
            veggieCount++;
            Destroy(other.gameObject);
            
        }

        switch (veggieCount)
        {
            case 1:
                Destroy(levelBlock1);
                Destroy(levelBlock1a);
                break;
            case 2:
                Destroy(levelBlock2);
                break;
            default:
                break;
        }

        {
            
        }
    }
}

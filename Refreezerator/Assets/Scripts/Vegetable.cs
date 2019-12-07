using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : PickUpObject
{
    public enum vegetables
    {

    }
    public vegetables type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.setTemperature();
        if (temperature > 0)
        {
            Debug.Log("Vegtabletemp" + this.name + " " + temperature);
            Debug.Log("color: " + gameObject.GetComponent<Renderer>().material.color.a);
            var col = gameObject.GetComponent<Renderer> ().material.color;
            col.a = temperature;
            this.GetComponent<MeshRenderer>().material.color = 
                new Color(
                    gameObject.GetComponent<Renderer>().material.color.r,
                    gameObject.GetComponent<Renderer>().material.color.g,
                    temperature,
                    gameObject.GetComponent<Renderer>().material.color.a);

        }
    }
}

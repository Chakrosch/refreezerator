using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : PickUpObject
{
    public enum vegetables
    {
        carrot,
        broccoli,
        eggplant,
        corn,
        tomato,
        paprika
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
            Debug.Log("color: " + renderer.color.a);
            var col = renderer.color;
            col.a = temperature;
            renderer.color = 
                new Color(
                    temperature / 2 + 0.5f,
                    renderer.color.g,
                    renderer.color.b,
                    renderer.color.a);

        }
    }
}

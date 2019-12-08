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
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.setTemperature();
        base.setFlying();
        if (temperature > 0)
        {
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

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}

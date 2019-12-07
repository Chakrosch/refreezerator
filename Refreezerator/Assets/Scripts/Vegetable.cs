using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : PickUpObject
{

    private bool isFlying;
    public float distance;
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
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.setTemperature();
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

        if (isFlying)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, distance))
            {
                if (hit.collider.tag == "floor")
                {
                    gameObject.layer = 8;
                } else if (hit.collider.tag == "enemy")
                {
                    Stun stun = hit.collider.GetComponent<Stun>();
                    if (stun)
                    {
                        stun.stun();
                    }
                }
            }
        }
    }

    public void throwVegetable()
    {
        gameObject.layer = 11;
        isFlying = true;
    }
}

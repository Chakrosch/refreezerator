using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public float timeToChangeState;
    private bool isStunned;

    private float getSpeed;

    private float stunTime;
    private new SpriteRenderer renderer;


    private void Start()
    {
        stunTime = 0;
        isStunned = false;
        renderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime / timeToChangeState;
        }
        else if (isStunned)
        {
            isStunned = false;
            renderer.color = Color.white;
            if (GetComponent<Lizard>() != null)
            {
                GetComponent<Lizard>().agent.speed = getSpeed;
            }
            else if (GetComponent<Lizard_Big>() != null)
            {
               GetComponent<Lizard_Big>().agent.speed = getSpeed;
            }
        }
    }
    
    public void stun()
    {
        print("STUNNED");
        stunTime = 1;
        isStunned = true;
        renderer.color = Color.black;
        if(GetComponent<Lizard>() != null)
        {
          getSpeed = GetComponent<Lizard>().agent.speed;
          GetComponent<Lizard>().agent.speed = 0;
        }
        else if(GetComponent<Lizard_Big>() != null)
        {
           getSpeed = GetComponent<Lizard_Big>().agent.speed;
            GetComponent<Lizard_Big>().agent.speed = 0;
        }

    }


    public bool getStunned()
    {
        return isStunned;
    }

}
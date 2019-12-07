using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Stun : MonoBehaviour
{
    public float timeToChangeState;
    private bool isStunned;

    private float stunTime;
    private SpriteRenderer renderer;


    private void Start()
    {
        stunTime = 0;
        isStunned = false;
        renderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isStunned)
        {
            print(stunTime);
        }
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime / timeToChangeState;
        }
        else if (isStunned)
        {
            isStunned = false;
            renderer.color = Color.white;
        }
    }
    
    public void stun()
    {
        print("STUNNED");
        stunTime = 1;
        isStunned = true;
        renderer.color = Color.black;

    }


    public bool getStunned()
    {
        return isStunned;
    }

}
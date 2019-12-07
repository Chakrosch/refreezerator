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
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime / timeToChangeState;
        }
        else if (isStunned)
        {
            isStunned = false;
            this.GetComponent<Lizard>().enabled = true;
            this.GetComponent<Lizard_Big>().enabled = true;
            renderer.color = Color.white;
        }
    }
    
    public void stun()
    {
        stunTime = 1;
        this.GetComponent<Lizard>().enabled = false;
        this.GetComponent<Lizard_Big>().enabled = false;
        renderer.color = Color.gray;
        
    }
}
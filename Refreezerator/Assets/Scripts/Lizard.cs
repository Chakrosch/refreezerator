﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lizard : PickUpObject
{
    public NavMeshAgent agent;
    Stun stun;
    Vegetable nearestVeg;
    public float roamRadius;
    public float counter;
    public Transform target;
    public float seeRange;
    public static PlayerController player;
    private float countdown;
    private bool finished;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = this.GetComponent<NavMeshAgent>();
        stun = this.GetComponent<Stun>();
        agent.updateRotation = false;
        agent.speed = 0f;
        rb.useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {if (countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
    if(countdown <= 0 && !finished)
        {
            finished = true;
            agent.speed = 3.5f;
            GetComponent<Collider>().enabled = true;
            rb.useGravity = true;
        }
        CheckNearby();
        DoMovement();
        if(counter > 0)
        {
            counter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            if (collision.gameObject.GetComponent<PickUpObject>().isFrozen)
            {
                stun.stun();
            }
            else
            {
                BofrostMachine.instance.allTheVeggies.Remove(collision.gameObject);
                Destroy(collision.gameObject);
                Instantiate(PrefabManager.instance.getBigLiz(), transform.position + transform.forward, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    void CheckNearby()
    {
        int smallestIndex = 0;
        float smallestDist = Mathf.Infinity;
        for (int i = 0; i < BofrostMachine.instance.allTheVeggies.Count; i++)
        {
            if (smallestDist > Vector3.Distance(BofrostMachine.instance.allTheVeggies[i].transform.position, transform.position))
            {
                smallestDist = Vector3.Distance(BofrostMachine.instance.allTheVeggies[i].transform.position, transform.position);
                smallestIndex = i;
            }
        }

        if (smallestDist > Vector3.Distance(player.transform.position, transform.position))
        {
            smallestDist = Vector3.Distance(player.transform.position, transform.position);
            if (smallestDist <= seeRange)
            {
                target = player.transform;
            }
        }
        else
        {
            if (smallestDist <= seeRange)
            {
                target = BofrostMachine.instance.allTheVeggies[smallestIndex].transform;
            }
        }
    }

    public void DoMovement()
    {
        if(target != null)
        {
            agent.SetDestination(target.position);
        }else
        {
            if (counter <= 0)
            {
                FreeRoam();
                counter = Random.Range(2f, 4f);
            }
        }
    }

    void FreeRoam()
    {
        {
            Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, roamRadius, 1);
            Vector3 finalPosition = hit.position;
            agent.destination = finalPosition;
        }
    }
}
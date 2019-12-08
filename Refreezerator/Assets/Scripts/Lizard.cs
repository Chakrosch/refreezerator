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
    public List<Vegetable> vegetables;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = this.GetComponent<NavMeshAgent>();
        stun = this.GetComponent<Stun>();
        agent.updateRotation = false;
        
        Vegetable[] vegs = GameObject.FindObjectsOfType<Vegetable>();
        foreach (Vegetable item in vegs)
        {
            vegetables.Add(item);
        }

    }

    // Update is called once per frame
    void Update()
    {
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
                Instantiate(PrefabManager.instance.bigLizardPrefab, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerController.GameOver();
        }
    }

    void CheckNearby()
    {
        int smallestIndex = 0;
        float smallestDist = Mathf.Infinity;
        for (int i = 0; i < vegetables.Count; i++)
        {
            if (smallestDist > Vector3.Distance(vegetables[i].transform.position, transform.position))
            {
                smallestDist = Vector3.Distance(vegetables[i].transform.position, transform.position);
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
                target = vegetables[smallestIndex].transform;
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
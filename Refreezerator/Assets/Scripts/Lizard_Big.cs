using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard_Big : MonoBehaviour
{
    private float direction_change_timer = 5f;
    public Rigidbody rb;
    public float speed;
    public int chaseDistance;
    private bool playerNearby = false;
    public GameObject player;
    private int layerMask = 1 << 10;
    private Stun stun;


// Start is called before the first frame update
    void Start()
    {
        stun = this.GetComponent<Stun>();
        layerMask = ~layerMask; //bitshift for collision with everything except layer 10 (player)
    }

// Update is called once per frame
    void Update()
    {
        updatePlayerDistance();
        if (!stun.getStunned())
        {
            if (playerNearby)
            {
                ChaseMove();
            }
            else
            {
                IdleMove();
            }    
        }
    }

    void ChaseMove()
    {
        bool walkAround = false;
        Vector3 dir = player.transform.position - this.transform.position;
        // Debug.DrawRay( transform.position+new Vector3(0,0,0.5f),  dir,  Color.green,   1.0f,  false);
        if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), dir, 2, layerMask)
            || Physics.Raycast(transform.position - new Vector3(0, 0, 0.5f), dir, 2, layerMask)
            || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), dir, 2, layerMask)
            || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), dir, 2, layerMask))
        {
            Vector3 movement = new Vector3(
                player.transform.position.x - this.transform.position.x,
                player.transform.position.y - this.transform.position.y,
                player.transform.position.z - this.transform.position.z
            );
            movement = Vector3.Normalize(movement);
            if (walkAround)
            {
                movement = WalkAround();
            }

            rb.velocity = movement * speed;
        }
    }

    void IdleMove()
        {
            if (direction_change_timer >= 5f)
            {
                direction_change_timer = 0f;
                int dir = Random.Range(0, 4);

                if (dir == 0)
                {
                    rb.velocity = new Vector3(0, 0, 0);
                }
                else if (dir == 1)
                {
                    rb.velocity = new Vector3(1, 0, 1) * speed;
                }
                else if (dir == 2)
                {
                    rb.velocity = new Vector3(-1, 0, 1) * speed;
                }
                else if (dir == 3)
                {
                    rb.velocity = new Vector3(-1, 0, 1) * speed;
                }
                else
                {
                    rb.velocity = new Vector3(-1, 0, -1) * speed;
                }
            }

            direction_change_timer += Time.deltaTime;
        }

    private void updatePlayerDistance()
        {
            if (Vector3.Distance(player.transform.position, this.transform.position) < chaseDistance)
            {
                playerNearby = true;
            }
            else
            {
                playerNearby = false;
            }
        }

        Vector3 WalkAround()
        {
            float a = player.transform.position.x - this.transform.position.x;
            float b = player.transform.position.y - this.transform.position.y;
            //Bestimmt die beiden Richtungen in der sich d Player befindet. Raycastet nach der blockierenden wand und läuft in die andere Richtung.
            if (a > 0 && b > 0)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), new Vector3(1, 0, 0), 2, layerMask))
                {
                    Debug.Log("oben");
                    return new Vector3(0, 0, 1);
                }

                Debug.Log("rechts1");
                return new Vector3(1, 0, 0);
            }
            else if (a <= 0 && b > 0)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), new Vector3(-1, 0, 0), 2, layerMask))
                {
                    Debug.Log("links");
                    return new Vector3(-1, 0, 0);
                }

                Debug.Log("oben");
                return new Vector3(0, 0, 1);
            }
            else if (a > 0 && b <= 0)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), new Vector3(1, 0, 0), 2, layerMask))
                {
                    Debug.Log("unten");
                    return new Vector3(0, 0, -1);
                }

                Debug.Log("rechts2");
                return new Vector3(1, 0, 0);
            }
            else if (a <= 0 && b <= 0)
            {
                if (Physics.Raycast(transform.position + new Vector3(0, 0, 0.5f), new Vector3(-1, 0, 0), 2, layerMask))
                {
                    Debug.Log("unten");
                    return new Vector3(0, 0, -1);
                }

                Debug.Log("links");
                return new Vector3(-1, 0, 0);
            }

            return new Vector3(0, 10, 0); //sollte nie erreicht werden, wenn doch fliegt biggus echsus jetzt zum Mond
        }
    }
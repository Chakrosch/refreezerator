using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{

    private float direction_change_timer = 5f;
    public Rigidbody rb;
    public float speed;
    private bool veggieNearby = false;
    private GameObject isChased;
    private float countDown;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countDown < 0)
        {
            anim.SetTrigger("randomEarFlap");
            countDown = Random.Range(3f, 10f);
        }
        else
        {
            countDown -= Time.deltaTime;
        }
        if (veggieNearby)
            {
                ChaseMove();
            }
            else
            {
                IdleMove();
            }
        }
    

    void ChaseMove()
    {
        Vector3 movement = new Vector3(
            isChased.transform.position.x - this.transform.position.x,
            isChased.transform.position.y - this.transform.position.y,
            isChased.transform.position.z - this.transform.position.z
        );
        movement = Vector3.Normalize(movement);
        rb.velocity = movement * speed;
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
                anim.SetBool("isRunning", false);
            }
            else if (dir == 1)
            {
                rb.velocity = new Vector3(1, 0, 0) * speed;
                anim.SetBool("isRunning", true);
            }
            else if (dir == 2)
            {
                rb.velocity = new Vector3(0, 0, 1) * speed;
                anim.SetBool("isRunning", true);
            }
            else if (dir == 3)
            {
                rb.velocity = new Vector3(-1, 0, 0) * speed;
                anim.SetBool("isRunning", true);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, -1) * speed;
                anim.SetBool("isRunning", true);
            }
        }

        direction_change_timer += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vegetable")
        {
            veggieNearby = true;
            isChased = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "vegetable")
        {
            veggieNearby = false;
            isChased = null;
        }
    }
}
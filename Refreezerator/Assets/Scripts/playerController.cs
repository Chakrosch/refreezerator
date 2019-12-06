using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class playerController : MonoBehaviour
{
    private Vector3 movement; // Y-Axis should always be zero
    public float movementSpeed;
    public Rigidbody rb;
    private bool vegetableNearby;

    void Update()
    {
        getInput();
        move();
    }

    /// <summary>
    /// Input is transformed to movement
    /// </summary>
    private void getInput()
    {
        movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        movement.z = Input.GetAxis("Vertical") * movementSpeed;
        if (Input.GetButtonDown("Interact"))
        {
            pickUp();
        }
    }

    /// <summary>
    /// Moves the player in 3d Space
    /// </summary>
    private void move()
    {
        rb.velocity = movement;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "vegetable")
        {
            vegetableNearby = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "vegetable")
        {
            vegetableNearby = false;
        }
    }

    private void pickUp()
    {
        Debug.Log("Test");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 movement; // Y-Axis should always be zero
    public float movementSpeed;
    public Rigidbody rb;
    private PickUpObject objectInRange;
    public Fridge fridge;
    public float throwForce;


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
            if (fridge.currentObject != null)
            {
                throwOut();
            }
            else
            {
                pickUp();
            }
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
        if(collision.gameObject.tag == "vegetable" && collision.gameObject.GetComponent<Vegetable>() != null)
        {
            objectInRange = collision.gameObject.GetComponent<PickUpObject>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "vegetable" && collision.gameObject.GetComponent<Vegetable>() != null)
        {
            objectInRange = null;
        }
    }

    private void pickUp()
    {
        if (objectInRange != null)
        {
            fridge.currentObject = objectInRange;
            objectInRange = null;
            fridge.currentObject.setInvisible(true);
            fridge.currentObject.setRigidbody(false);
            fridge.currentObject.transform.parent = transform;
            fridge.currentObject.transform.position = Vector3.zero;
        }
    }

    private void throwOut()
    {
        fridge.currentObject.setInvisible(false);
        fridge.currentObject.setRigidbody(true);
        fridge.currentObject.rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        fridge.currentObject = null;
    }
}
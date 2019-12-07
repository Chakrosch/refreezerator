using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 movement; // Y-Axis should always be zero
    private Vector3 throwVec;
    public float movementSpeed;
    public Rigidbody rb;
    private PickUpObject objectInRange;
    public Fridge fridge;
    public float throwForce;
    public Vector3 lookingDirection;


    void Update()
    {
        getInput();
        move();
        if(fridge.currentObject != null) 
        {
            fridge.currentObject.transform.localPosition = Vector3.up;
        }
        getDirection();
    }

    /// <summary>
    /// Input is transformed to movement
    /// </summary>
    private void getInput()
    {
        movement.x = Input.GetAxis("Horizontal") * movementSpeed;
        movement.z = Input.GetAxis("Vertical") * movementSpeed;

        throwVec = lookingDirection * throwForce;

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

    private void getDirection()
    {
        if(Input.GetAxis("Vertical") > 0)
        {
            lookingDirection.z = 1;
            lookingDirection.x = 0;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            lookingDirection.z = -1;
            lookingDirection.x = 0;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            lookingDirection.x = 1;
            lookingDirection.z = 0;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            lookingDirection.x = -1;
            lookingDirection.z = 0;
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
        if(collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            objectInRange = collision.gameObject.GetComponent<PickUpObject>();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
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
            fridge.currentObject.transform.localPosition = Vector3.up;
            fridge.currentObject.inFridge = true;
        }
    }

    private void throwOut()
    {
        fridge.currentObject.setInvisible(false);
        fridge.currentObject.setRigidbody(true);
        fridge.currentObject.transform.parent = null;
        fridge.currentObject.inFridge = false;
        fridge.currentObject.rb.AddForce(throwVec, ForceMode.Impulse);
        fridge.currentObject = null;
    }
}
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
            fridge.currentObject = objectInRange.type.ToString();
            Destroy(objectInRange.gameObject);
            objectInRange = null;
        }
    }

    private void throwOut()
    {


        switch (fridge.currentObject.type)
        {
            case FridgeObject.fridgeObjectTypes.alcohol:
                throwAlcohol();
                break;
            case FridgeObject.fridgeObjectTypes.enemy:
                throwEnemy();
                break;
            case FridgeObject.fridgeObjectTypes.player:
                break;
            case FridgeObject.fridgeObjectTypes.vegetable:
                throwVegetable();
                break;
        }
    }

    private void throwAlcohol()
    {
        for (int i = 0; i < PrefabManager.instance.alcohol.Length; i++)
        {
            if(PrefabManager.instance.alcohol[i].GetComponent<Alcohol>() != null)
                if(PrefabManager.instance.alcohol[i].GetComponent<Alcohol>().name == fridge.currentObject.name)
                {
                    Instantiate(PrefabManager.instance.alcohol[i]);
                    break;
                }
        }
        fridge.currentObject = null;
    }

    private void throwVegetable()
    {

    }

    private void throwEnemy()
    {

    }
}
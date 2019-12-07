using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

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
    public EasterEgg easteregg;
    public KeyCode lastKey;
    public Image currentItemImage;
    public icicleSlider slider;

void Start(){

	slider =  GameObject.Find("icicle").GetComponent<icicleSlider>();
        currentItemImage = GameObject.Find("ItemSlot").GetComponent<Image>();
	
}
    void Update()
    {
        getInput();
        move();
        if(fridge.currentObject != null) 
        {
            fridge.currentObject.transform.localPosition = Vector3.up;
        }
        getDirection();
        checkEasterEgg();
        if(fridge.currentObject != null)
        {
            slider.value = 1 - fridge.currentObject.temperature;
        }
        else
        {
            slider.value = 0;
        }
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

    private void checkEasterEgg()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastKey = KeyCode.S;
        }
        else if (Input.GetKeyDown(KeyCode.G) && lastKey == KeyCode.S)
        {
            lastKey = KeyCode.G;
        }
        else if (Input.GetKeyDown(KeyCode.J) && lastKey == KeyCode.G)
        {
            lastKey = KeyCode.Alpha0;
            easteregg.show();
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
            fridge.currentObject.transform.localPosition = Vector3.down*100;
            fridge.currentObject.inFridge = true;
            Color c = currentItemImage.color;
            c.a = 1;
            currentItemImage.color = c;
            currentItemImage.sprite = fridge.currentObject.renderer.sprite;
        }
    }

    private void throwOut()
    {
        fridge.currentObject.setInvisible(false);
        if (fridge.currentObject.isFrozen)
        {
            fridge.currentObject.setFreeze(true);
            fridge.currentObject.throwObject();
        }
        fridge.currentObject.setRigidbody(true);
        fridge.currentObject.transform.parent = null;
        fridge.currentObject.inFridge = false;
        fridge.currentObject.rb.AddForce(throwVec, ForceMode.Impulse);

        fridge.currentObject = null;
        Color c = currentItemImage.color;
        c.a = 0;
        currentItemImage.color = c;
    }
	public static void GameOver()
	{
  //Highscore mechanic; execute on player death or return to safety		
		WriteString();
		Home.resetVeggieCount();
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}
    static void WriteString()
    {
        string path = "Assets/highscore.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(Home.getVeggie()+";");
        writer.Close();
    }

}
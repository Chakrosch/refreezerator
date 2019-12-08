using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLizardCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PickUpObject>() != null)
        {
            if (collision.gameObject.GetComponent<PickUpObject>().isFrozen)
            {
                Debug.Log("Test");
                // Stun
            }
        }
        else if (collision.gameObject.tag == "vegetable")
        {
            Debug.Log("Test2");
            Instantiate(PrefabManager.instance.babyLizardPrefab, transform.position, Quaternion.identity);
        
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerController.GameOver();
        }
    }
}

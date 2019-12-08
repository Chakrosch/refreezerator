using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quicksand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "enemy" )
        {
            Destroy(other.gameObject);
        }else if (other.gameObject.tag == "vegetable")
        {
            Destroy(other.gameObject);
        }else if (other.gameObject.tag == "Player")
        {
            PlayerController.GameOver();
        }
    }
}

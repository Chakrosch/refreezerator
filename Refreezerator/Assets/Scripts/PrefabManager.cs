using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static PrefabManager instance;

    public GameObject[] vegetables;
    public GameObject[] enemies;
    public GameObject[] player;
    public GameObject[] alcohol;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
}

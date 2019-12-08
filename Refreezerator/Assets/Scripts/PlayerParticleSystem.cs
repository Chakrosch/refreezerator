using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PlayerParticleSystem : MonoBehaviour
{
    [Header("Colors")]
    public Color onDust;
    public Color onWater;
    [Header("Data")]
    public float maxParticleEmittance = 10f;

    private ParticleSystem.EmissionModule eMod;
    private ParticleSystem.MainModule mMod;
    private PlayerController player;

    void Start()
    {
        ParticleSystem pSys = GetComponent<ParticleSystem>();
        eMod = pSys.emission;
        mMod = pSys.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        eMod.rateOverTime = maxParticleEmittance * player.movementSpeed * Mathf.Abs(player.movement.x + player.movement.y + player.movement.z)/3;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("floor"))
            mMod.startColor = onDust;
        else
            mMod.startColor = onWater;
    }
}

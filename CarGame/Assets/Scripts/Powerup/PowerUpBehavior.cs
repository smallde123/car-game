﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpBehavior : MonoBehaviour
{

    public PowerupController controller;

    [SerializeField]
    private Powerup powerup;

    private Transform transform_;
    public GameObject pickupEffect;

    // Start is called before the first frame update
    void Awake()
    {
        transform_ = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            ActivatePowerup();
            gameObject.SetActive(false);
            Instantiate(pickupEffect, transform.position, transform.rotation);
        }
    }

    private void ActivatePowerup()
    {
        controller.ActivatePowerup(powerup);
    }

    public void SetPowerup(Powerup powerup)
    {
        this.powerup = powerup;
        gameObject.name = powerup.name;
    }
}

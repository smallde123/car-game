using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPowerUps : MonoBehaviour
{
    public Transform obj;
    public GameObject controller;
    public PowerupController power;
    
    // Start is called before the first frame update
    void Start()
    {
        obj = this.transform;
        controller = GameObject.FindWithTag("powerupcontroller");
        power = controller.GetComponent<PowerupController>();

        if (Random.value < /*0.01*/ 0.1)//Chance of powerup spawning
        {
            power.SpawnPowerup(power.powerups[Random.Range(0, 3)], obj.position);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
//power.powerups.Count - 1
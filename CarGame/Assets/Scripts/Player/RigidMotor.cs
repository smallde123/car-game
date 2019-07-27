using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class RigidMotor : MonoBehaviour
    {

        public Rigidbody rb;
        [HideInInspector]
        public PlayerStats playerStats;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = Vector3.forward * playerStats.speed;
        }
    }

}

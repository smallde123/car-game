using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HealthManager : MonoBehaviour
    {

        public float health = 100;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            health = Mathf.Clamp(health, 0, 100);
        }

        public void SubHealth(int num)
        {
            health -= num;
        } 

        public void AddHealth(int num)
        {
            health += num;
        }
    }

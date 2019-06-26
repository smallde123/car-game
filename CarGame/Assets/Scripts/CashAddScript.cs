using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class CashAddScript : MonoBehaviour
    {

        public GameObject cashManager;
        public AudioSource audio;
        public GameObject playerScript;

        Transform target;

        // Start is called before the first frame update
        void Start()
        {
            cashManager = GameObject.FindWithTag("cashmanager");
            playerScript = GameObject.FindGameObjectWithTag("Player");
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "pizza")
            {
                cashManager.GetComponent<CashManager>().AddCash(10);
                print("delivered");
                Destroy(other.gameObject);
                audio.Play();
                playerScript.GetComponent<PlayerStats>().AddDelivered();
            }
        }
    }

}

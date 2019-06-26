using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class OnCollide : MonoBehaviour
    {

        public AudioSource audio;
        public GameObject playerScript;

        public void Start()
        {
            playerScript = GameObject.FindGameObjectWithTag("Player");
        }

        public void Update()
        {
            
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag != ("Player") && col.gameObject.tag != ("Sedan") && col.gameObject.tag != ("cashadd"))
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<MeshCollider>().enabled = false;
                GetComponent<Rotation>().enabled = false;
                GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionY;
                audio.Play();
                playerScript.GetComponent<PlayerStats>().AddRuined();
            }
        }
    }

}

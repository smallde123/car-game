using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{ 

    public class DeliverySpawner : MonoBehaviour
    {
        public int objCount;
        public float objSpeed;
        public bool canThrow;
        
        public Transform spawnPos;
        public Rigidbody objPrefab;
        public TouchEvents tEvents;

        void Start()
        {

        }

        void Update()
        {
            Mathf.Clamp(objCount, 0, 25);
        }

        public void ThrowObj()
        {
            if (tEvents.canPlay == true && objCount > 0 && canThrow == true)
            {
                Rigidbody objInstance;
                objInstance = Instantiate(objPrefab, spawnPos.position, spawnPos.rotation) as Rigidbody;
                objInstance.AddForce(spawnPos.forward * objSpeed);
                objCount--;
            }
        }
    }

}

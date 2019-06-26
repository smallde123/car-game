using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompleteProject
{

    public class FloatingText : MonoBehaviour
    {

        public GameObject PositiveTextPrefab;
        public GameObject NegativeTextPrefab;
        public PlayerStats stats;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == ("cashadd"))
            {
                if (PositiveTextPrefab != null)
                {
                    ShowFloatingText();
                }
            }
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == ("wood") && stats.canTakeDamage == true)
            {
                if (NegativeTextPrefab != null)
                {
                    //ShowWoodText();
                }
            }

            if (col.gameObject.tag == ("cement"))
            {
                if (NegativeTextPrefab != null)
                {
                    //ShowCementText();
                }
            }
        }

        void ShowFloatingText()
        {
            var go = Instantiate(PositiveTextPrefab, transform.position, Quaternion.identity, transform);
            go.GetComponent<TextMesh>().text = "$" + stats.vehicles[stats.currentVehicle].cashEarned.ToString();
        }
    }

}

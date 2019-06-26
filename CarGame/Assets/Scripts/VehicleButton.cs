using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace CompleteProject
{

    public class VehicleButton : MonoBehaviour
    {
        public PlayerStats playerStats;
        public CashManager cashManager;
        public int vehicleNumber;
        public static Button carButton;

        public Text name;
        public Text cost;
        public Text description;

        private AudioSource source;


        // Start is called before the first frame update
        void Start()
        {
            source = GetComponent<AudioSource>();
            SetButton();
        }

        void SetButton()
        {
            string costString = playerStats.vehicles[vehicleNumber].cost.ToString();
            name.text = playerStats.vehicles[vehicleNumber].name;
            cost.text = "$" + playerStats.vehicles[vehicleNumber].cost;
            description.text = playerStats.vehicles[vehicleNumber].description;
        }

        public void BuyCar()
        {
            if (cashManager.playerCash >= playerStats.vehicles[vehicleNumber].cost)
            {
                cashManager.playerCash -= playerStats.vehicles[vehicleNumber].cost;
                playerStats.currentVehicle = vehicleNumber;
                carButton.interactable = false;
            }
            else
            {
                source.Play();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}

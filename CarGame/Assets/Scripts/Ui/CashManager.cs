using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class CashManager : MonoBehaviour
    {

        public float playerCash;


        // Start is called before the first frame update
        void Awake()
        {
            playerCash = PlayerPrefs.GetFloat("Cash");
        }

        // Update is called once per frame
        void Update()
        {
            playerCash = Mathf.Clamp(playerCash, 0, Mathf.Infinity);

            if(PlayerPrefs.HasKey("Cash"))
            {
                if(Input.GetKeyDown(KeyCode.M))
                {
                    PlayerPrefs.DeleteKey("Cash");
                    playerCash = 0;
                    SaveCash();
                    Debug.Log("RESET CASH");
                }
            }
        }

        public void GetCash()
        {
            playerCash = PlayerPrefs.GetFloat("Cash");
        }

        public void SaveCash()
        {
            PlayerPrefs.SetFloat("Cash", playerCash);
        }

        public void AddCash(float num)
        {
            playerCash += num;
            SaveCash();
        }

        public void SubCash(float num)
        {
            playerCash -= num;
            SaveCash();
        }
    }

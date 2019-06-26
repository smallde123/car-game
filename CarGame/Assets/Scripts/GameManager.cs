using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    public int goodsDelivered = 0;
    [SerializeField] private int cash;

    //public Text cashText;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        UpdateUI();
    }

    public void AddCash(int amount)
    {
        //cash += amount;
        cash += amount;
        UpdateUI();
    }

    public void SubCash(int amount)
    {
        cash -= amount;
        UpdateUI();
    }

    public bool RequestMoney(int amount)
    {
        if(amount <= cash)
        {
            return true;
        }
        return false;
    }

    void UpdateUI()
    {
        //cashText.text = "$ " + cash.ToString("f0");
    }

}

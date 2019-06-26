using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public int pizzasDelivered = 0;
    public int pizzasRuined = 0;

    public void UpdateDelivered()
    {
        pizzasDelivered++;
        Debug.Log(pizzasDelivered);
    }
    public void UpdateRuined()
    {
        pizzasRuined++;
        Debug.Log(pizzasRuined);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class VehicleObject : ScriptableObject
{
    public string vehicleName = "Vehicle Name Here";
    public Mesh vehicleMesh;
    public Material carColor;
    public int cost = 5000;
    public int cashEarned;
    public float maxRepairTime = 300;
    public string description;
    public bool isBrokeDown;

    public float vSpeed = 30f;
    public int vHealth = 100;
}

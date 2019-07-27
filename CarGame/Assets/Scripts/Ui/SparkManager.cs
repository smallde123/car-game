using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkManager : MonoBehaviour
{

    public float sparks = 0;

    void Awake()
    {
        sparks = PlayerPrefs.GetInt("sparks");
    }

    
    void Update()
    {
        sparks = Mathf.Clamp(sparks, 0, Mathf.Infinity);
    }

    public void GetSparks()
    {
        sparks = PlayerPrefs.GetFloat("sparks");
    }
    public void SaveSparks()
    {
        PlayerPrefs.SetFloat("sparks", sparks);
    }


    public void AddSparks(float num)
    {
        sparks += num;
        SaveSparks();
    }

    public void SubSparks(float num)
    {
        sparks -= num;
        SaveSparks();
    }
}

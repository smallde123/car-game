﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleChanges : MonoBehaviour
{
    public int lowRange;
    public int highRange;
    public Material[] carMat;

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<MeshFilter>().mesh.name != ("TaxiCab Instance") || GetComponent<MeshFilter>().mesh.name != ("UtilVan Instance"))
        {
            int prefab_mat = Random.Range(lowRange, highRange);

            this.gameObject.GetComponent<MeshRenderer>().material = carMat[prefab_mat];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

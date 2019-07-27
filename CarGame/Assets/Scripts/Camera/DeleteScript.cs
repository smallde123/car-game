using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("wood"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("cement"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("cashadd"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("car"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("spark"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("sparkb"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("emptyObj"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("hole"))
        {
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == ("powerup"))
        {
            Destroy(col.gameObject);
        }
    }
}

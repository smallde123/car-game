using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public GameObject destroyedVersion;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == ("Player"))
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }

}

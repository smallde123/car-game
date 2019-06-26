using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleOrange;
    public Transform teleBlue;
    public GameObject theObj;
    public Rigidbody objrig;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Sedan"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) > 0.1f)
            {
                theObj.transform.position = teleOrange.transform.position;
                theObj.transform.rotation = teleOrange.transform.rotation;
                objrig.velocity = other.attachedRigidbody.velocity;
                objrig.velocity = teleOrange.transform.up * objrig.velocity.magnitude;
                objrig.AddForce(other.attachedRigidbody.velocity);
            }
        }
        if(other.gameObject.tag == ("Sedan"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) > 0.1f)
            {
                theObj.transform.position = teleBlue.transform.position;
                theObj.transform.rotation = teleBlue.transform.rotation;
                objrig.velocity = other.attachedRigidbody.velocity;
                objrig.velocity = teleBlue.transform.up * objrig.velocity.magnitude;
                objrig.AddForce(other.attachedRigidbody.velocity);
            }
        }
    }
}

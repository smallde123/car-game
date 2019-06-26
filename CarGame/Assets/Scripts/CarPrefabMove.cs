using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPrefabMove : MonoBehaviour
{
    float speed = 20;
    float timer;

    private bool canDespawn;

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        canDespawn = true;
        timer = 6.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = -transform.up * speed;
        GetComponent<Rigidbody>().velocity = v;

        timer = Mathf.Clamp(timer,0.0f, 10.0f);
        if (canDespawn == true)
        {
            timer -= 1.0f * Time.deltaTime;
        }
        if(timer <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == ("Player") || col.gameObject.tag == ("car") || col.gameObject.tag == ("suv") || col.gameObject.tag == ("truck"))
        {
            m_Rigidbody.constraints = RigidbodyConstraints.None;
            speed = 0;
            canDespawn = false;
        }
    }
}

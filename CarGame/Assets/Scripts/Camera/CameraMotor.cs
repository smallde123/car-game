using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public int camHeight;

    private Transform lookAt;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 3.0f;
    private Vector3 animationOffset = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start ()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform; //makes camera look at the player(car)
        startOffset = transform.position - lookAt.position;
	}
	
	// Update is called once per frame
	void Update ()
    {

        moveVector = lookAt.position + startOffset;
        //x
        moveVector.x = 0;
        //y
        moveVector.y = Mathf.Clamp(moveVector.y, camHeight, camHeight);

        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            //Animation at the start of the game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }
	}
}

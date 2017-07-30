using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraMove : MonoBehaviour {

    private Transform playerTransform;
    private Vector3 startOffset;
    private Vector3 moveVector;

    private float transition = 0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffSet = new Vector3(0, 5, 5);

    // Use this for initialization
    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - playerTransform.position;

    }
	
	// Update is called once per frame
	void Update () {
        moveVector = playerTransform.position + startOffset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);
        if (transition > 1.0f)
        {
            //after start animation is over
            transform.position = moveVector;
        }
        else
        {
            transform.position = Vector3.Lerp(moveVector + animationOffSet, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(playerTransform.position + Vector3.up);
        }

        
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeatTerrain : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter()
    {
        this.transform.Translate ( new Vector3(0,0,150));
        //Debug.Log("Working");
    }
}

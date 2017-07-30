using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour {

    public GameObject[] floors;

    private Transform playerTransform;
    private float zSpawn = -8f;
    private float bridgeLength = 3f;
    private int numberBridgeOnScreen = 200;
    private int currentBridge = 0;

    private float zFloorStartDeleting = 10f;

    private List<GameObject> activeBridges;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform ;
        activeBridges = new List<GameObject>();
        //first load of bridge floors
        for (int i = 0; i < numberBridgeOnScreen; i++)
        {
            SpawnBridge();
        }


    }

    // Update is called once per frame
    void Update () {
        if (playerTransform.position.z - zFloorStartDeleting > (zSpawn - numberBridgeOnScreen * bridgeLength))
        {
            SpawnBridge();
            DeleteBridge();
        }
            
	}

    private void SpawnBridge(int bridgeIndex = -1)
    {
        GameObject oc;
        int nextBridgeIndex = Random.Range(0, floors.Length);
        while (nextBridgeIndex == currentBridge)
        {
           nextBridgeIndex = Random.Range(0, floors.Length);
        }
        oc = Instantiate(floors[nextBridgeIndex]) as GameObject;
        oc.transform.SetParent(transform);
        oc.transform.position = Vector3.forward * zSpawn;
        zSpawn += bridgeLength;
        activeBridges.Add(oc);
        currentBridge = nextBridgeIndex;
    }

    private void DeleteBridge()
    {
        Destroy(activeBridges[0]);
        activeBridges.RemoveAt(0);
    }
}

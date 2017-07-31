using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapGeneration : MonoBehaviour {

    public AudioClip letsGoSound;
    private AudioSource source;
    

    public GameObject[] floors;
    public GameObject battery;

    private Transform playerTransform;
    private float zSpawn = -50f;
    private float bridgeLength = 100f;
    private int numberBridgeOnScreen = 10;
    public float probabilitySpawnBattery = 0.08f;
    private int currentBridge = 0;
    private Vector3[] batteryPositions;

    private float zFloorStartDeleting = 75f;

    private List<GameObject> activeBridges;
    private List<GameObject> activeBatterys;

	// Use this for initialization
	void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform ;
        activeBatterys = new List<GameObject>();
        activeBridges = new List<GameObject>();
        InicialitateBatteryPositions();
        source = GetComponent<AudioSource>();
        source.PlayOneShot(letsGoSound, 0.4f);

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

    private void InicialitateBatteryPositions()
    {
        batteryPositions = new Vector3[13];
        batteryPositions[0] = new Vector3(-4, 1.5f, 20);
        batteryPositions[1] = new Vector3(-4, 1.5f, 20);
        batteryPositions[2] = new Vector3(1, 1.5f, 0);
        batteryPositions[3] = new Vector3(3, 1.5f, 0);
        batteryPositions[4] = new Vector3(-3, 1.5f, 15);
        batteryPositions[5] = new Vector3(-3, 1.5f, 15);
        batteryPositions[6] = new Vector3(1, 1.5f, -35);
        batteryPositions[7] = new Vector3(3, 1.5f, -20);
        batteryPositions[8] = new Vector3(-3, 1.5f, 12);
        batteryPositions[9] = new Vector3(-3, 1.5f, 0);
        batteryPositions[10] = new Vector3(4, 1.5f, -40);
        batteryPositions[11] = new Vector3(4, 1.5f, 15);
        batteryPositions[12] = new Vector3(1, 1.5f, 10);
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
        oc.transform.Rotate(0, 90, 0);
        zSpawn += bridgeLength;
        activeBridges.Add(oc);
        currentBridge = nextBridgeIndex;

        //spawn battery 
        SpawnBattery(oc);
    }

    private void SpawnBattery(GameObject oc)
    {

        //float random = Random.Range(1, 100);
        //if (random <= probabilitySpawnBattery * 100)
        //{
            InstantiateBattery(oc);
            InstantiateBattery(oc);
        //}
    }

    private GameObject InstantiateBattery(GameObject oc)
    {
        GameObject ba;
        ba = Instantiate(battery) as GameObject;
        activeBatterys.Add(ba);
        ba.transform.SetParent(oc.transform);
        ba.transform.position = oc.transform.position;
        ba.transform.position += batteryPositions[(int)Mathf.Abs(Random.Range(0f, batteryPositions.Length))];
        return ba;
    }

    private void DeleteBridge()
    {
        if (activeBridges[0].transform.Find("Battery"))
        {
            Destroy(activeBatterys[0]);
            activeBatterys.RemoveAt(0);
        }
        Destroy(activeBridges[0]);
        activeBridges.RemoveAt(0);
    }
}

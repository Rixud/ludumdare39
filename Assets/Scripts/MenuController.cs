using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private Button but;

    // Use this for initialization
    void Start () {
        but = transform.Find("StartButton").GetComponent<Button>();
        but.onClick.AddListener(IniciateGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IniciateGame()
    {
        SceneManager.LoadScene("testCdg");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    private Button but;
    private Button but2;

    // Use this for initialization
    void Start () {
        but = transform.Find("StartButton").GetComponent<Button>();
        but.onClick.AddListener(IniciateGame);
        but2 = transform.Find("CreditsButton").GetComponent<Button>();
        but2.onClick.AddListener(IniciateCredits);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void IniciateGame()
    {
        SceneManager.LoadScene("testCdg");
    }

    void IniciateCredits()
    {
        SceneManager.LoadScene("credits");
    }
}

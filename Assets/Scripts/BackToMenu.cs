using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour {

    private Button but;

    // Use this for initialization
    void Start()
    {
        but = transform.Find("CreditsButton").GetComponent<Button>();
        but.onClick.AddListener(BackMenu);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BackMenu()
    {
        SceneManager.LoadScene("menu");
    }
}

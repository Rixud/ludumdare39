using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour {

    public GameObject player;
    private PlayerMotor playerM;
    private float altitudeDeath = 0.80f;
    Animator anim;
    private Button but;
	// Use this for initialization
	void Start () {
        playerM = player.GetComponent<PlayerMotor>();
        anim = GetComponent<Animator>();
        but = transform.Find("RestartButton").GetComponent<Button>();
        but.onClick.AddListener(ReloadMap);
    }


    // Update is called once per frame
    void Update () {
        if (playerM.energyLevel <= 1 || playerM.transform.position.y <= altitudeDeath)
        {
            anim.SetTrigger("GameOver");
        }
	}

    void ReloadMap ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

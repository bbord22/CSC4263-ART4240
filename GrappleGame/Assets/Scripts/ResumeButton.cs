using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour {
    private PlayerController playerController;

	void Start () {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

    public void onClick()
    {
        playerController.pause();
    }
}

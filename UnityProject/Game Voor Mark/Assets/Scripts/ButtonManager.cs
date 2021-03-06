﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public void BackToStart()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        if (GameManager.INSTANCE != null)
            Destroy(GameManager.INSTANCE.gameObject);

        SceneManager.LoadScene(1);


    }
}

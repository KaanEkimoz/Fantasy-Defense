using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hostile"))
        {
            gameOverPanel.SetActive(false);
        }
    }


    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}


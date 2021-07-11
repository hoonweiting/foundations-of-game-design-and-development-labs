using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Transform ui;
    private Transform restartUi;

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Ui").transform;
        restartUi = GameObject.FindGameObjectWithTag("RestartUi").transform;

        foreach (Transform restartUiChild in restartUi)
        {
            Debug.Log("restartUiChild found. Name: " + restartUiChild.name);
            restartUiChild.gameObject.SetActive(false);
        }
    }

    public void StartButtonClicked()
    {
        foreach (Transform uiChild in ui)
        {
            if (uiChild.name != "Score")
            {
                Debug.Log("uiChild found. Name: " + uiChild.name);
                uiChild.gameObject.SetActive(false);
            }
        }

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(int score)
    {
        Time.timeScale = 0.0f;

        foreach (Transform uiChild in ui)
        {
            Debug.Log("uiChild found. Name: " + uiChild.name);
            uiChild.gameObject.SetActive(false);
        }

        foreach (Transform restartUiChild in restartUi)
        {
            Debug.Log("restartUiChild found. Name: " + restartUiChild.name);
            restartUiChild.gameObject.SetActive(true);

            if (restartUiChild.name == "Score")
            {
                restartUiChild.gameObject.GetComponent<Text>().text = "Score: " + score.ToString();
            }
        }
    }

    public void RestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

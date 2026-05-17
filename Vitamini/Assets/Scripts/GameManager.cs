using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    public Button reiniciarButton;
    public Button menuButton;

    private bool gameOverActivo = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (gameOverPanel  != null)
            gameOverPanel.SetActive(false);

        if (reiniciarButton != null)
            reiniciarButton.onClick.AddListener(ReiniciarEscena);
        if (menuButton != null)
            menuButton.onClick.AddListener(IrAlMenu);
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOverActivo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReiniciarEscena();
            }
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                IrAlMenu();
            }
        }

    }

    public void ActivarGameOver(GameObject panel)
    {
        gameOverPanel = panel;
        GameOver();
    }

    public void GameOver()
    {
        if (gameOverActivo) return;
         gameOverActivo = true;
         if (gameOverPanel != null)
         {
            gameOverPanel.SetActive(true);
         }

         if (gameOverText != null)
        {
            gameOverText.text = "Game Over";
        }
         Debug.Log("Game Over");
    }

    public void ReiniciarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
     public void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayMenu");
    }

}

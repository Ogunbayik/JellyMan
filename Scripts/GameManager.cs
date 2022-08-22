using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text modeText;
    public SpawnManager spawnManager;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Canvas healthCanvas;
    [SerializeField] private Canvas scoreCanvas;

    private GameObject player;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public enum GameStates
    {
        Start,
        InGame,
        GameOver,
        PassLevel
    }
    public GameStates currentState;
    public enum GamePanels
    {
        StartPanel,
        InGamePanel,
        GameOverPanel,
    }
    public void PanelController(GamePanels currentPanel)
    {
        startPanel.SetActive(false);
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(false);

        switch (currentPanel)
        {
            case GamePanels.StartPanel:
                startPanel.SetActive(true);
                break;
            case GamePanels.InGamePanel:
                inGamePanel.SetActive(true);
                break;
            case GamePanels.GameOverPanel:
                gameOverPanel.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        currentState = GameStates.Start;
    }
    private void Update()
    {
        switch(currentState)
        {
            case GameStates.Start: GameStart();
                break;
            case GameStates.InGame: GameInGame();
                break;
            case GameStates.GameOver: GameOver();
                break;
        }
    } public void GameStart()
    {
        PanelController(GamePanels.StartPanel);
        healthCanvas.gameObject.SetActive(false);
        scoreCanvas.gameObject.SetActive(false);
    }

    public void GameInGame()
    {
        PanelController(GamePanels.InGamePanel);
        healthCanvas.gameObject.SetActive(true);
        scoreCanvas.gameObject.SetActive(true);
        spawnManager.gameObject.SetActive(true);
    }
    public void GameOver()
    {
        PanelController(GamePanels.GameOverPanel);
        spawnManager.StopAllSpawning();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }













}

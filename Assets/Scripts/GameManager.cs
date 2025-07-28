using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int _playerScore { get; set; }
    public int _enemyScore { get; set; }

    [SerializeField] private GameObject endGamePanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
    }

    private void Start()
    {
        _playerScore = 0;
        _enemyScore = 0;
    }

    public void ShowEndGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
    }
}

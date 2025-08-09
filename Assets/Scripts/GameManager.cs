using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Purchasing;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private BallBehavior ballBehavior;
    public int _playerScore { get; set; }
    public int _botScore { get; set; }
    public int _lastPoint { get; set; }
    public bool _isLost { get; set; }

    [SerializeField] private GameObject loseGameText;
    [SerializeField] private GameObject winGameText;
    [SerializeField] private GameObject endGamePanel;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI botScoreText;

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
        _botScore = 0;
        endGamePanel.SetActive(false);
        loseGameText.SetActive(false);
        winGameText.SetActive(false);
    }

    public void UpdateUI()
    {
        if (playerScoreText != null && botScoreText != null)
        {
            playerScoreText.text = "" + _playerScore;
            botScoreText.text = "" + _botScore;
        }
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        loseGameText.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        endGamePanel.SetActive(true);
        winGameText.SetActive(true);
    }

    public void RestartButton()
    {
        Debug.Log("Restart");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopGame()
    {
        StartCoroutine(DelayBeforNextPlay());
    }

    private IEnumerator DelayBeforNextPlay()
    {
        Debug.Log("Game stop");
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            ball.transform.position = Vector2.zero;
        }

        UpdateUI();
        Debug.Log("before game crash");
        _isLost = true;
        yield return new WaitForSeconds(2f);
        _isLost = false;
        Debug.Log("game crash");

        if (ball != null)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            Vector2 newDir;
            if (_lastPoint == 1)
                newDir = new Vector2(0f, 1f);
            else if (_lastPoint == -1)
                newDir = new Vector2(0f, -1f);
            else
            {
                newDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            newDir.x = Random.Range(-0.5f, 0.5f);

            rb.velocity = newDir.normalized * ballBehavior._ballSpeed;
        }
    }
}

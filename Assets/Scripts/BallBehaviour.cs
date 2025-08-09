using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float _ballSpeed { get; set; }
    private int _endGameScore = 10;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _ballSpeed = 10f;
        Vector2 initDir = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;

        _rb.velocity = initDir * _ballSpeed;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance._isLost)
            return;
        NormalizeSpeed();

        // if (Mathf.Abs(_rb.velocity.x) < 0.1f)
        // {
        //     float newX = (_rb.velocity.x >= 0 ? 1f : -1f) * 0.5f;
        //     Vector2 fixDir = new Vector2(newX, _rb.velocity.y).normalized;
        //     _rb.velocity = fixDir * _ballSpeed;
        // }
    }

    private void NormalizeSpeed()
    {
        Vector2 curVerlocity = _rb.velocity;
        curVerlocity.x = NormalizeVeclocity(curVerlocity.x, 0.2f, 0.3f);
        curVerlocity.y = NormalizeVeclocity(curVerlocity.y, 0.2f, 0.3f);
        _rb.velocity = curVerlocity.normalized * _ballSpeed;
    }

    private float NormalizeVeclocity(float cur, float comparator, float value) {
        if (Math.Abs(cur) < comparator)
        {
            float adjusted = (UnityEngine.Random.value < 0.5f) ? -value : value;
            return adjusted;
        }
        return cur;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBorder"))
        {
            GameManager.Instance._botScore++;
            GameManager.Instance._lastPoint = -1;
            GameManager.Instance.StopGame();
            Debug.Log("Enemy Score: " + GameManager.Instance._botScore);
        }
        else if (collision.gameObject.CompareTag("BotBorder"))
        {
            GameManager.Instance._playerScore++;
            GameManager.Instance._lastPoint = 1;
            GameManager.Instance.StopGame();
            Debug.Log("Player Score: " + GameManager.Instance._playerScore);
        }

        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bot"))
        {
            Rigidbody2D paddleRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (paddleRb != null)
            {
                float paddleVelocityX = paddleRb.velocity.x;

                Vector2 ballDir = _rb.velocity.normalized;
                ballDir.x += paddleVelocityX * 1f;

                _rb.velocity = ballDir.normalized * _ballSpeed;
            }
        }
        if (GameManager.Instance._playerScore == _endGameScore)
        {
            GameManager.Instance.WinGame();
        }
        else if (GameManager.Instance._botScore == _endGameScore)
        {
            GameManager.Instance.LoseGame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _ballSpeed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _ballSpeed = 3f;
        Vector2 initDir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        _rb.velocity = initDir * _ballSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBorder"))
        {
            GameManager.Instance._enemyScore++;
            Debug.Log("Enemy Score: " + GameManager.Instance._enemyScore);
        }
        else if (collision.gameObject.CompareTag("BotBorder"))
        {
            GameManager.Instance._playerScore++;
            Debug.Log("Player Score: " + GameManager.Instance._playerScore  );
        }
    }
}

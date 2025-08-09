using UnityEngine;

public class BotController : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    private float _moveSpeed;
    private Rigidbody2D _rb;
    private float _offsetX;
    private float minX = -2.2f; // Left limit
    private float maxX = 2.2f;  // Right limit

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }
    private void Start()
    {
        _moveSpeed = 5f;
    }

    private void FixedUpdate()
    {
        if (ballTransform == null) return;

        float targetX = ballTransform.position.x;
        float step = _moveSpeed * Time.fixedDeltaTime;

        Vector2 newPos = new Vector2(Mathf.MoveTowards(_rb.position.x, targetX, step), _rb.position.y);
        if (newPos.x > maxX)
        {
            newPos.x = maxX;
        }
        if (newPos.x < minX)
        {
            newPos.x = minX;
        }
        _rb.MovePosition(newPos);
    }
}

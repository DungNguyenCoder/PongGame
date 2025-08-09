using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private bool _dragging = false;
    private float _offsetX;
    private float minX = -2.2f; // Left limit
    private float maxX = 2.2f;  // Right limit

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _offsetX = mouseWorld.x - _rb.position.x;
            _dragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _dragging = false;
        }
    }

    private void FixedUpdate()
    {
        if (_dragging)
        {
            Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float targetX = mouseWorld.x - _offsetX;

            targetX = Mathf.Clamp(targetX, minX, maxX);

            Vector2 newPos = new Vector2(targetX, _rb.position.y);
            _rb.MovePosition(newPos);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // giữ chuột trái
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // giữ y không đổi, chỉ thay đổi x theo chuột (tỉ lệ 1:1)
            Vector2 newPos = new Vector2(mouseWorldPos.x, _rb.position.y);
            _rb.MovePosition(newPos);
        }
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Vector2 _input;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
        _rb.freezeRotation = true;
    }

    void Update()
    {
        _input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = _input * moveSpeed;
    }
}
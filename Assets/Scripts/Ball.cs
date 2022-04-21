
using System.Collections;
using UnityEngine;


public sealed class Ball : MonoBehaviour
{
    public int BonusPoints { get; private set; }
    public Color BallColor { get; private set; }
    private Rigidbody2D _rb;
    private float _velocity;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(float xPosition, float scale, float velocity, int bonusPoints, Color color)
    {        
        transform.position = new Vector3(xPosition, transform.parent.position.y, 0);
        transform.localScale = new Vector3(scale, scale, transform.localScale.z);
        _velocity = velocity;
        BonusPoints = bonusPoints;
        BallColor = color;
        _spriteRenderer.color = color;
    }

    private void FixedUpdate() => Move();

    private void Move() => _rb.MovePosition(_rb.position + Vector2.down * _velocity * Time.deltaTime);

    public void Deactivate() =>   gameObject.SetActive(false);
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerV2 : MonoBehaviour {

    public int damage = 1;
    public float speed = 2f;
    public Vector2 direction;

    public float livingTime = 3f;

    public Color initialColor = Color.white;
    public Color finalColor;

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private float _startingTime;

    private bool _returning;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        // Save initial time
        _startingTime = Time.time;
        // Destroy the bullet after some time
        Destroy(gameObject, livingTime);
    }

    private void Update() {
        // Change bullet's colot over time
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }

    private void FixedUpdate() {
        // Move Object
        Vector2 movement = direction.normalized * speed;
        _rigidbody.velocity = movement;
        // transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        // transform.Translate(movement);

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!_returning && collision.CompareTag("Player")) {
            // Tell player to get hurt
            // collision.gameObject.GetComponent<PlayerHealthController>().AddDamage(1);
            collision.SendMessageUpwards("AddDamage", damage);

            Destroy(gameObject);
            // Debug.Log("Encontre al Player");
        }

        if (_returning && collision.CompareTag("Enemy")) {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(gameObject);
        }
    }

    public void AddDamage() {
        _returning = true;
        direction *= -1f;
    }
}

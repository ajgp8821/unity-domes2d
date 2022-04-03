using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed = 2f;
    public Vector2 direction;

    public float livingTime = 3f;

    public Color initialColor = Color.white;
    public Color finalColor;

    private SpriteRenderer _renderer;
    private float _startingTime;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        // Save initial time
        _startingTime = Time.time;
        // Destroy the bullet after some time
        Destroy(gameObject, livingTime);
    }

    private void Update() {
        // Move Object
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        // transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);
        transform.Translate(movement);

        // Change bullet's colot over time
        Debug.Log("_startingTime " + _startingTime);
        float _timeSinceStarted = Time.time - _startingTime;
        Debug.Log("_timeSinceStarted " + _timeSinceStarted);
        float _percentageCompleted = _timeSinceStarted / livingTime;
        Debug.Log("_percentageCompleted " + _percentageCompleted);
        Debug.Log("livingTime " + livingTime);

        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }
}

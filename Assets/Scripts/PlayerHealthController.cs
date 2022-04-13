using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour {

    public int totalHealth = 3;
    public RectTransform heartUI;

    // Game Over
    public RectTransform gameOverMenu;
    public GameObject hordes;

    private int _health;
    private float _heartSize = 16f;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private PlayerController _controller;

    private void Awake() {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<PlayerController>();
    }

    private void Start() {
        _health = totalHealth;
    }

    public void AddDamage(int amount) {
        _health -= amount;

        // Visual Feedback
        StartCoroutine("VisualFeedback");

        // Game Over
        if (_health <= 0) {
            _health = 0;
            gameObject.SetActive(false);
        }

        heartUI.sizeDelta = new Vector2(_heartSize * _health, _heartSize);

        Debug.Log("Payer got damaged. His current health is " + _health);
    }

    public void AddHealth(int amount) {
        _health += amount;

        // Max health
        if (_health > totalHealth) {
            _health = totalHealth;
        }

        heartUI.sizeDelta = new Vector2(_heartSize * _health, _heartSize);

        Debug.Log("Payer got dome life. His current health is " + _health);
    }

    private IEnumerator VisualFeedback() {
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        _renderer.color = Color.white;
    }

    private void OnEnable() {
        _health = totalHealth;
    }

    private void OnDisable() {
        gameOverMenu.gameObject.SetActive(true);

        hordes.SetActive(false);
        _animator.enabled = false;
        _controller.enabled = false;
    }
}

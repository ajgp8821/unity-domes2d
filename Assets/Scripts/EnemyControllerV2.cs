using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerV2 : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float wallAware = 0.5f;
    public LayerMask groundLayer;
    public float playerAware = 3f;
    public float aimingTime = 0.5f;
    public float shootingTime = 1.5f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private WeaponController _weapon;
    private AudioSource _audio;

    // Movement
    private Vector2 _movement;
    private bool _facingRight;

    private bool _isAttacking;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<WeaponController>();
        _audio = GetComponent<AudioSource>();
        /*for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(true);
        }*/
    }

    private void Start() {
        /*UpdateTraget();
        StartCoroutine("PatrolTarget");*/
        if (transform.localScale.x < 0f) {
            _facingRight = false;
        }
        else {
            _facingRight = true;
        }
    }


    private void Update() {
        Vector2 direction = Vector2.right;

        if (!_facingRight) {
            direction = Vector2.left;
        }

        if (!_isAttacking) {
            if (Physics2D.Raycast(transform.position, direction, wallAware, groundLayer)) {
                Flip();
            }
        }
    }

    private void FixedUpdate() {
        float horizontalVlocity = speed;

        if (!_facingRight) {
            horizontalVlocity = horizontalVlocity * -1f;
        }
        if (_isAttacking) {
            horizontalVlocity = 0f;
        }

        _rigidbody.velocity = new Vector2(horizontalVlocity, _rigidbody.velocity.y);
    }

    private void LateUpdate() {
        _animator.SetBool("Idle", _rigidbody.velocity == Vector2.zero);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!_isAttacking && collision.CompareTag("Player")) {
            StartCoroutine("AimAndShoot");
            // StartCoroutine(AimAndShoot());
        }
    }

    private void Flip() {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX = localScaleX * -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator AimAndShoot() {
        /*float speedBackup = speed;
        speed = 0;*/

        _isAttacking = true;

        yield return new WaitForSeconds(aimingTime);

        _animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(shootingTime);

        _isAttacking = false;
        // speed = speedBackup;
    }

    void CanShoot() {
        if (_weapon != null) {
            _weapon.Shoot();
            _audio.Play();
        }
    }

    private void OnEnable() {
        _isAttacking = false;

        Transform shootingArea = transform.Find("ShootingArea");

        if (shootingArea != null) {
            shootingArea.gameObject.SetActive(true);
        }
    }

    private void OnDisable() {
        StopCoroutine("AimAndShoot");
        _isAttacking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float speed = 2.5f;
    public float jumpForce = 2.5f;
    public float longIdleTime = 5f;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadio;


    // References
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Long Idle
    private float _longIdleTimer;

    // Movement
    private Vector2 _movement;
    private bool _facingRight = true;
    private bool _isgrounded;

    // Attack
    private bool _isAttaking;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {

        if (!_isAttaking) {
            // Movement
            // float horizontalInput = Input.GetAxis("Horizontal");
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            _movement = new Vector2(horizontalInput, 0f);

            // Flip character
            if (horizontalInput < 0 && _facingRight) {
                Flip();
            }
            else if (horizontalInput > 0 && !_facingRight) {
                Flip();
            }
        }

        // Is Grounded?
        _isgrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadio, groundLayer);

        // Is Jumping?
        if (Input.GetButtonDown("Jump") && _isgrounded && !_isAttaking) {
            // _rigidbody.AddForce(Vector2.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse);
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Wanna Attack?
        if (Input.GetButtonDown("Fire1") && _isgrounded && !_isAttaking) {
            _movement = Vector2.zero;
            _rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger("Attack");
        }
    }

    private void FixedUpdate() {
        if (!_isAttaking) {
            float horizontalVelocity = _movement.normalized.x * speed;
            // _rigidbody.velocity = new Vector2(horizontalVelocity * Time.deltaTime, _rigidbody.velocity.y);
            _rigidbody.velocity = new Vector2(horizontalVelocity, _rigidbody.velocity.y);
        }
    }

    private void LateUpdate() {
        _animator.SetBool("Idle", _movement == Vector2.zero);
        _animator.SetBool("IsGrounded", _isgrounded);
        _animator.SetFloat("VerticalVelocity", _rigidbody.velocity.y);

        // Animator
        _isAttaking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
        /*if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack")) {
            _isAttaking = true;
        }
        else {
            _isAttaking = false;
        }*/

        // Long Idle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle")) {
            _longIdleTimer += Time.deltaTime;

            if (_longIdleTimer >= longIdleTime) {
                _animator.SetTrigger("LongIdle");
            }
        }
        else {
            _longIdleTimer = 0f;
        }
    }

    private void Flip() {
        _facingRight = !_facingRight;
        float localScaleX = transform.localScale.x;
        localScaleX *= -1f;
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

}

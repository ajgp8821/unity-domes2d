using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRaycast2D : MonoBehaviour {

    private Animator _animator;
    private WeaponController _weapon;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<WeaponController>();
    }

    private void Start() {
        _animator.SetBool("Idle", true);
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1")) {
            _animator.SetTrigger("Shoot");
        }
    }

    void CanShoot() {
        if (_weapon != null) {
            // Shoot
            StartCoroutine(_weapon.ShootWithRayCast());
        }
    }
}

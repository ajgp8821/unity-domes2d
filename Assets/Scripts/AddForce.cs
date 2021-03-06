using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour {

    public Vector2 force;
    public ForceMode2D forceMode;
    public bool addRandom;

    private Rigidbody2D _rigidbody;

    private void Awake() {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        Vector2 appliedForce = force;

        if (addRandom) {
            appliedForce += Random.insideUnitCircle.normalized;
        }

        _rigidbody.AddForce(appliedForce, forceMode);
    }
}

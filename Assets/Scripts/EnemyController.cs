using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;

    private GameObject _target;
    private Animator _animator;
    private WeaponController _weapon;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<WeaponController>();
    }

    private void Start() {
        UpdateTraget();
        StartCoroutine("PatrolTarget");
    }

    private void UpdateTraget() {

        // If first time, create taget in the left
        if (_target == null) {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
            return;
        }

        // If we are in the left, change target to the right
        if (_target.transform.position.x == minX) {
            _target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);
        }

        // If we are in the right, change target to the left
        else if (_target.transform.position.x == maxX) {
            _target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    IEnumerator PatrolTarget() {
        // Coroutine to move the enemy
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.005f) {
            // Update animator
            _animator.SetBool("Idle", false);

            Vector2 direction = _target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            // IMPORTANT
            yield return null;
        }

        // At this point, I've reached the target, let's set our position to the target's one
        Debug.Log("Target reached");
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);
        UpdateTraget();

        // Update animator
        _animator.SetBool("Idle", true);

        // Shoot
        _animator.SetTrigger("Shoot");

        // And let´s wait for a moment
        Debug.Log("Waiting for " + waitingTime + " seconds");
        // IMPORTANT
        yield return new WaitForSeconds(waitingTime);

        // Once waited, let's restore the patrol behaiviour
        Debug.Log("Waited enough, let's update the target and move again");
        
        StartCoroutine("PatrolTarget");
    }

    void CanShoot() {
        if (_weapon != null) {            
            _weapon.Shoot();
        }
    }
}

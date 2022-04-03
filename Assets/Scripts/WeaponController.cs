using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject shooter;

    private Transform _firePoint;

    private void Awake() {
        // Get child with name
        _firePoint = transform.Find("FirePoint");
    }

    private void Start() {
        // Instantiate(bulletPrefab, _firePoint.transform.position, Quaternion.identity);
        Invoke("Shoot", 1f);
        Invoke("Shoot", 2f);
        Invoke("Shoot", 3f);
    }

    private void Update() {
        
    }

    void Shoot() {
        if (bulletPrefab != null && _firePoint != null && shooter != null) {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.transform.position, Quaternion.identity) as GameObject;

            BulletController bulletController = myBullet.GetComponent<BulletController>();

            if (shooter.transform.localScale.x < 0f) {
                // Left
                bulletController.direction = Vector2.left; // new Vector2(-1f, 0f);
            }
            else {
                // Right
                bulletController.direction = Vector2.right; // new Vector2(1f, 0f);
            }
        }
    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    public GameObject bulletPrefab;
    public GameObject shooter;

    public GameObject explosionEffect;
    public LineRenderer lineRenderer;

    private Transform _firePoint;

    private void Awake() {
        // Get child with name
        _firePoint = transform.Find("FirePoint");
    }

    private void Start() {
        // Instantiate(bulletPrefab, _firePoint.transform.position, Quaternion.identity);
        /*Invoke("Shoot", 1f);
        Invoke("Shoot", 2f);
        Invoke("Shoot", 3f);*/
    }

    private void Update() {
        
    }

    public void Shoot() {
        if (bulletPrefab != null && _firePoint != null && shooter != null) {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.transform.position, Quaternion.identity) as GameObject;

            BulletControllerV2 bulletController = myBullet.GetComponent<BulletControllerV2>();

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

    public IEnumerator ShootWithRayCast() {
        if (explosionEffect != null && lineRenderer != null) {
            Debug.Log("Raycast IF 1");
            RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);

            if (hitInfo) {
                Debug.Log("Raycast IF 2 hitInfo " + hitInfo);
                // Example code
                /*if (hitInfo.collider.tag == "Player") {
                    Transform player = hitInfo.transform;
                    player.GetComponent<PlayerHealth>().ApplyDamage(5);
                }*/

                // Instantiate explosion on hit point
                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);

                // Set line renderer
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else {
                Debug.Log("Raycast ELSE " + hitInfo);
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }

            lineRenderer.enabled = true;
            yield return null;

            lineRenderer.enabled = false;

        }
    }
}

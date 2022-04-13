using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItemController : MonoBehaviour {

    public int healthRestoration = 1;
    public GameObject lightingParticles;
    public GameObject burstParticles;

    private SpriteRenderer _renderer;
    private Collider2D _collider;

    private void Awake() {
        // _renderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Cure Player
            collision.SendMessageUpwards("AddHealth", healthRestoration);

            // Disable Collider
            _collider.enabled = false;

            // Visual stuff
            /*_renderer.enabled = false;
            lightingParticles.SetActive(false);
            burstParticles.SetActive(true);*/

            // Destroy after some time
            Destroy(gameObject, 2f);
        }
    }
}

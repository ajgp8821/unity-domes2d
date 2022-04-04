using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefabController : MonoBehaviour {
    public GameObject prefab;
    public Transform point;
    public float livingTime;

    public void Instantiate() {
        GameObject instantiateObject = Instantiate(prefab, point.position, Quaternion.identity) as GameObject;

        if (livingTime > 0) {
            Destroy(instantiateObject, livingTime);
        }
    }
}

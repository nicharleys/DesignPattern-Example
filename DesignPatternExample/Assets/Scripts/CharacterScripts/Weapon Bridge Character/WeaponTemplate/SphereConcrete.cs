using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereConcrete : WeaponSphere {
    private void Awake() {
        WeaponInit(gameObject);
    }
    private void FixedUpdate() {
        WeaponUpdate(1.2f);
    }
    private void OnCollisionEnter(Collision collision) {
        WeaponCollision(collision);
    }
}

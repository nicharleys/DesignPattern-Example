using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeConcrete : WeaponCube {
    private void Awake() {
    }
    private void FixedUpdate() {
        WeaponUpdate(1.2f);
    }
    private void OnCollisionEnter(Collision collision) {
        WeaponCollision(collision);
    }
}

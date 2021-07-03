using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleConcrete : WeaponCapsule {
    private void Awake() {
        RunWeaponAwake(gameObject, gameObject.GetComponent<AudioSource>());
    }
    private void FixedUpdate() {
        RunWeaponUpdate(gameObject, 1.2f);
    }
    private void OnCollisionEnter(Collision collision) {
        RunWeaponCollision(collision);
    }
}

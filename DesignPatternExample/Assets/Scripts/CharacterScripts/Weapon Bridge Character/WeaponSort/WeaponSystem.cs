using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : GameSystemAbstract {
    private List<WeaponAbstract> _weapon = new List<WeaponAbstract>();
    public WeaponSystem(GameFunction theFunction) : base(theFunction) {
        Initialize();
    }
    public void AddWeapon(WeaponAbstract theWeapon) {
        _weapon.Add(theWeapon);
    }
    public void RemoveWeapon(WeaponAbstract theWeapon) {
        _weapon.Remove(theWeapon);
    }
    private void UpdateWeapon(float lifeTime) {
        foreach(WeaponAbstract weapon in _weapon)
            weapon.WeaponUpdate(lifeTime);
    }
    private void CollisionWeapon(Collision collision) {
        foreach(WeaponAbstract weapon in _weapon)
            weapon.WeaponCollision(collision);
    }
}

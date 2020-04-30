using System;
using UnityEngine;

public class SemiAutoFire : MonoBehaviour, IFireMode
{
    public event Action OnFire;

    [SerializeField] private float _fireRate = 0.5f;
    private float _nextTimeToFire = 0;

    private IWeaponInput _weaponInput;

    private void Awake()
    {
        _weaponInput = GetComponent<IWeaponInput>();
    }

    public bool CanFire()
    {
        if (_weaponInput == null) { Debug.LogWarning("FireMode couldn't find WeaponInput component"); return false; }

        if (!_weaponInput.FireKeyDown()) { return false; }
        if(Time.time < _nextTimeToFire) { return false; }
        return true;
    }

    public void Fire()
    {
        OnFire?.Invoke();
        _nextTimeToFire = Time.time + _fireRate;
    }
}

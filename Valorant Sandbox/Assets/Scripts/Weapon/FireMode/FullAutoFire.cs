using System;
using UnityEngine;

public class FullAutoFire : MonoBehaviour, IFireMode
{
    public event Action OnFire;

    [SerializeField] private float _fireRate = 0.15f;
    private float _nextTimeToFire = 0;

    private IWeaponInput _weaponInput;

    private void Awake()
    {
        _weaponInput = GetComponent<IWeaponInput>();
    }

    public bool CanFire()
    {
        if(_weaponInput == null) { Debug.LogWarning("FireMode couldn't find WeaponInput component"); return false; }

        if (!_weaponInput.FireKeyDown() && !_weaponInput.FireKeyPressed()) { return false; }
        if (Time.time < _nextTimeToFire) { return false; }
        return true;
    }

    public void Fire()
    {
        OnFire?.Invoke();
        _nextTimeToFire = Time.time + _fireRate;
    }
}

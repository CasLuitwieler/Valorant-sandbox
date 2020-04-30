using System;
using System.Collections;
using UnityEngine;

public class BurstFire : MonoBehaviour, IFireMode
{
    public event Action OnFire;

    [SerializeField] private float _fireRate = 0.5f, _burstFireRate = 0.1f;
    [SerializeField] private int _nBurstBullets = 3;

    private int _currentBullet = 0;
    private float _nextTimeToFire = 0;

    private Coroutine _burstCoroutine;
    private bool _isBursting = false;

    private IWeaponInput _weaponInput;

    private void Awake()
    {
        _weaponInput = GetComponent<IWeaponInput>();
    }

    public bool CanFire()
    {
        if (_weaponInput == null) { Debug.LogWarning("FireMode couldn't find WeaponInput component"); return false; }

        if (!_weaponInput.FireKeyDown()) { return false; }
        if (Time.time < _nextTimeToFire) { return false; }
        return true;
    }

    public void Fire()
    {
        if(_isBursting) { Debug.Log("Enter Burst Fire() while still in burst"); }
        _nextTimeToFire += 999f;
        _burstCoroutine = StartCoroutine(Burst());
    }

    private IEnumerator Burst()
    {
        _isBursting = true;
        while(_currentBullet < _nBurstBullets)
        {
            OnFire?.Invoke();
            _currentBullet++;
            yield return new WaitForSeconds(_burstFireRate);
        }

        _currentBullet = 0;
        _nextTimeToFire = Time.time + _fireRate;
        _isBursting = false;
    }
}

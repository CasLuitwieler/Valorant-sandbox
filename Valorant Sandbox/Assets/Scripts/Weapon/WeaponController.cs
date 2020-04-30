using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private float _bulletDistance = 100f, _damageAmount = 10f;
    [SerializeField] private ParticleSystem _muzzleFlash = null, _bulletTracer = null;

    private int _currentFireModeIndex, _nFireModes;
    private bool _hasMultipleFireModes = false;

    private IFireMode _currentFireMode;
    private IFireMode[] _fireModes;

    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
        SetupFireModes();
    }

    private void Update()
    {
        DrawDebugRay();

        if(_currentFireMode == null) { return; }

        if (Input.GetKeyDown(KeyCode.V) && _hasMultipleFireModes)
        {
            //cycle fire mode
            Debug.Log("Previous index: " + _currentFireModeIndex);

            _fireModes[_currentFireModeIndex].OnFire -= Shoot;

            _currentFireModeIndex++;
            if (_currentFireModeIndex >= _nFireModes)
                _currentFireModeIndex = 0;
            _currentFireMode = _fireModes[_currentFireModeIndex];

            _fireModes[_currentFireModeIndex].OnFire += Shoot;
            Debug.Log("New index: " + _currentFireModeIndex);
        }

        if (_currentFireMode.CanFire())
        {
            _currentFireMode.Fire();
        }
    }

    private void Shoot()
    {
        _muzzleFlash.Play();
        _bulletTracer.Play();
        Vector3 rayStartPos = _cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        if(Physics.Raycast(rayStartPos, _cam.transform.forward, out RaycastHit hit, _bulletDistance))
        {
            if(hit.transform.TryGetComponent(out IShootable shootable))
            {
                shootable.PlayHitEffect(hit.point, hit.normal);
            }

            if(!hit.transform.TryGetComponent(out IDamageable damageable)) { return; }

            damageable.TakeDamage(_damageAmount);
        }
    }

    private void SetupFireModes()
    {
        _fireModes = GetComponentsInChildren<IFireMode>();

        _nFireModes = _fireModes.Length;
        if (_nFireModes == 0) { Debug.LogWarning("No firemode attached to weapon"); return; }
        else if(_nFireModes > 1) { _hasMultipleFireModes = true; }
        _currentFireModeIndex = 0;
        _currentFireMode = _fireModes[_currentFireModeIndex];
        _fireModes[_currentFireModeIndex].OnFire += Shoot;
    }

    private void DrawDebugRay()
    {
        Vector3 mousePos = _cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        Debug.DrawRay(mousePos, _cam.transform.forward * _bulletDistance, Color.yellow);
    }
}

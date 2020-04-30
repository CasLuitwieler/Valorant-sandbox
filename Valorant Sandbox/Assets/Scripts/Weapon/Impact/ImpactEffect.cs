using UnityEngine;

public class ImpactEffect : MonoBehaviour, IShootable
{
    [SerializeField] private GameObject _hitEffect = null;

    public void PlayHitEffect(Vector3 position, Vector3 normal)
    {
        GameObject impactEffect = (GameObject)Instantiate(_hitEffect, position, Quaternion.LookRotation(normal));
        //impactEffect.GetComponent<ParticleSystem>().Play();
        Destroy(impactEffect, 5f);
    }
}
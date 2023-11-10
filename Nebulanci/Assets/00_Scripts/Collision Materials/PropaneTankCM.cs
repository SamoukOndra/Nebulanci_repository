using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropaneTankCM : CollisionMaterials
{
    [SerializeField] new ParticleSystem particleSystem;
    [SerializeField] Transform VFX_holder;

    [SerializeField] float pushForce = 666f;
    [SerializeField] float dmg = 150;
    [SerializeField] float explosionForce = 5000;

    Vector3 _hitPoint;
    Vector3 _forceDirection;

    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        VFX_holder.gameObject.SetActive(false);
    }


    public override void Interact(Vector3 hitPoint, Quaternion rotation)
    {
        VFX_holder.gameObject.SetActive(true);
        //_hitPoint = hitPoint;
        //_forceDirection = CalculateForceDirection(rotation);

        VFX_holder.position = hitPoint;
        VFX_holder.transform.rotation = rotation;
        particleSystem.Play();
    }


    public void Push()
    {
        //rigidbody.AddForceAtPosition(_forceDirection * pushForce * Time.deltaTime, _hitPoint, ForceMode.Force);
        rigidbody.AddForceAtPosition(VFX_holder.forward * pushForce * Time.deltaTime, VFX_holder.position, ForceMode.Force);
    }

    private Vector3 CalculateForceDirection(Quaternion rotation)
    {
        Vector3 direction = rotation * Vector3.back;
        Debug.Log(direction);
        return direction.normalized;
    }

    public void Explode()
    {
        GameObject explosionGO = ExplosionPool.explosionPoolSingleton.GetPooledExplosion(gameObject, dmg, explosionForce);
        if (explosionGO != null)
        {
            explosionGO.transform.position = VFX_holder.position;
            explosionGO.SetActive(true);
        }
    }
}

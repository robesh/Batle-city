using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float _baseDamage;
    public LayerMask _damagebleMask;
    public float _explosionRadius = 0.5f;
    public float _explosionForce = 100f;

    public const int DAMAGE_TYPE_SHELL = 1;
    public const int DAMAGE_TYPE_BLASTER = 2;
    public const int DAMAGE_TYPE_NONE = -1;

    public const int LAYER_MASK_DAMEGABLE = 10;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Damagerable dmgObj = other.gameObject.GetComponent<Damagerable>();
        if (dmgObj == null)
        {
            ComponentManager cm = other.GetComponent<ComponentManager>();
            if (cm != null) dmgObj = cm.getDamagerable();
            else
            {
                if (Damagerable.GetDamagerableType(other.gameObject.tag) != Damagerable.DAMAGERABLE_TYPE_OUTERWALLS)
                {
                    Debug.LogError("Error have no Damagerable");
                    Destroy(gameObject);
                    return;
                }
                else
                {/*Outer Walls*/}
            }
        }

        switch (GetDamagerType(gameObject.tag))
        {
            case DAMAGE_TYPE_SHELL: BlowingShell(other.gameObject); break;
            case DAMAGE_TYPE_BLASTER: BlasterDamage(other.gameObject); break;

            case DAMAGE_TYPE_NONE: Debug.LogError("Error damager type"); break;
        }
        
        //dmgObj.TakeDamage(_baseDamage);

        //Destroy(gameObject);
    }

    private void BlowingShell(GameObject collisionObject)
    {
        //animation
        //self blowing animation

        //other blowing animation
        ///////Damegable
        //blow radius
        Damagerable dmgObj;
        Rigidbody targetRiggidbody;
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _damagebleMask);
        if (Damagerable.GetDamagerableType(collisionObject.tag) == Damagerable.DAMDGERABLE_TYPE_ENEMY)
        {
            dmgObj = collisionObject.GetComponent<Damagerable>();
            dmgObj.TakeDamage(_baseDamage);
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            targetRiggidbody = colliders[i].GetComponent<Rigidbody>();
            if (Damagerable.GetDamagerableType(colliders[i].gameObject.tag) == Damagerable.DAMDGERABLE_TYPE_ENEMY || 
                Damagerable.GetDamagerableType(colliders[i].gameObject.tag) == Damagerable.DAMDGERABLE_TYPE_PLAYER)
            {
                if (!targetRiggidbody) continue;
                targetRiggidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
            }
            float damege = CalculateBlowingDamage(targetRiggidbody.position);
            dmgObj = colliders[i].gameObject.GetComponent<Damagerable>();
            dmgObj.TakeDamage(damege);
        }
        //m_ExplosionParticles.transform.parent = null;
        //m_ExplosionParticles.Play();
        //m_ExplosionAudio.Play();
        //Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);

    }

    private void BlasterDamage (GameObject collisionObject)
    {
        if (collisionObject.layer == 10)
        {
            //TODO Blaster sound, particle
            Damagerable dmgObj = collisionObject.GetComponent<Damagerable>();
            dmgObj.TakeDamage(_baseDamage);
            Destroy(gameObject);

            return;
        }

        Destroy(gameObject);
    }

    private float CalculateBlowingDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (_explosionRadius - explosionDistance) / _explosionRadius;
        float damage = relativeDistance * _baseDamage;
        damage = Mathf.Max(0, damage);
        return damage;
    }

    public static int GetDamagerType(string type)
    {
        if (type == "Shell") return DAMAGE_TYPE_SHELL;
        if (type == "Blaster") return DAMAGE_TYPE_BLASTER;

        return DAMAGE_TYPE_NONE;
    }
}

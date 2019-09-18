using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : WeaponController
{
    public override void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        base.Start();
        InvokeRepeating("Fire", delay, fireRate);
    }

    private void Update()
    {
       

    }

    public override void Fire()
    {
        //TODO prepare spawn position

        base.Fire(/*audioSource*/);
    }
}

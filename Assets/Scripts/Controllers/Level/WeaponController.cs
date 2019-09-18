using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : TankManager {

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate, delay;
    public float _timeBetweenBullets = 0.15f;
    public float culdown;

    private float nextFire = 0;
    private int _weaponType;
    private float _shotOffset = 0.04542f;

    private const int WEAPON_TYPE_SHELL = 1;
    private const int WEAPON_TYPE_BLASTER = 2;

    private const int WEAPON_TYPE_NONE = -1;

    // Use this for initialization
    public override void Start () {
        base.Start();
        _weaponType = GetWeaponType(gameObject.name);
        //audioSource = GetComponent<AudioSource>();
        //InvokeRepeating("Fire", delay, fireRate);
        //EnableShooting(true);
	}

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            //audioSource.Play();

            //nextFire = Time.time + culdown;
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            Fire(/*audioSource*/);
        }

    }

    private bool CanFire()
    {
        return (GetShootingPermision() && GetShootingPermisionAll());
    }

    public virtual void Fire(/*AudioSource audioSource*/)
    {
        //audioSource.Play();
        if (CanFire() && Time.time > nextFire)
        {
            if (_weaponType == WEAPON_TYPE_SHELL)
            {
                OneShot();
            }
            if (_weaponType == WEAPON_TYPE_BLASTER)
            {
                TwoShotDec();
            }
            //nextFire = Time.time + culdown;
            //Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }               
    }

    public void OneShot()
    {
        nextFire = Time.time + culdown;
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    public void TwoShotDec()
    {
        nextFire = Time.time + (culdown / 2);
        Vector3 shotPosition = shotSpawn.position;
        shotPosition.x += _shotOffset;
        Instantiate(shot, shotPosition, shotSpawn.rotation);
        _shotOffset *= -1;
    }

    public void ToShotEmid()
    {
        nextFire = Time.time + culdown;
        Vector3 shotPosition = shotSpawn.position;
        shotPosition.x += _shotOffset;
        Instantiate(shot, shotPosition, shotSpawn.rotation);
        shotPosition.x -= 2 * _shotOffset;
        Instantiate(shot, shotPosition, shotSpawn.rotation);
    }

    private int GetWeaponType(string type)
    {
        if (type == "Shell Tower") return WEAPON_TYPE_SHELL;
        if (type == "Blaster Tower") return WEAPON_TYPE_BLASTER;

        return WEAPON_TYPE_NONE;
    }
}

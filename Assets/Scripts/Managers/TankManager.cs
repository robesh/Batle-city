using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    private bool _enableMoving;
    private bool _enableShooting;
    private static bool _enableMovingAll;
    private static bool _enableShootingAll;
    private PlayerMovement playerMovement;
    private WeaponController weaponController;

    public virtual void Start()
    {
        _enableMoving = true;
        _enableShooting = true;
        //_enableMovingAll = false;
        //_enableShootingAll = false;

    }

    protected void EnableMoving(bool permision)
    {
        _enableMoving = permision;
    }

    protected void EnableShooting (bool permision)
    {
        _enableShooting = permision;
    }

    public static void EnableMovingAll (bool permision)
    {
        _enableMovingAll = permision;
    }

    public static void EnableShootingAll (bool permision)
    {
        _enableShootingAll = permision;
    }

    protected bool GetMovingPermision()
    {
        return _enableMoving;
    }

    protected bool GetShootingPermision()
    {
        return _enableShooting;
    }

    public static bool GetMovingPermisionAll()
    {
        return _enableMovingAll;
    }

    public static bool GetShootingPermisionAll()        
    {
        return _enableShootingAll;
    }
}

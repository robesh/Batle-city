using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerRotator : MonoBehaviour
{
    Rigidbody _towerRB;
    Quaternion _towerStartAngle;
    Vector3 _towerStartPosition;

    public float RotatorSpeed;

    private void Awake()
    {
        _towerRB = GetComponent<Rigidbody>();
        //_towerRB.ResetCenterOfMass();
        //_towerRB.centerOfMass = _centerOfMass.position;
        _towerStartPosition = _towerRB.position;
        _towerStartAngle = _towerRB.rotation;
    }

    public void RotateTower(Quaternion angle, Vector3 position)
    {
        Quaternion previosAngle = transform.rotation;
        //_towerRB.MoveRotation(angle * _towerStartAngle);
        _towerRB.MoveRotation(Quaternion.RotateTowards(previosAngle, angle, Time.deltaTime * RotatorSpeed));
        //_towerRB.MovePosition(position - _towerStartPosition);
        //transform.rotation = Quaternion.RotateTowards(previosAngle, angle, Time.deltaTime * RotatorSpeed);
        transform.position = position;
        //_towerRB.MovePosition(position);
    }
}

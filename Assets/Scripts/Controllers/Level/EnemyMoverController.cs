using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoverController : TankManager
{
    public float _speed;
    public float _turnSpeed;
    public float _nawTime = 1;

    private Rigidbody _rb;

    private WaitForSeconds _waitNaw;

    public EnemyMoverController (float speed, float turnSpeed)
    {
        _speed = speed;
        _turnSpeed = turnSpeed;
    }

    public override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody>();
        _waitNaw = new WaitForSeconds(_nawTime);
        StartCoroutine(EnemyMove());
        //EnemyMove_();
    }

    public void Init(float speed, float turnSpeed)
    {
        _speed = speed;
        _turnSpeed = turnSpeed;
    }

    private void FixedUpdate()
    {
        
    }

    private IEnumerator EnemyMove()
    {
        int h, v;

        while (true)
        { 
            h = Random.Range(-1, 2);
            v = Random.Range(-1, 2);
            if (Mathf.Abs(h) + Mathf.Abs(h) == 2)
            {
                if (Random.Range(-1, 2) > 0) h = 0;
                else v = 0;
            }
            //_rb.velocity = transform.forward * _speed;
            Move(h, v);

            yield return _waitNaw;
        }
    }

    //private void EnemyMove_()
    //{
    //    int h, v;

    //    while (true)
    //    {
    //        h = Random.Range(-1, 2);
    //        v = Random.Range(-1, 2);
    //        if (Mathf.Abs(h) + Mathf.Abs(v) == 2)
    //        {
    //            if (Random.Range(-1, 2) > 0) h = 0;
    //            else v = 0;
    //        }
    //        //_rb.velocity = transform.forward * _speed;
    //        Move(h, v);

    //        //yield return _waitNaw;
    //    }
    //}

    private void Move(float h, float v)
    {
        Vector3 movment = new Vector3(h, 0, v);

        movment = movment.normalized * _speed;


        float turn = h * 180 + v * 90;
        transform.Rotate(0, turn, 0);

        _rb.velocity = movment;
    }
}

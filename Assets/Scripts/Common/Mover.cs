using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    Rigidbody _rb;

    public float _speed;

	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody>();
        _rb.velocity = transform.forward * _speed;
    }

    private void Update()
    {
        //rb.AddForce(transform.forward * speed);
        //rb.velocity = transform.forward * speed;
        //rb.MovePosition(transform.forward * speed * Time.deltaTime);

    }
}

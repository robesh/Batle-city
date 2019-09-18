using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject _player;

    private Vector3 _offset;

	// Use this for initialization
	void Start () {
        _player = GameObject.FindGameObjectWithTag("Player");
        _offset = transform.position - _player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = _player.transform.position + _offset; ;
    }
}

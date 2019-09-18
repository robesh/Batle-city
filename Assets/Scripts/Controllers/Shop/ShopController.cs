using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public float _turnSpeed;
    //public GameObject playerObject;

    private GameObject _playerObject;
    private Rigidbody _playerRigidbody;
    private ShopUiManager _UIManager;
    // Start is called before the first frame update
    void Start()
    {
        _playerObject = GameObject.FindGameObjectWithTag("Player");
        _playerRigidbody = _playerObject.GetComponent< Rigidbody >();
        _UIManager = _playerObject.GetComponent<ShopUiManager>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        TurnTank(h);
    }

    private void TurnTank(float turnInputValue)
    {
        float turn = turnInputValue * _turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * turnRotation);
    }

}

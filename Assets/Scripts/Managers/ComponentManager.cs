using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentManager : MonoBehaviour
{
    public GameObject _tankTower;
    public GameObject _tankWheels;
    public GameObject _tankBase;

    private PlayerMovement _playerMovement;
    private Damagerable _damagerable;
    private EnemyMoverController _enemyMover;
    // Start is called before the first frame update
    void Start()
    {
        TowerModel towerModel = _tankTower.GetComponent<TowerModel>();
        WheelModel wheelModel = _tankWheels.GetComponent<WheelModel>();
        BaseModel baseModel = _tankBase.GetComponent<BaseModel>();

        _damagerable = gameObject.GetComponent<Damagerable>();
        _damagerable.Init(baseModel.GetHp(), baseModel.GetSheld());

        if (gameObject.tag == "Player")
        {            
            _playerMovement = gameObject.GetComponent<PlayerMovement>();
            _playerMovement.Init(wheelModel.GetSpeed(), towerModel.GetTurnSpeed());
        }
        else if (gameObject.tag == "Enemy")
        {
            _enemyMover = gameObject.GetComponent<EnemyMoverController>();
            _enemyMover.Init(wheelModel.GetSpeed(), towerModel.GetTurnSpeed());
        }
    }

    public Damagerable getDamagerable() => _damagerable;
}

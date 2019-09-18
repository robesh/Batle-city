using UnityEngine;

public class PlayerMovement : TankManager
{
    public float _speed;
    public float _turnSpeed;

    private Vector3 _movment;
    //Animator anim;
    private Rigidbody _playerRigidbody;
    private int _floorMask;
    private const float _camRayLength = 100;
    private TowerRotator _towerRotator;

    public PlayerMovement(float speed, float turnSpeed)
    {
        _speed = speed;
        _turnSpeed = turnSpeed;
    }

    public override void Start()
    {
        base.Start();
    }

    public void Init(float speed, float turnSpeed)
    {
        _speed = speed;
        _turnSpeed = turnSpeed;
    }

    private void Awake()
    {
        _floorMask = LayerMask.GetMask("Floor");
        //anim = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _towerRotator = GetComponentInChildren<TowerRotator>();
        //_baseManager = GetComponentInChildren<BaseManager>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (GetMovingPermision() && GetMovingPermisionAll())
        {
            Move(h, v);
            TurnTower();

            if (v == 1)
            {
                if (transform.rotation.y < 180 && transform.rotation.y >= 0)
                    TurnBase(1);
                else if (transform.rotation.y > -180 && transform.rotation.y < 0)
                    TurnBase(-1);
            }
            if (v == -1)
            {
                if (transform.rotation.y < 180 && transform.rotation.y >= 0)
                    TurnBase(-1);
                else if (transform.rotation.y > -180 && transform.rotation.y < 0)
                    TurnBase(1);
            }
            if (h == 1)
            {
                if (transform.rotation.y < 270 && transform.rotation.y >= 0)
                    TurnBase(1);
                else if (transform.rotation.y > -270 && transform.rotation.y < 0)
                    TurnBase(-1);
            }
            if (h == -1)
            {
                if (transform.rotation.y < 270 && transform.rotation.y >= 0)
                    TurnBase(-1);
                else if (transform.rotation.y > -270 && transform.rotation.y < 0)
                    TurnBase(1);
            }
        }
        //TurnBase(h);

        //Animating(h, v);
    }

    private void Move (float h, float v)
    {
        _movment.Set(h, 0, v);

        _movment = _movment.normalized * _speed * Time.deltaTime;

        _playerRigidbody.MovePosition(transform.position + _movment);
    }

   private void TurnTower()
    {
        //Camera camera;// = Camera.main;// Camera.main.ScreenPointToRay(Input.mousePosition);

        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast (camRay,out floorHit, _camRayLength, _floorMask))
        {
            Vector3 playerToMouse = -(floorHit.point - transform.position);

            playerToMouse.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            _towerRotator.RotateTower(newRotation, transform.position);
            //playerRigidbody.MoveRotation(newRotation);
        }
    }

    private void TurnBase(float turnInputValue)
    {
        float turn = turnInputValue * _turnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        _playerRigidbody.MoveRotation(_playerRigidbody.rotation * turnRotation);
    }

    /*void Animating(float h, float v)
    {
        bool walking = h != 0 || v != 0;
        anim.SetBool("IsWalking", walking);
    }*/
}

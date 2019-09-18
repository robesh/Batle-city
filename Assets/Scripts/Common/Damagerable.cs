using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagerable : MonoBehaviour
{
    public float _HP;
    public float _sheld;

    private Rigidbody _rb;

    public const int DAMDGERABLE_TYPE_PLAYER = 1;
    public const int DAMDGERABLE_TYPE_ENEMY = 2;
    public const int DAMDGERABLE_TYPE_BASE = 3;
    public const int DAMAGERABLE_TYPE_OUTERWALLS = 4;
    public const int DAMAGERABLE_TYPE_INNERWALLS = 5;

    private const int DAMDGERABLE_TYPE_NONE = -1;

    private bool isDestroyed = false;

    public Damagerable(float hp, float sheld)
    {
        _HP = hp;
        _sheld = sheld;
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO getsounds, particles
        _rb = GetComponent<Rigidbody>();
    }

    public void Init(float hp, float sheld)
    {
        _HP = hp;
        _sheld = sheld;
    }

    public /*virtual*/ void TakeDamage(float damage)
    {
        if (isDestroyed) return;
        _HP -= damage;

        if (_HP <= 0)
        {
            isDestroyed = true;
            //TODO player or enemy
            switch (GetDamagerableType(gameObject.tag))
            {
                case DAMDGERABLE_TYPE_PLAYER: GameManager.RoundResult(false); break;
                case DAMDGERABLE_TYPE_ENEMY: GameManager.CountEnemy(); break;
                case DAMDGERABLE_TYPE_BASE: GameManager.RoundResult(false); break;
                case DAMAGERABLE_TYPE_INNERWALLS: /*sound inner walls*/ break;
                case DAMAGERABLE_TYPE_OUTERWALLS: break;

                case DAMDGERABLE_TYPE_NONE: Debug.LogError("Error damagerable type"); break;
            }

            DestroyGameObject();
        }
    }

    private void DestroyGameObject()
    {
        //TODO destroy animation, sound
        Destroy(gameObject);
    }

    public static int GetDamagerableType(string type)
    {
        if (type == "Player") return DAMDGERABLE_TYPE_PLAYER;
        if (type == "Enemy") return DAMDGERABLE_TYPE_ENEMY;
        if (type == "Base") return DAMDGERABLE_TYPE_BASE;
        if (type == "OuterWalls") return DAMAGERABLE_TYPE_OUTERWALLS;
        if (type == "InnerWalls") return DAMAGERABLE_TYPE_INNERWALLS;

        return DAMDGERABLE_TYPE_NONE;
    }
}

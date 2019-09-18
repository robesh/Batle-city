using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int _damagePerShot = 20;
    public float _timeBetweenBullets = 0.15f;
    public float _range = 100f;
    public GameObject Shell;

    float _timer;
    Ray _shootRay = new Ray();
    RaycastHit _shootHit;
    //int _shootableMask;
    //ParticleSystem gunParticles;
    
    //AudioSource gunAudio;
    //Light gunLight;
    float _effectsDisplayTime = 0.2f;


    void Awake ()
    {
        //_shootableMask = LayerMask.GetMask ("Shootable");
        //gunParticles = GetComponent<ParticleSystem> ();
        //gunLine = GetComponent <LineRenderer> ();
        //gunAudio = GetComponent<AudioSource> ();
        //gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        _timer += Time.deltaTime;

		if(Input.GetButton ("Fire1") && _timer >= _timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot ();
        }

        if(_timer >= _timeBetweenBullets * _effectsDisplayTime)
        {
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        //gunLine.enabled = false;
        //gunLight.enabled = false;
    }


    void Shoot ()
    {
        _timer = 0f;

        //gunAudio.Play ();

        //gunLight.enabled = true;

        //gunParticles.Stop ();
        //gunParticles.Play ();

        //gunLine.enabled = true;
        //gunLine.SetPosition (0, transform.position);

        _shootRay.origin = transform.position;
        _shootRay.direction = transform.forward;

        //if(Physics.Raycast (_shootRay, out _shootHit, _range, _shootableMask))
        //{
        //    EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
        //    if(enemyHealth != null)
        //    {
        //        enemyHealth.TakeDamage (damagePerShot, shootHit.point);
        //    }
        //    //gunLine.SetPosition (1, shootHit.point);
        //}
        //else
        //{
        //    //gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        //}
    }
}

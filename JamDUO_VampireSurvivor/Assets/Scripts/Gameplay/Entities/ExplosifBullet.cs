using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosifBullet : MonoBehaviour
{
    [SerializeField] int _team;
    [SerializeField] float _timeToLive = 10.0f;
    
    [SerializeField] ParticleSystem ExplosifPillFX;
    [SerializeField] float radius = 1f;
    [SerializeField] float height = 2f;
    [SerializeField] GameObject mesh;
    [SerializeField] GameObject area;
    [SerializeField] AudioClip explosionClip;


    float _speed = 10;
    float _damage = 5;
    Vector3 _direction;
    bool move = true;

    public void Initialize(Vector3 direction, float damage, float speed)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, _timeToLive);
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);

    }

    void Update()
    {
        if(move)
        {
            transform.position += _direction * _speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
        }
      
        transform.Rotate(0, 240f * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider col)
    {
        var other = HitWithParent.GetComponent<Unit>(col);

        if (other == null)
        {

        }
        else if (other.Team != _team)
        {
            Explosion(transform.position, _direction);
        }
    }

    void Explosion(Vector3 pos, Vector3 dir)
    {
        
        AudioSource a = gameObject.AddComponent<AudioSource>();
        a.playOnAwake = false;
        a.clip = explosionClip ;
        a.Play();
        Destroy(a, 1.5f); ;

        move = false;
        ExplosifPillFX.Play();
        Destroy(gameObject, 1.5f);
        mesh.SetActive(false);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            Unit unit = collider.GetComponent<Unit>();

            if (unit != null)
            {
                if(unit.Team != _team)
                {
                    Debug.Log("explosion dmg");
                    unit.Hit(_damage);
                }
             
            }
        }   
    }
}

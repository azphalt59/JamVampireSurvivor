using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosifBullet : MonoBehaviour
{
    [SerializeField] int _team;
    [SerializeField] float _timeToLive = 10.0f;
    [SerializeField] GameObject explosifArea;
    [SerializeField] ParticleSystem ExplosifPillFX;

    float _speed = 10;
    float _damage = 5;
    Vector3 _direction;

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
        transform.position += _direction * _speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
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
            Explosion();
        }
    }

    void Explosion()
    {
        ExplosifPillFX.Play();
        explosifArea.SetActive(true);
    }


}

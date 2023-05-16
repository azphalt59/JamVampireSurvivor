using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOlympic : MonoBehaviour
{
    [SerializeField] int _team;
    [SerializeField] float _timeToLive = 5.0f;

    float _speed = 10;
    float _damage = 5;
    Vector3 _direction;
    // Start is called before the first frame update

    public void Initialize(Vector3 direction, float damage, float speed)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
    }
    void Start()
    {
        GameObject.Destroy(gameObject, _timeToLive);
        transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
    }

    // Update is called once per frame
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
            other.Hit(_damage);
           
        }
    }
}

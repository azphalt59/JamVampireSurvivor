using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Represents a bullet moving in a given direction
/// A bullet can be fired by the player or by an enemy
/// call Initialize to set the direction
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] int _team;
    [SerializeField] float _timeToLive = 10.0f;

    float _speed = 10;
    float _damage = 5;
    Vector3 _direction;

    public void Initialize(Vector3 direction , float damage , float speed )
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, _timeToLive);
        Vector3 lookDir = (transform.position + _direction) - transform.position;
        var dir = _direction;
        dir.x = 0;
        dir.z = 0;
        transform.LookAt(lookDir);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime;
        transform.GetChild(0).Rotate(new Vector3(5, 0, 0));
    }

    private void OnTriggerEnter(Collider col)
    {
        var other = HitWithParent.GetComponent<Unit>(col);

        if (other == null)
        {
            GameObject.Destroy(gameObject);
        }
        else if (other.Team != _team)
        {
            GameObject.Destroy(gameObject);

            other.Hit(_damage);
        }
    }
}
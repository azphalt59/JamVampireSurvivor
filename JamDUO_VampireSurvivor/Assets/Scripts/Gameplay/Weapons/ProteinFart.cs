using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinFart : MonoBehaviour
{
   
    public float damage;
    private void Start()
    {
        Destroy(gameObject, 1f);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<EnemyController>() != null)
        {
            other.GetComponent<EnemyController>().Hit(damage * Time.deltaTime);
        }
    }

    public void SetDamage(float amount)
    {
        damage = amount;
    }
}

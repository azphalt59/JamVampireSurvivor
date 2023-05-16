using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProteinFart : MonoBehaviour
{
   
    public float damage;
    public ParticleSystem RoarFx;
    public float scaleMultiplier = 5;
    private void Start()
    {
        Destroy(gameObject, 2f);
        RoarFx.gameObject.transform.localScale = new Vector3(2*scaleMultiplier, 2*scaleMultiplier, 2*scaleMultiplier);
        RoarFx.Play();
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
    public void SetScaleMultiplier(float amount)
    {
        amount = scaleMultiplier;
    }
}

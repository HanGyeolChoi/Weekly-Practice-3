using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float damage;
    public float damageRate;

    private List<IDamagable> damagables = new List<IDamagable>();

    private void Start()
    {
        StartCoroutine("DealDamage");
    }
    private IEnumerator DealDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(damageRate);
            for (int i = 0; i < damagables.Count; i++)
            {
                damagables[i].TakeDamage(damage);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagables.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagables.Remove(damagable);
        }
    }
}
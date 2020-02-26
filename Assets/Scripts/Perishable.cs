using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perishable : MonoBehaviour
{
    public int maxHealth = 1;
    public GameObject explosionEffect;
    public float destroyEffectDuration = 1.5f;
    public bool dieOverTime = false;
    public float lifeTime = .5f;
    int health;
    // Start is called before the first frame update
    void Start()
    {
        if (!dieOverTime)
            health = maxHealth;
        else
            Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int doDamage(int damage)
    {
        health -= damage;
        isDeath();
        return health;
    }

    public void killEntity()
    {
        health = 0;
        isDeath();
    }

    public bool isDeath()
    {
        bool isDeath = false;
        if (health <= 0)
        {
            var explosioneff = Instantiate(explosionEffect);
            explosioneff.transform.position = GetComponent<Transform>().position;
            Destroy(gameObject);
            Destroy(explosioneff, destroyEffectDuration);
            isDeath = true;
        }
        return isDeath;
    }
}

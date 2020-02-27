using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perishable : MonoBehaviour
{
    public int maxHealth = 1;
    public GameObject explosionEffect;
    public float destroyEffectDuration = 1.5f;
    [SerializeField]
    private bool dieOverTime = false;
    private float lifeTime = .5f;
    private string receivedDamageType;
    private bool isTrace = false;
    private GameObject target;
    int health;
    // Start is called before the first frame update
    void Awake()
    {
        if (GetComponent<ParticleSystem>())
            isTrace = true;
        if (!dieOverTime)
        {
            health = maxHealth;
            receivedDamageType = null;
        }
        else
            Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (isTrace)
        {
            if(target)
                transform.position = target.transform.position;
            else
            {
                var emitter = GetComponent<ParticleSystem>();
                emitter.Stop();
            }
        }
    }

    public int doDamage(int damage, string damageType)
    {
        health -= damage;
        receivedDamageType = damageType;
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
            if (destroyEffectDuration > 0)
            {
                Destroy(explosioneff, destroyEffectDuration);
            }
            isDeath = true;
        }
        return isDeath;
    }

    public void setLifeTime(float value)
    {
        lifeTime = value;
        if(dieOverTime)
            Destroy(gameObject, lifeTime);
    }
    public void setDieOverTime(bool dieOverTime)
    {
        this.dieOverTime = dieOverTime;
        setLifeTime(lifeTime);
    }

    public GameObject getTarget() { return target; }
    public void setTarget(GameObject target) { this.target = target; }
}

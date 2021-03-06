﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perishable : MonoBehaviour
{
    public int maxHealth = 1;
    public GameObject explosionEffect;
    public float destroyEffectDuration = 1.5f;
    public float heightExplosion = 0f;
    [SerializeField]
    private bool dieOverTime = false;
    private float lifeTime = .5f;
    private string receivedDamageType;
    public bool isTrace = false;
    public bool isExplosion = false;
    private bool isPlayer = false;
    public bool onlyBomb;
    private GameObject target;
    private GameObject manager = null;
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
        if(damageType == "bullet" && !onlyBomb)
            health -= damage;
        else
        {
            health -= damage;
        }
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
            if (manager != null && isPlayer) {
                manager.GetComponent<GameManager>().death();
            }

            if (manager != null && !isPlayer)
            {
                manager.GetComponent<GameManager>().enemyKill();
            }

            var explosioneff = Instantiate(explosionEffect);

            if (isExplosion)
            {
                explosioneff.transform.position = new Vector3(transform.position.x, transform.position.y + heightExplosion, transform.position.z);
            }
            else
            {
                explosioneff.transform.position = transform.position;
            }

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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "DeathPlane")
        {
            killEntity();
        }
    }
    public void setPlayer(bool set) { isPlayer = set; }
    public GameObject getTarget() { return target; }
    public void setTarget(GameObject target) { this.target = target; }
    public void setManager(GameObject manager) { this.manager = manager; }
}

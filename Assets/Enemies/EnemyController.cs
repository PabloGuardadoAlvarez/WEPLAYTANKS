using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float totalHealth, actualHealth;
    public Image healthBar;
    public GameObject explosionEffect;

    private void Awake()
    {
        actualHealth = totalHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHealth <= 0) {
            var explosioneff = Instantiate(explosionEffect);
            explosioneff.transform.position = GetComponent<Transform>().position;
            Destroy(gameObject);
            Destroy(explosioneff, 1.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet") {
            actualHealth--;
            healthBar.fillAmount = actualHealth / totalHealth;
        }
    }
}

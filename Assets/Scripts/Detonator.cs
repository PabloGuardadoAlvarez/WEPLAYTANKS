using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detonator : MonoBehaviour
{
    public float detonationDelayTime = 8f;
    public float detonationActivateTime = 2f;
    public GameObject explosionIndicator;
    [SerializeField]
    private float detonationRadius = 5f;
    [SerializeField]
    private int damage = 1;
    Perishable perish;
    Renderer cubeRenderer;
    private bool started = false;
    private GameObject bomber;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.yellow);
        perish = GetComponent<Perishable>();
        StartCoroutine(waitTime());
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(GetComponent<Transform>().position, detonationRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Perishable otherPerishable = hitColliders[i].gameObject.GetComponent<Perishable>();
            if (otherPerishable != null && bomber.gameObject != hitColliders[i].gameObject)
            {
                StartCoroutine(bombBlink());
                StartCoroutine(explode());
                started = true;
            }
        }
    }

    public void SetBomber(GameObject bomber)
    {
        this.bomber = bomber;
        Debug.Log(bomber.name + " setted a Bomb.");
    }
    public GameObject GetBomber() { return bomber; }

    IEnumerator waitTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(detonationDelayTime);
            if (!started)
            {
                StartCoroutine(bombBlink());
                StartCoroutine(explode());
                var explosionRadius = Instantiate(explosionIndicator);
                explosionRadius.transform.position = transform.position;
                explosionRadius.transform.localScale = explosionRadius.transform.localScale * detonationRadius;
                Destroy(explosionRadius, 0.5F);
            }
        }
    }

    IEnumerator bombBlink()
    {
        while (true)
        {
            cubeRenderer.material.SetColor("_Color", Color.white);
            yield return new WaitForSeconds(.3f);
            cubeRenderer.material.SetColor("_Color", Color.yellow);
            yield return new WaitForSeconds(.3f);
        }
    }

    IEnumerator explode()
    {
        while (true)
        {
            yield return new WaitForSeconds(detonationActivateTime);
            Collider[] hitColliders = Physics.OverlapSphere(GetComponent<Transform>().position, detonationRadius);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Perishable otherPerishable = hitColliders[i].gameObject.GetComponent<Perishable>();
                if (otherPerishable)
                {
                    otherPerishable.doDamage(damage, "bomb");
                }
            }
            perish.doDamage(1, "self");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Renderer cubeRenderer;
    private bool started = false;
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        cubeRenderer.material.SetColor("_Color", Color.yellow);
        StartCoroutine(waitTime());
    }
    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(GetComponent<Transform>().position, 5f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag == "enemy")
            {
                StartCoroutine(bombBlink());
                started = true;
            }
        }
    }

    IEnumerator bombBlink()
    {
        while (true)
        {
            StartCoroutine(explote());
            cubeRenderer.material.SetColor("_Color", Color.white);
            yield return new WaitForSeconds(.3f);
            cubeRenderer.material.SetColor("_Color", Color.yellow);
            yield return new WaitForSeconds(.3f);  
        }
    }

    IEnumerator waitTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
            if (!started) {
                StartCoroutine(bombBlink());
            }
        }
    }

    IEnumerator explote()
    {
        while (true)
        {
            yield return new WaitForSeconds(4f);
            var explosioneff = Instantiate(explosionEffect);
            explosioneff.transform.position = GetComponent<Transform>().position;
            Destroy(gameObject);
            Destroy(explosioneff,1.5f);
            Collider[] hitColliders = Physics.OverlapSphere(GetComponent<Transform>().position, 5f);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.tag == "enemy")
                {
                    Destroy(hitColliders[i].gameObject);
                }
            }

        }
    }

}

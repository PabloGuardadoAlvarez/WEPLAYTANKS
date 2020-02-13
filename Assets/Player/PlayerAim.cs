using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using UnityEngine.UI;

public class PlayerAim : MonoBehaviour
{

    public GameObject bullet, puntaPistola, explosionEffect;
    public int numBullets,bulletsLeft;
    public Image[] bullets;
    private bool isReloading;

    private void Awake()
    {
        bulletsLeft = numBullets;
        isReloading = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < bulletsLeft)
            {
                bullets[i].enabled = true;
            }
            else {
                bullets[i].enabled = false;
            }
        }

        if (bulletsLeft == 0 && !isReloading) {
            StartCoroutine("reload");
        }

        //mirar con el raton
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z), Vector3.up);
        }

        if (Input.GetMouseButtonDown(0) && bulletsLeft > 0)
        {
            bulletsLeft --;
            //Effectos de la exlosion del disparo
            var explosion =Instantiate(explosionEffect);
            explosion.transform.position = puntaPistola.transform.position;

            //movimiento de camara
            CameraShaker.Instance.ShakeOnce(1.5f,2f,.1f,1f);

            //Instanciamos la bala y le damos velocidad
            var disparo = Instantiate(bullet);
            disparo.transform.position = puntaPistola.transform.position;
            var balaRb = disparo.GetComponent<Rigidbody>();
            balaRb.AddForce(transform.forward * 375f);
            //balaRb.velocity = transform.forward * 10f;

            //destruimos la bala
            Destroy(explosion, 1);
        }

    }

    IEnumerator reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1.5f);
        bulletsLeft = numBullets;
        isReloading = false;

    }
}

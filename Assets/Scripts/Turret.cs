using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int numBullets = 3, bulletsLeft;
    public float bulletSpeed = 375f;
    public GameObject bullet;
    public GameObject explosionEffect;
    public GameObject gunMuzzle;
    public GameObject[] bullets;

    // Start is called before the first frame update
    void Start()
    {
        bulletsLeft = numBullets;
        bullets = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool doShot()
    {
        bool success = false;
        if (bulletsLeft > 0)
        {
            bulletsLeft--;
            //Effectos de la exlosion del disparo
            var explosion = Instantiate(explosionEffect);
            explosion.transform.position = gunMuzzle.transform.position;

            //movimiento de camara
            CameraShaker.Instance.ShakeOnce(1.5f, 2f, .1f, 1f);

            //Instanciamos la bala y le damos velocidad
            var shot = Instantiate(bullet);
            shot.transform.position = gunMuzzle.transform.position;
            var bullet_rb = shot.GetComponent<Rigidbody>();
            bullet_rb.AddForce(transform.forward * bulletSpeed);
            shot.GetComponent<Projectile>().setShooter(this.gameObject);
            //balaRb.velocity = transform.forward * 10f;

            //Destruyo detonación por disparo
            Destroy(explosion, 1);
            success = true;
        }
        return success;
    }

    public void addBullet()
    {
        bulletsLeft++;
    }
    

    public bool hasBullets()
    {
        bool hasBullets = false;
        if (bulletsLeft > 0)
            hasBullets = true;
        return hasBullets;
    }

    public void rotateTurret(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000))
        {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z), Vector3.up);
        }
    }

    public void aimTurret(Vector3 aim) {
        transform.LookAt(new Vector3(aim.x, transform.position.y, aim.z), Vector3.up);
    }
}

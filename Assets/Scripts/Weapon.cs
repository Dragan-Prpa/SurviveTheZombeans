using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVelocity = 30f;
    public float bulletLifeTime = 3f;

    public bool isAutomatic = true;
    public float fireRate = 0.1f;
    private float nextFireTime = 0;
    public float MagSize = 30f;
    public float currentAmmo = 150f;
    private float Mag;
    public TMP_Text ammoText;
    public AudioSource audioSource;

    public AudioClip shot;
    public AudioClip reload;
    public float reloadTime = 1f;

    private bool isReloading = false;

    
    public Slider reloadBar;

    void Start()
    {
        Mag = MagSize;
        reloadBar.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Reload") && !isReloading)
        {        
            StartCoroutine(Reload());
            audioSource.PlayOneShot(reload);
            
        }


        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && Mag > 0 && !isReloading && isAutomatic)
        {
            FireWeapon();
            nextFireTime = Time.time + fireRate;
            Mag--;
        }
        if (Input.GetButtonDown("Fire1") && Mag > 0 && !isReloading && !isAutomatic)
        {
            FireWeapon();
            Mag--;
        }
         ammoText.text = Mag + " / " + currentAmmo;
    }

    private void FireWeapon()
    {
        audioSource.PlayOneShot(shot);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation * Quaternion.Euler(0, 0, 0));
        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);
        StartCoroutine(DestroyBullet(bullet, bulletLifeTime));
    }

    private IEnumerator DestroyBullet(GameObject bullet, float bulletLifeTime)
    {
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadBar.gameObject.SetActive(true);
        reloadBar.value = 0;
        float elapsed = 0f;

        while (elapsed < reloadTime)
        {
            elapsed += Time.deltaTime;
            reloadBar.value = Mathf.Clamp01(elapsed / reloadTime);
            yield return null;
        }
        if (currentAmmo >= MagSize-Mag)
        {
            currentAmmo = currentAmmo - MagSize + Mag;
            Mag = MagSize;
        }
        else
        {
            Mag += currentAmmo;  
            currentAmmo = 0;
            
        }
            isReloading = false;
        reloadBar.gameObject.SetActive(false);
    }
}

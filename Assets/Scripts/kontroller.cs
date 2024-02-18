using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f;
    public Text endGameText;
    public Text dusmanSayisiText;
    public Button restartButton; // Yeniden ba�latma butonu

    private int dusmanSayisi = 0;
    private float nextFire = 0.0f;

    void Start()
    {
        endGameText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false); // Ba�lang��ta yeniden ba�latma butonunu gizle
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * bulletSpeed;
        Destroy(bullet, 2.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Engel"))
        {
            GameOver();
        }
        else if (other.gameObject.CompareTag("Dusman"))
        {
            Destroy(other.gameObject);
            dusmanSayisi++;
            dusmanSayisiText.text = "Yok Etti�iniz Ter�rist S���naklar�n�n Say�s�: " + dusmanSayisi;
        }
    }

    void GameOver()
    {
        endGameText.gameObject.SetActive(true);
        endGameText.text = "Da�a �arpt�n�z! Oyun bitti!";
        restartButton.gameObject.SetActive(true); // Yeniden ba�latma butonunu g�r�n�r hale getir
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

}

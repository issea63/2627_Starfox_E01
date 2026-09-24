using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header ("Objects")]
    public GameObject explosionEffect;
    public AudioSource explosionSound;
    public Image healthBar;
    [Header ("Stats")]
    public float maximunHealth = 100f;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maximunHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0 )
        {
            StartCoroutine(DeathRoutine());
        }
        UpdateHealthBar();
    }

    public void CureDamage(float cureAmount)
    {
        currentHealth += cureAmount;
        if( currentHealth <= maximunHealth )
        {
            currentHealth = maximunHealth;
        }
        UpdateHealthBar();
    }
    public void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maximunHealth;
    }

    public IEnumerator DeathRoutine()
    {
        Instantiate(explosionEffect,gameObject.transform.position,Quaternion.identity);
        explosionSound.Play();
        gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }
}

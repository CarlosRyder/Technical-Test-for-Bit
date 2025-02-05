using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    TMP_Text healthText;
    public int maxHealth = 10;

    void Awake()
    {
        healthText = GameObject.Find("HealthText").GetComponent<TMP_Text>();
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        healthText.text = health.ToString();
    }

    void Update()
    {
        healthText.text = "Health: " + health + "/" + maxHealth;
    }
}

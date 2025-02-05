using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int health;
    TMP_Text healthText;
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
        healthText.text = "Health: " + health;
    }
}

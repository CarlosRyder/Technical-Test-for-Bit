using TMPro;
using UnityEngine;

public class AttackDamange : MonoBehaviour
{
    int damange;
    TMP_Text damangeText;
    void Awake()
    {
        damangeText = GameObject.Find("DamangeText").GetComponent<TMP_Text>();
    }

    public void ChangeDamange(int amount)
    {
        damange += amount;
        damangeText.text = damange.ToString();
    }

    void Update()
    {
        damangeText.text = "Attack Damange: " + damange;
    }
}

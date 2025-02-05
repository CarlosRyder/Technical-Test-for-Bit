using System.Collections;
using TMPro;
using UnityEngine;

public class WelcomeText : MonoBehaviour
{
    private TMP_Text welcomePhrase;

    string frase = "These are my weapons, not yours!\n\nBut if you explore you can \n\nfind some of these!";

    void Awake()
    {
        welcomePhrase = GetComponent<TMP_Text>();
    }

    void OnEnable()
    {
        StartCoroutine(Reloj());
    }

    IEnumerator Reloj()
    {
        welcomePhrase.text = ""; // Ensure the text starts empty
        foreach (char caracter in frase)
        {
            welcomePhrase.text += caracter;
            yield return new WaitForSeconds(0.06f);
        }
    }
}

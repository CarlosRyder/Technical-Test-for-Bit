using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itenName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;

    public bool UseItem()
    {
        if (attributesToChange == AttributesToChange.attackDamange)
        {
            GameObject.Find("AttackDamangeManager").GetComponent<AttackDamange>().ChangeDamange(amountToChangeAttribute);
        }
        
        if (statToChange == StatToChange.health)
        {
            PlayerHealth playerHealth = GameObject.Find("HealthManager").GetComponent<PlayerHealth>();
            if (playerHealth.health == playerHealth.maxHealth)
            {
                return false;
            }
            else
            {
                playerHealth.ChangeHealth(amountToChangeStat);
                return true;
            }
        }
        return false;
    }

    public enum StatToChange
    {
        none,
        health,
    };

    public enum AttributesToChange
    {
        none,
        attackDamange,
    };
}

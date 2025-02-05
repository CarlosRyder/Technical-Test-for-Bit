using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itenName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttribute;
    public void UseItem() 
    {
        if (statToChange == StatToChange.health)
        {
            GameObject.Find("HealthManager").GetComponent<PlayerHealth>().ChangeHealth(amountToChangeStat);
        }

        if (attributesToChange == AttributesToChange.attackDamange)
        {
            GameObject.Find("AttackDamangeManager").GetComponent<AttackDamange>().ChangeDamange(amountToChangeAttribute);
        }
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

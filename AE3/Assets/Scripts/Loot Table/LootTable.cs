using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot
{
    public GameObject Item;
    public int ItemChance;
}


[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private Loot[] Loot;

    int Total;

    public GameObject Drop()
    {
        Total = 0;

        foreach (var loot in Loot)
        {
            Total += loot.ItemChance;
        }

        int CurrentProb = Random.Range(0, Total);

        for (int i = 0; i < Loot.Length; i++)
        {
            if (CurrentProb <= Loot[i].ItemChance)
            {
                return Loot[i].Item;
            }
            else
            {
                CurrentProb -= Loot[i].ItemChance;
            }
        }

        return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot{
    public GameObject loot;
    public float chance;

}
[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;
    public GameObject LootLoot(){
        float CumProb = 0;
        float CurrentProb = Random.Range(0,100);
        for (int i=0;i<loots.Length;i++){
            CumProb += loots[i].chance;
            if (CurrentProb<=CumProb) {
                Debug.Log("yes");
                return loots[i].loot;
            }
        }
        return null;
    }
}

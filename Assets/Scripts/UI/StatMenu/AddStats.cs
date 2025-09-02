using UnityEngine;

public class AddStats : MonoBehaviour
{
    [SerializeField] private Stats localPlayerStats;
    [SerializeField] private PlayerStatsManager remotePlayerStats;

    public void AddMaxHealth()
    { 
        localPlayerStats.MaxHealth++;
        remotePlayerStats.MaxHealth++;
        Debug.Log("ADDED!! " + localPlayerStats.MaxHealth);
    }
    public void DecreasMaxHealth()
    {
        localPlayerStats.MaxHealth--;
        remotePlayerStats.MaxHealth--;
        Debug.Log("DECREASED!!" + localPlayerStats.MaxHealth);
    }
}

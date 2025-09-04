using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    public float Strength;
    public float Damage;

    public float Agility;
    public float Speed;

    public float Health;
    public float MaxHealth;
    public float Vigor;

    public float Energy;
    public float MaxEnergy;

    public float Exp;
    public float MaxExp;
    public int Level;
    public int UpgradePoints;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // avoid duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

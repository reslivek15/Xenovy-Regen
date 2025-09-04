using UnityEngine;

public class Stats : MonoBehaviour
{
    public static Stats Instance { get; private set; }

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
}

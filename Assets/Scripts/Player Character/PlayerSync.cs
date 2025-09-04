using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerSync : MonoBehaviour
{
    private Stats stats;

    void Start()
    {
        stats = GetComponent<Stats>();

        // First, try to load from file
        PlayerSaveData fileData = SaveSystem.LoadPlayer();
        if (fileData != null)
        {
            // Apply loaded data
            stats.Strength = fileData.Strength;
            stats.Damage = fileData.Damage;
            stats.Agility = fileData.Agility;
            stats.Speed = fileData.Speed;
            stats.MaxHealth = fileData.MaxHealth;
            stats.Health = fileData.Health;
            stats.Vigor = fileData.Vigor;
            stats.Energy = fileData.Energy;
            stats.MaxEnergy = fileData.MaxEnergy;
            stats.Exp = fileData.Exp;
            stats.MaxExp = fileData.MaxExp;
            stats.Level = fileData.Level;
            stats.UpgradePoints = fileData.UpgradePoints;

            PlayerStatsManager.Instance.Health = stats.Health;
            PlayerStatsManager.Instance.MaxHealth = stats.MaxHealth;
            PlayerStatsManager.Instance.Vigor = stats.Vigor;
            PlayerStatsManager.Instance.Strength = stats.Strength;
            PlayerStatsManager.Instance.Damage = stats.Damage;
            PlayerStatsManager.Instance.Agility = stats.Agility;
            PlayerStatsManager.Instance.Speed = stats.Speed;
            PlayerStatsManager.Instance.Exp = stats.Exp;
            PlayerStatsManager.Instance.MaxExp = stats.MaxExp;
            PlayerStatsManager.Instance.Level = stats.Level;
            PlayerStatsManager.Instance.UpgradePoints = stats.UpgradePoints;
        }
        else
        {
            // If no save file, load from PlayerStatsManager (scene-to-scene)
            stats.Health = PlayerStatsManager.Instance.Health;
            stats.MaxHealth = PlayerStatsManager.Instance.MaxHealth;
            stats.Vigor = PlayerStatsManager.Instance.Vigor;

            stats.Strength = PlayerStatsManager.Instance.Strength;
            stats.Damage = PlayerStatsManager.Instance.Damage;

            stats.Agility = PlayerStatsManager.Instance.Agility;
            stats.Speed = PlayerStatsManager.Instance.Speed;

            stats.Exp = PlayerStatsManager.Instance.Exp;
            stats.MaxExp = PlayerStatsManager.Instance.MaxExp;
            stats.Level = PlayerStatsManager.Instance.Level;
            stats.UpgradePoints = PlayerStatsManager.Instance.UpgradePoints;
        }
    }

    void OnDestroy()
    {
        SaveSystem.SavePlayer(CapturePlayerData());
        // Save back into PlayerStatsManager before Player gets destroyed
        if (PlayerStatsManager.Instance != null)
        {
            PlayerStatsManager.Instance.Health = stats.Health;
            PlayerStatsManager.Instance.MaxHealth = stats.MaxHealth;
            PlayerStatsManager.Instance.Vigor = stats.Vigor;

            PlayerStatsManager.Instance.Strength = stats.Strength;
            PlayerStatsManager.Instance.Damage = stats.Damage;

            PlayerStatsManager.Instance.Agility = stats.Agility;
            PlayerStatsManager.Instance.Speed = stats.Speed;

            PlayerStatsManager.Instance.Exp = stats.Exp;
            PlayerStatsManager.Instance.MaxExp = stats.MaxExp;
            PlayerStatsManager.Instance.Level = stats.Level;
            PlayerStatsManager.Instance.Level = stats.UpgradePoints;
        }
    }

    public PlayerSaveData CapturePlayerData()
    {
        PlayerSaveData data = new PlayerSaveData();
        data.Strength = stats.Strength;
        data.Damage = stats.Damage;

        data.Agility = stats.Agility;
        data.Speed = stats.Speed;

        data.Vigor = stats.Vigor;
        data.Health = stats.Health;
        data.MaxHealth = stats.MaxHealth;

        data.Energy = stats.Energy;
        data.MaxEnergy = stats.MaxEnergy;

        data.Exp = stats.Exp;
        data.MaxExp = stats.MaxExp;
        data.Level = stats.Level;
        data.UpgradePoints = stats.UpgradePoints;
        return data;
    }

    public void ApplyData(PlayerSaveData data)
    {
        if (data == null) return; // no save data found

        stats.Strength = data.Strength;
        stats.Damage = data.Damage;

        stats.Agility = data.Agility;
        stats.Speed = data.Speed;

        stats.Vigor = data.Vigor;
        stats.Health = data.Health;
        stats.MaxHealth = data.MaxHealth;

        stats.Energy = data.Energy;
        stats.MaxEnergy = data.MaxEnergy;

        stats.Exp = data.Exp;
        stats.MaxExp = data.MaxExp;
        stats.Level = data.Level;
        stats.UpgradePoints = data.UpgradePoints;
    }
}

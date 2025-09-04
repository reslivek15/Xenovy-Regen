using UnityEngine;
using TMPro;
using static UnityEngine.EventSystems.EventTrigger;
public class AddStats : MonoBehaviour
{
    [SerializeField] private Stats localPlayerStats;
    [SerializeField] private PlayerStatsManager remotePlayerStats;
    [SerializeField] private PlayerSync playerSync;

    [SerializeField] private TMP_Text upgradePointsValue;
    [SerializeField] private TMP_Text levelValue;

    [SerializeField] private TMP_Text changeVigorValue;
    [SerializeField] private TMP_Text currentVigorValue;
    [SerializeField] private TMP_Text currentHealthValue;
    [SerializeField] private TMP_Text changeHealthValue;

    [SerializeField] private TMP_Text changeStrengthValue;
    [SerializeField] private TMP_Text currentStrengthValue;
    [SerializeField] private TMP_Text currentDamageValue;
    [SerializeField] private TMP_Text changeDamageValue;

    [SerializeField] private TMP_Text changeAgilityValue;
    [SerializeField] private TMP_Text currentAgilityValue;
    [SerializeField] private TMP_Text currentSpeedValue;
    [SerializeField] private TMP_Text changeSpeedValue;

    int changeUpgradePoints;

    private void Start()
    {
        changeUpgradePoints = localPlayerStats.UpgradePoints;
        upgradePointsValue.text = localPlayerStats.UpgradePoints.ToString();
        currentHealthValue.text = localPlayerStats.MaxHealth.ToString();
        currentDamageValue.text = localPlayerStats.Damage.ToString();
        currentAgilityValue.text = localPlayerStats.Speed.ToString();
    }
    private void Update()
    {
        upgradePointsValue.text = changeUpgradePoints.ToString();
        levelValue.text = localPlayerStats.Level.ToString();

        changeVigorValue.text = localPlayerStats.Vigor.ToString();
        changeHealthValue.text = localPlayerStats.MaxHealth.ToString();

        changeStrengthValue.text = localPlayerStats.Strength.ToString();
        changeDamageValue.text = localPlayerStats.Damage.ToString();

        changeAgilityValue.text = localPlayerStats.Agility.ToString();
        changeSpeedValue.text = localPlayerStats.Speed.ToString();
        
    }

    public void AddVigor()
    { 
        if(changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Vigor++;
            remotePlayerStats.Vigor++;
            localPlayerStats.MaxHealth += 30f;
            remotePlayerStats.MaxHealth += 30f;
        }
    }
    public void DecreaseVigor()
    {
        if(changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Vigor--;
            remotePlayerStats.Vigor--;
            localPlayerStats.MaxHealth -= 30f;
            remotePlayerStats.MaxHealth -= 30f;
        }
    }
    public void AddStrength()
    {
        if (changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Strength++;
            remotePlayerStats.Strength++;
            localPlayerStats.Damage++;
            remotePlayerStats.Damage++;
        }
    }
    public void DecreaseStrength()
    {
        if (changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Strength--;
            remotePlayerStats.Strength--;
            localPlayerStats.Damage--;
            remotePlayerStats.Damage--;
        }
    }
    public void AddSpeed()
    {
        if (changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Agility++;
            remotePlayerStats.Agility++;
            localPlayerStats.Speed += 3.5f;
            remotePlayerStats.Speed += 3.5f;
        }
    }
    public void DecreaseSpeed()
    {
        if (changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Agility--;
            remotePlayerStats.Agility--;
            localPlayerStats.Speed -= 3.5f;
            remotePlayerStats.Speed -= 3.5f;
        }
    }

    public void SaveChanges()
    {
        localPlayerStats.UpgradePoints = changeUpgradePoints;
        SaveSystem.SavePlayer(playerSync.CapturePlayerData());
        currentHealthValue.text = localPlayerStats.MaxHealth.ToString();
        currentDamageValue.text = localPlayerStats.Damage.ToString();
        currentAgilityValue.text = localPlayerStats.Speed.ToString();
    }
    public void DiscardChanges()
    {
        changeUpgradePoints = localPlayerStats.UpgradePoints;
        PlayerSaveData data = SaveSystem.LoadPlayer();
        playerSync.ApplyData(data);
    }

}

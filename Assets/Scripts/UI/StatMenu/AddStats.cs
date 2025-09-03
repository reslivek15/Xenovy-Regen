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

    [SerializeField] private TMP_Text changeStrengthValue;
    [SerializeField] private TMP_Text currentStrengthValue;

    [SerializeField] private TMP_Text changeSpeedValue;
    [SerializeField] private TMP_Text currentSpeedValue;
    int changeUpgradePoints;

    private void Start()
    {
        changeUpgradePoints = localPlayerStats.UpgradePoints;
        upgradePointsValue.text = localPlayerStats.UpgradePoints.ToString();
    }
    private void Update()
    {
        upgradePointsValue.text = changeUpgradePoints.ToString();
        changeVigorValue.text = localPlayerStats.Vigor.ToString();
        changeStrengthValue.text = localPlayerStats.Strength.ToString();
        changeSpeedValue.text = localPlayerStats.Speed.ToString();
        levelValue.text = localPlayerStats.Level.ToString();
    }

    public void AddVigor()
    { 
        if(changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Vigor++;
            remotePlayerStats.Vigor++;
        }
    }
    public void DecreaseVigor()
    {
        if(changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Vigor--;
            remotePlayerStats.Vigor--;
        }
    }
    public void AddStrength()
    {
        if (changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Strength++;
            remotePlayerStats.Strength++;
        }
    }
    public void DecreaseStrength()
    {
        if (changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Strength--;
            remotePlayerStats.Strength--;      
        }
    }
    public void AddSpeed()
    {
        if (changeUpgradePoints > 0)
        {
            changeUpgradePoints--;
            localPlayerStats.Speed++;
            remotePlayerStats.Speed++;
        }
    }
    public void DecreaseSpeed()
    {
        if (changeUpgradePoints < localPlayerStats.UpgradePoints)
        {
            changeUpgradePoints++;
            localPlayerStats.Speed--;
            remotePlayerStats.Speed--;
        }
    }

    public void SaveChanges()
    {
        localPlayerStats.UpgradePoints = changeUpgradePoints;
        SaveSystem.SavePlayer(playerSync.CapturePlayerData());
    }
    public void DiscardChanges()
    {
        changeUpgradePoints = localPlayerStats.UpgradePoints;
        PlayerSaveData data = SaveSystem.LoadPlayer();
        playerSync.ApplyData(data);
    }

}

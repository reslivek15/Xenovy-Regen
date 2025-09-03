using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerHealthBar;
    [SerializeField] private GameObject healthPercentage;
    private GameObject player;
    private Stats playerStats;
    public Transform playerStation;

    private PlayerSync playerSync;

    [SerializeField] private GameObject playerEnergyBar;
    [SerializeField] private GameObject playerEnergyCount;
    

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    private Stats enemyStats;
    public Transform enemyStation;
    private GameObject enemyHealthBar;

    public GameObject damgePopUpPrefab;
    private GameObject damgePopUp;

    [SerializeField] private GameObject optionButtons;
    [SerializeField] private GameObject statsOverlay;
    [SerializeField] private GameObject attackOverlay;
    [SerializeField] private GameObject itemsOverlay;
    void Start()
    {
        Setup();
        state = BattleState.START;
    }

    private void Setup()
    {
        player = Instantiate(playerPrefab,playerStation);
        player.GetComponent<Movement>().enabled = false;
        player.GetComponent<Rotation>().enabled = false;
        player.name = "Player";
        playerStats = player.GetComponent<Stats>();
        playerStats.Strength = PlayerStatsManager.Instance.Strength;
        playerStats.Speed = PlayerStatsManager.Instance.Speed;
        playerStats.MaxHealth = PlayerStatsManager.Instance.MaxHealth;
        playerStats.Health = PlayerStatsManager.Instance.Health;
        playerStats.Exp = PlayerStatsManager.Instance.Exp;
        playerStats.MaxExp = PlayerStatsManager.Instance.MaxExp;
        playerStats.Level = PlayerStatsManager.Instance.Level;
        playerStats.UpgradePoints = PlayerStatsManager.Instance.UpgradePoints;
        playerStats.Vigor = PlayerStatsManager.Instance.Vigor;


        enemy = Instantiate(enemyPrefab, enemyStation);
        enemy.name = "Enemy";
        enemyStats = enemy.GetComponent<Stats>();
        enemyHealthBar = enemy.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject;

        if(playerStats.Speed >= enemyStats.Speed)
        {
            PlayerTurn();
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }

    }
    private void UpdatePlayerEnergy()
    {
        float playerEnergyPercentage = (float)((playerStats.Energy * 100) / playerStats.MaxEnergy);
        playerEnergyCount.GetComponent<TMP_Text>().text = (playerStats.Energy / 10).ToString() + "/ 10";
        playerEnergyBar.GetComponent<UnityEngine.UI.Image>().fillAmount = playerEnergyPercentage / 100;
    }
    private void UpdatePlayerHealth()
    {
        float playerHealthPercentage = (float)((playerStats.Health * 100) / playerStats.MaxHealth);
        healthPercentage.GetComponent<TMP_Text>().text = playerHealthPercentage.ToString() + "%";
        playerHealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = playerHealthPercentage / 100;
    }
    IEnumerator UpdateEnemyHealth()
    {
        float enemyHealthPercentage = (float)((enemyStats.Health * 100) / enemyStats.MaxHealth);

        while(enemyHealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount > enemyHealthPercentage / 100)
        {
            enemyHealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount-=0.01f;
            yield return new WaitForSeconds(0.01f);
        }     
    }
    
    private void MakeDamagePopUp()
    {
        damgePopUp = Instantiate(damgePopUpPrefab, enemy.transform.GetChild(1).transform);
        damgePopUp.GetComponent<TMP_Text>().text = playerStats.Strength.ToString();
        damgePopUp.GetComponent<DamagePopup>().MovePopup();
    }
    private void PlayerTurn()
    {
        optionButtons.SetActive(true);
        statsOverlay.SetActive(true);

        UpdatePlayerEnergy();
        UpdatePlayerHealth();
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        playerStats.Health -= enemyStats.Strength;
        PlayerStatsManager.Instance.Health = playerStats.Health;

        PlayerTurn();
    }
    public void BasicAttack()
    {
        enemyStats.Health -= playerStats.Strength;
        playerStats.Energy += 10f;
        
        MakeDamagePopUp();

        StartCoroutine(UpdateEnemyHealth());
        
        attackOverlay.SetActive(false);

        if(enemyStats.Health <= 0)
        {
            playerStats.Exp += 20;
            playerStats.Energy = 0f;
            if(playerStats.Exp > playerStats.MaxExp)
            {
                playerStats.Level++;
                playerStats.UpgradePoints++;
                playerStats.Exp-=playerStats.MaxExp;
                playerStats.MaxExp += 50f;
                
            }
            SceneManager.LoadScene("OpenWorld");
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }       
    }
}

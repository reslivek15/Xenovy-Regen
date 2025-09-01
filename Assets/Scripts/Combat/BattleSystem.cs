using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
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

    [SerializeField] private GameObject energyBar;
    

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
        player.GetComponent<Animator>().enabled = false;
        player.name = "Player";
        playerStats = player.GetComponent<Stats>();
        playerSync = player.GetComponent<PlayerSync>();

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
    }

    IEnumerator EnemyTurn()
    {
        yield return new WaitForSeconds(1f);

        optionButtons.SetActive(true);
        statsOverlay.SetActive(true);

        playerStats.Health -= enemyStats.Strength;

        float playerHealthPercentage = (float)((playerStats.Health * 100) / playerStats.MaxHealth);
        healthPercentage.GetComponent<TMP_Text>().text = playerHealthPercentage.ToString() + "%";
        playerHealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = playerHealthPercentage / 100;
    }
    public void BasicAttack()
    {
        enemyStats.Health -= playerStats.Strength;

        MakeDamagePopUp();

        StartCoroutine(UpdateEnemyHealth());
        
        attackOverlay.SetActive(false);

        if(enemyStats.Health <= 0)
        {
            playerStats.Exp += 20;
            //SaveSystem.SavePlayer(playerSync.CapturePlayerData());
            SceneManager.LoadScene("OpenWorld");
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }       
    }
}

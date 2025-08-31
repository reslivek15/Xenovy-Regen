using Microsoft.Unity.VisualStudio.Editor;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    [SerializeField] private GameObject playerPrefab;
    private GameObject player;
    private Stats playerStats;
    public Transform playerStation;

    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;
    private Stats enemyStats;
    public Transform enemyStation;
    private GameObject enemyHealthBar;

    public GameObject damgePopUpPrefab;
    private GameObject damgePopUp;

    [SerializeField] private GameObject playerActionGUI;
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
        

        enemy = Instantiate(enemyPrefab, enemyStation);
        enemy.name = "Enemy";
        enemyStats = enemy.GetComponent<Stats>();
        enemyHealthBar = enemy.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).gameObject;

        if(playerStats.Speed >= enemyStats.Speed)
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }
    
    private void PlayerTurn()
    {
        playerActionGUI.SetActive(true);
    }

    public void BasicAttack()
    {
        enemyStats.Health -= playerStats.Strength;

        damgePopUp = Instantiate(damgePopUpPrefab,enemy.transform.GetChild(1).transform);
        damgePopUp.GetComponent<TMP_Text>().text = playerStats.Strength.ToString();
        damgePopUp.GetComponent<DamagePopup>().MovePopup();


        float healthPercentage = (float)((enemyStats.Health * 100) / enemyStats.MaxHealth);
        enemyHealthBar.GetComponent<UnityEngine.UI.Image>().fillAmount = healthPercentage/100;
    }
}

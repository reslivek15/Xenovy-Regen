using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    private Stats playerStats;
    void Start()
    {
        state = BattleState.START;
    }
 
}

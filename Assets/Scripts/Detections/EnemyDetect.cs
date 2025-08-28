using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("<color=red>FIGHT TRIGGERED!! </color>");
    }
}

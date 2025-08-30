using UnityEngine;

public class EnemyDetect : MonoBehaviour
{
    public NextScene script;
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("<color=red>FIGHT TRIGGERED!! </color>");
        script.MoveScene();
    }
}

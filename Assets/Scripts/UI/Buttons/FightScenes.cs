using UnityEngine;
using UnityEngine.SceneManagement;

public class FightScenes : MonoBehaviour
{
    public void FightScene()
    {
        SceneManager.LoadScene("FightScene1");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

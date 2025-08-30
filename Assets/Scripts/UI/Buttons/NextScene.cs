using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
   private Scene thisScene;

    private void Start()
    {
        thisScene = SceneManager.GetActiveScene();
    }

    public void MoveScene()
    {
        ///Debug.Log(thisScene.buildIndex);
        SceneManager.LoadScene(thisScene.buildIndex + 1);
    }
}

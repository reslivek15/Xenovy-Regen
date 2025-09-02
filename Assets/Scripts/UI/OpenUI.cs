using UnityEngine;

public class OpenUI : MonoBehaviour
{
    [SerializeField] private GameObject statsMenu;
    public void OpenStatMenu()
    {
        if (statsMenu.activeSelf)
        {
            statsMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            statsMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

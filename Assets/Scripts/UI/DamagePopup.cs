using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    public void MovePopup()
    {
        StartCoroutine(Popup());
    }

    IEnumerator Popup()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0f);
        float yPos = transform.position.y + 1;
        while (transform.position.y < yPos)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, 0f);
            yield return new WaitForSeconds(0.005f);
        }
        Destroy(gameObject);
    }

}
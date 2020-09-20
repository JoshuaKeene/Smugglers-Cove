using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public GameObject Credits;
    
    void Update()
    {
        if (Credits.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
        {
            EndGame();
        }
    }

    public void RoleCredits()
    {
        gameObject.GetComponent<Animator>().Play(AnimationManager.RoleCredits);
        UIManager.TheUI.SubText.SetActive(false);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

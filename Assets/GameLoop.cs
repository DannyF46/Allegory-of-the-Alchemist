using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene(1);
        Debug.Log("new game?");
    }
}

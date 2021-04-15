using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public void LevelButton(int levelNum)
    {
        SceneManager.LoadScene(levelNum, LoadSceneMode.Single);
    }
    public void QuitButton()
    {
        Application.Quit();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour
{
    public void LevelButton(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }
}

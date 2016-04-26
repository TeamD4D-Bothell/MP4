using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string level1 = "Level01";

    public void loadLevel()
    {
        SceneManager.LoadScene(level1);
    }
}

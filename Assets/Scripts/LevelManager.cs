using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuManager : MonoBehaviour
{
    public void LoadLevel(int levelIndex)
    {
        Debug.Log("Chargement du niveau " + levelIndex);
        SceneManager.LoadScene(levelIndex);
        Time.timeScale = 1;
    }
}

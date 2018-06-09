using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BetweenLevels : MonoBehaviour {

	public void LevelUp()
    {
        SceneManager.LoadScene("Level2");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("StartGame");
    }
}

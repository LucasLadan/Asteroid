using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyHolder : MonoBehaviour
{
    private int difficulty = 2;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void startGame(int newDifficulty)
    {
        difficulty = newDifficulty;
        SceneManager.LoadScene("Game");
    }

    public int getDifficulty()
        { return difficulty; }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent pause;

    public UnityEvent resume;

    private float timer = 0;

    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _maxScore;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private GameObject _WinLoseButtons;
    [SerializeField] private TextMeshProUGUI _ConditionText;
    [SerializeField] private TextMeshProUGUI _Timer;

    private void Update()
    {
        timer += Time.deltaTime;
        _Timer.text = ("Time: " + timer);
    }

    public void TriggerPause()
    {
        pause.Invoke();
    }

    public void TriggerResume()
    {
        resume.Invoke();
    }

    public void UpdateScore(int score, int maxScore)
    {
        _score.text = score.ToString();
        _maxScore.text = maxScore.ToString();
    }    

    public void UpdateHealth(int health)
    {
        _health.text = health.ToString();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void YouWin()
    {
        pause.Invoke();
        _WinLoseButtons.SetActive(true);
        _ConditionText.text = "You win";
    }

    public void YouLose()
    {
        PlayerControl _player = FindObjectOfType<PlayerControl>();
        if (_player != null)
        {
            _player.Paused();
            _player.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        } 
            
        _WinLoseButtons.SetActive(true);
        _ConditionText.text = "You lose";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private float _timerValue;
    [SerializeField] private Text _timerView;

    [Header("Objects")]
    [SerializeField] private Player _player;
    [SerializeField] private Exit _exit;

    private float _timer = 0;
    private bool _gameIsEnded;

    private void Awake()
    {
        _timer = _timerValue;
        _timerView.text = $"{_timerValue:F1}";
    }

    private void Start()
    {
        _player.Enable();
        _exit.Close();
    }

    private void Update()
    {
        if (_gameIsEnded)
            return;

        TimerTick();
        TryCompleteLevel();
        LookAtPlayerHealth();
        LookAtPlayerInventory();
    }

    private void TimerTick()
    {
        _timer -= Time.deltaTime;
        _timerView.text = $"{_timer:F1}";

        if (_timer <= 0)
            Lose();
    }

    private void TryCompleteLevel()
    {
        var flatExitPosition = new Vector2(_exit.transform.position.x, _exit.transform.position.z);
        var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

        if ((flatExitPosition == flatPlayerPosition) && (_player.HasKey))
            Victory();
    }

    private void LookAtPlayerHealth()
    {
        if (_player.IsAlive)
            return;

        Lose();
        Destroy(_player.gameObject);
    }

    private void LookAtPlayerInventory()
    {
        if (_player.HasKey == false)
            return;

        _exit.Open();
    }

    private void Lose()
    {
        _gameIsEnded = true;
        _player.Disable();
        Debug.Log("Lose!");
    }

    private void Victory()
    {
        _gameIsEnded = true;
        _player.Disable();
        Debug.Log("Victory!");
    }
}

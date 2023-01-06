using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public enum GameState
    {
        Playing,
        Paused
    }

    private GameState currentState;

    public event Action<GameState> OnGameStateChanged;

    protected override void Awake()
    {
        base.Awake();
        SetGameStateWithDelay(GameState.Playing, 2f);
    }

    public void SetGameStateWithDelay(GameState state, float delay = 0f)
    {
        StartCoroutine(SetGameStateWithDelayCor(state, delay));
    }

    private IEnumerator SetGameStateWithDelayCor(GameState state, float delay)
    {
        Debug.Log("Degisiyor...");
        yield return new WaitForSeconds(delay);
        Debug.Log("Degisti!");
        currentState = state;
        OnGameStateChanged?.Invoke(state);
    }
}

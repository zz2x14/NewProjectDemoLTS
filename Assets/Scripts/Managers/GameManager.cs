using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingletonTool<GameManager>
{
    [SerializeField] private GameState gameState = GameState.Playing;
    [SerializeField] private PlayerBattleState playerBattleState = PlayerBattleState.Peaceful;
    
    public GameState GameState
    {
        get => gameState;
        set => gameState = value;
    }
    
    public PlayerBattleState BattleState
    {
        get => playerBattleState;
        set => playerBattleState = value;
    }

}

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public enum PlayerBattleState
{
    Peaceful,
    InBattle
}

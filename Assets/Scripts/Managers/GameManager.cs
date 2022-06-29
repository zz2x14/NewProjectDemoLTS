using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : PersistentSingletonTool<GameManager>
{
    [SerializeField] private GameState gameState = GameState.Playing;
    [SerializeField] private PlayerBattleState playerBattleState = PlayerBattleState.Peaceful;
    [SerializeField] private GameChapter gameChapter = GameChapter.ZeroChapter;
    
    public GameState _GameState
    {
        get => gameState;
        set => gameState = value;
    }
    public PlayerBattleState BattleState
    {
        get => playerBattleState;
        set => playerBattleState = value;
    }
    public GameChapter _GameChapter
    {
        get =>gameChapter;
        set => gameChapter = value;
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

public enum GameChapter
{
    ZeroChapter,
    FirstChapter,
}

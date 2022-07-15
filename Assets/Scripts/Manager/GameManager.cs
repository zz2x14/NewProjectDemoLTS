using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyEventSpace;

public class GameManager : PersistentSingletonTool<GameManager>
{
    [SerializeField] private GameState gameState = GameState.Playing;
    [SerializeField] private PlayerBattleState playerBattleState = PlayerBattleState.Peaceful;
    [SerializeField] private GameChapter gameChapter = GameChapter.ZeroChapter;
    
    [Header("战斗对象列表")] 
    [SerializeField] private List<EnemyController> battleTargetsList = new List<EnemyController>();

    public GameState _GameState
    {
        get => gameState;
        set => gameState = value;
    }
    public GameChapter _GameChapter
    {
        get =>gameChapter;
        set => gameChapter = value;
    }
    public PlayerBattleState _BattleState => playerBattleState;

    private void OnEnable()
    {
        StartCoroutine(nameof(BattleStateCor));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator BattleStateCor()
    {
        while (gameObject.activeSelf)
        {
            playerBattleState = battleTargetsList.Count == 0 ? PlayerBattleState.Peaceful : PlayerBattleState.InBattle;

            yield return null;
        }
    }

    public void AddIntoBattleList(EnemyController target)
    {
        if(!battleTargetsList.Contains(target))
            battleTargetsList.Add(target); 
    }

    public void DepartFromBattleList(EnemyController target)
    {
        battleTargetsList.Remove(target);
    }

    public void ClearBattleList()
    {
        battleTargetsList.Clear();
    }

    public void ShoppingStateOpreation(bool shopping)
    {
        gameState = shopping ? GameState.Shopping : GameState.Playing;
        Time.timeScale = shopping ? 0 : 1;
        
        if (shopping)
        {
            ComponentProvider.Instance.PlayerInputAvatar.DisableGamePlayInput();
            ComponentProvider.Instance.PlayerInputAvatar.DisablePlayerMenuInput();
        }
        else
        {
            ComponentProvider.Instance.PlayerInputAvatar.EnableGameplayInput();
            ComponentProvider.Instance.PlayerInputAvatar.EnablePlayerMenuInput();
        }
    }

}

public enum GameState
{
    Playing,
    Shopping,
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
    SecondChapter,
    ThirdChapter,
    FourthChapter,
    FifthChapter
}

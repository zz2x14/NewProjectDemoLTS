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

    public Vector3 CurBattleTargetPos => battleTargetsList[0].transform.position;
    public Vector3 CurBattleTargetPosWithOffset => new Vector3(battleTargetsList[0].transform.position.x,
        battleTargetsList[0].transform.position.y - battleTargetsList[0].OnGroundHeight, 0f);
    public Vector3 FindOneTargetPosWithOffset()
    {
        for (int i = 0; i < FindObjectsOfType<EnemyController>().Length; i++)
        {
            if (FindObjectsOfType<EnemyController>().Length > 0)
            {
                return  new Vector3(FindObjectsOfType<EnemyController>()[0].transform.position.x,
                    FindObjectsOfType<EnemyController>()[0].transform.position.y - FindObjectsOfType<EnemyController>()[0].OnGroundHeight, 0f);
            }
        }

        return ComponentProvider.Instance.PlayerPos.position;
    }
    public Vector3 FindOneTargetPos()
    {
        for (int i = 0; i < FindObjectsOfType<EnemyController>().Length; i++)
        {
            if (FindObjectsOfType<EnemyController>().Length > 0)
            {
                return  FindObjectsOfType<EnemyController>()[0].transform.position;
            }
        }

        return ComponentProvider.Instance.PlayerPos.position;
    }

    public EnemyController CurBattleTarget => battleTargetsList[0];

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

    public void PlayerMenuingStateOpreation(bool playerMeuning)
    {
        gameState = playerMeuning ? GameState.Paused : GameState.Playing;
        Time.timeScale = playerMeuning ? 0 : 1;
        
        if (playerMeuning)
        {
            ComponentProvider.Instance.PlayerInputAvatar.DisableGamePlayInput();
            ComponentProvider.Instance.PlayerInputAvatar.EnablePlayerMenuInput();
        }
        else
        {
            ComponentProvider.Instance.PlayerInputAvatar.EnableGameplayInput();
            ComponentProvider.Instance.PlayerInputAvatar.DisablePlayerMenuInputWithoutSwitchKey();
        }
    }

}

public enum GameState
{
    Playing,
    Shopping,
    PlayerMeuning,
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

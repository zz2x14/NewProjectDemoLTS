using System;
using System.Collections;
using System.Collections.Generic;
using MyEventSpace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuSystem : PersistentSingletonTool<PlayerMenuSystem>
{
    [SerializeField] private Canvas playerMenuCanvas;
    [SerializeField] private List<Canvas> playerMenuCanvasList = new List<Canvas>();
    [SerializeField] private List<Image> navigationImageList = new List<Image>();
    [SerializeField] private List<TextMeshProUGUI> navigationNameList = new List<TextMeshProUGUI>();

    private PlayerInput playerInput;

    private int canvasIndex;

    private bool isOpen;

    private Color elseWhiteColor;
    private Color elseBlackColor;

    protected override void Awake()
    {
        base.Awake();

        playerInput = FindObjectOfType<PlayerInput>();

        canvasIndex = 0;
        
        elseWhiteColor = new Color(1, 1, 1,0.5f);
        elseBlackColor = new Color(0, 0, 0, 0.5f);
    }
   
    private void Update()
    {
        SwitchPlayerMenu(null,EventArgs.Empty);
        SwitchPlayerMenuObject();
    }
    
    private void SwitchPlayerMenu(object sender,EventArgs e)
    {
        if(!ComponentProvider.Instance.PlayerInputAvatar.IsMenuSwitchKeyPressed || GameManager.Instance._GameChapter == GameChapter.ZeroChapter) return;

        isOpen = !isOpen;
        playerMenuCanvas.enabled = isOpen;
        EnableTargetCanvas(0);
        EnableTargetNavigation(0);

        if (isOpen)
        {
            Time.timeScale = 0f;
            ComponentProvider.Instance.PlayerInputAvatar.DisableGamePlayInput();
            ComponentProvider.Instance.PlayerInputAvatar.EnablePlayerMenuInput();
            GameManager.Instance._GameState = GameState.Paused;
            
            EventManager.Instance.EventHandlerTrigger(EventName.OnPlayerMenuOpen,e);
        }
        else
        {
            Time.timeScale = 1f;
            ComponentProvider.Instance.PlayerInputAvatar.DisablePlayerMenuInput();
            ComponentProvider.Instance.PlayerInputAvatar.EnableGameplayInput();
            GameManager.Instance._GameState = GameState.Playing;
        }
    }

    private void SwitchPlayerMenuObject()
    {
        if(!isOpen) return;
        
        if (playerInput.IsSwitchNextKeyPressed)
        {
            canvasIndex++;
            
            if (canvasIndex > playerMenuCanvasList.Count - 1)
                canvasIndex = 0;
            
            EnableTargetCanvas(canvasIndex);
            EnableTargetNavigation(canvasIndex);
        }
        else if (playerInput.IsSwitchLastKeyPressed)
        {
            canvasIndex--;
            if (canvasIndex < 0)
                canvasIndex = playerMenuCanvasList.Count - 1;
            
            EnableTargetCanvas(canvasIndex);
            EnableTargetNavigation(canvasIndex);
        }
    }

    private void EnableTargetNavigation(int index)
    {
        for (int i = 0; i < navigationImageList.Count; i++)
        {
            navigationImageList[i].color = i == index ? Color.white : elseWhiteColor;
        }
        
        for (int i = 0; i < navigationNameList.Count; i++)
        {
            navigationNameList[i].color = i == index ? Color.black : elseBlackColor;
        }
    }

    public void EnableTargetCanvas(int index)
    {
        for (int i = 0; i < playerMenuCanvasList.Count; i++)
        {
            playerMenuCanvasList[i].enabled = i == index;
        }
    }
    
}

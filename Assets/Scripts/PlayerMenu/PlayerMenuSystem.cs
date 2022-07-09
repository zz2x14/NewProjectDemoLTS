using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenuSystem : PersistentSingletonTool<PlayerMenuSystem>
{
    [SerializeField] private Canvas playerMenuCanvas;
    [SerializeField] private List<Canvas> playerMenuCanvasList = new List<Canvas>();
    
    private PlayerInput playerInput;

    private int canvasIndex;

    protected override void Awake()
    {
        base.Awake();

        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.IsSwitchNextKeyPressed)
        {
            canvasIndex++;
            if (canvasIndex > playerMenuCanvasList.Count - 1)
                canvasIndex = 0;
            
            EnableTargetCanvas(canvasIndex);
        }
        else if (playerInput.IsSwitchLastKeyPressed)
        {
            canvasIndex--;
            if (canvasIndex < 0)
                canvasIndex = playerMenuCanvasList.Count - 1;
            
            EnableTargetCanvas(canvasIndex);
        }
    }


    public void SwitchPlayerMenu(bool open)
    {
        playerMenuCanvas.enabled = open;
    }
    
    public void EnableTargetCanvas(int index)
    {
        for (int i = 0; i < playerMenuCanvasList.Count; i++)
        {
            if (i == index)
            {
                playerMenuCanvasList[i].enabled = true;
            }
            else
            {
                playerMenuCanvasList[i].enabled = false;
            }
        }
        
    }
    
}

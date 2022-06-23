using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideUIRespondInput : MonoBehaviour
{
    private PlayerInput playerInput;
    
    [SerializeField]private InputType respondInput;

   // private Dictionary<InputType, bool> inputRespondTable;
    
    private void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }


    private void OnEnable()
    {
        StartCoroutine(nameof(EnableWhenPlayerMoveCor));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // private void Start()
    // {
    //     InitializeInputTable();
    // }
    //
    // private void InitializeInputTable()
    // {
    //     inputRespondTable = new Dictionary<InputType, bool>();
    //     
    //     inputRespondTable.Add(respondInput,playerInput.IsRunning);
    // }

    IEnumerator EnableWhenPlayerMoveCor()
    {
        while (!playerInput.IsRunning)
        {
            gameObject.SetActive(true);

            yield return null;
        }
    }
}

public enum InputType
{
    Move,
    Jump
}

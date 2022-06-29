using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStateMachine : StateMachine
{
   [SerializeField] private List<NpcStateBase> npcStatesList = new List<NpcStateBase>();

   private NpcController npcController;

   private List<NpcStateBase> statesStock;

   protected override void Awake()
   {
      base.Awake();
      
      npcController = GetComponent<NpcController>();
   }

   private void Start()
   {
      CopyStates();
      
      InitializeNpcStates();
      
      SwitchOn(statesStock[0]);
   }

   private void InitializeNpcStates()
   {
      stateTable = new Dictionary<Type, IState>(statesStock.Count);

      for (int i = 0; i < statesStock.Count; i++)
      {
         statesStock[i].Initialize(npcController, this);
         stateTable.Add(statesStock[i].GetType(),statesStock[i]);
      }
   }

   // protected override void Update()
   // {
   //    base.Update();
   //    
   //    Debug.Log(curState);
   // }

   private void CopyStates()
   {
      statesStock = new List<NpcStateBase>(npcStatesList.Count);

      foreach (NpcStateBase state in npcStatesList)
      {
         NpcStateBase stateCopy = Instantiate(state);
         statesStock.Add(stateCopy);
      }
      
      npcStatesList.Clear();
   }
   
   
}

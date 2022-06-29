 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;
 using System;
 using TMPro;
 using UnityEngine.UI;


 public class SceneController : PersistentSingletonTool<SceneController>
 { 
     private float progressValue;
     
     [SerializeField] private Canvas bgCanvas;
     [SerializeField] private Canvas progressCanvas;
     [SerializeField] private Image progressBar;
     [SerializeField] private TextMeshProUGUI progressText;
     [SerializeField] private TextMeshProUGUI saveTipText;
     [SerializeField] private GameObject saveTipIcon;
     [SerializeField] private GameObject sceneFaderImage;

     private Coroutine teleportCor;

     private PlayerInput playerInput;
     private PlayerController playerController;

     private void Start()
     {
         playerInput = FindObjectOfType<PlayerInput>();
         playerController = playerInput.transform.GetComponent<PlayerController>();
     }

     public void Teleport(int sceneID)
     {
        teleportCor =  StartCoroutine(TeleportCor(sceneID));
     }
 
     IEnumerator TeleportCor(int sceneID)
     {
         playerInput.EnableSceneTeleportInput();
         
         playerController.SavePlayerData();
         
         progressValue = 0f;
         
         saveTipIcon.SetActive(false);
         saveTipText.gameObject.SetActive(false);
         saveTipText.text = "";
         progressText.text = "";
         progressBar.fillAmount = 0;
         
         bgCanvas.enabled = true;
         progressCanvas.enabled = true;
         
         AsyncOperation async = SceneManager.LoadSceneAsync(sceneID);
 
         async.allowSceneActivation = false;
         
 
         while (!async.isDone)
         {
             if (async.progress < 0.9f)
             {
                 progressValue = async.progress;
                 progressBar.fillAmount = progressValue;
                 progressText.text = progressValue.ToString("P0");
             }
             else
             {
                 progressValue = 1f;
                 progressBar.fillAmount = progressValue;
                 progressText.text = progressValue.ToString("P0");
             }
             
             if ( async.progress >= 0.9f)
             {
                 progressText.text = "按任意键继续...";
                 saveTipText.text = "/自动保存成功/";
                 saveTipIcon.SetActive(true);
                 saveTipText.gameObject.SetActive(true);
               
                 if (playerInput.IsSceneTeleportConfirmKeyPressed)
                 {
                     SetPlayerPos();
                     
                     playerController.LoadPlayerData();
                     
                     bgCanvas.enabled = false;
                     progressCanvas.enabled = false;
                    
                     sceneFaderImage.SetActive(true);
                         
                     async.allowSceneActivation = true;
             
                     saveTipIcon.SetActive(false);
                     
                     playerInput.DisableSceneTeleportInput();
                     playerInput.EnableGameplayInput();
                 }
             }

             if (async.allowSceneActivation && teleportCor != null)
             {
                 StopCoroutine(teleportCor);
             }
             
             yield return null;
         }

     }

     public void SetPlayerPos()
     {
         playerInput.transform.position = Vector3.zero;
     }
     


 }

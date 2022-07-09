 using System.Collections;
 using UnityEngine;
 using UnityEngine.SceneManagement;
 using TMPro;
 using UnityEngine.Events;
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

     // private PlayerInput playerInput;
     // private PlayerController playerController;

     private const string TEXT_GETANEKEYTIP = "按任意键继续...";
     private const string TEXT_LOADOVERTIP = "/自动保存成功/";

     public void Teleport(int sceneID)
     {
        teleportCor = StartCoroutine(TeleportCor(sceneID));
     }
 
     IEnumerator TeleportCor(int sceneID)
     {
         ComponentProvider.Instance.PlayerInputAvatar.EnableSceneTeleportInput();
         
         ComponentProvider.Instance.PlayerAvatar.SavePlayerData();
         ComponentProvider.Instance.PlayerAvatar.DisableHealthBar();
         
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
                 progressText.text = TEXT_GETANEKEYTIP;
                 saveTipText.text = TEXT_LOADOVERTIP ;
                 saveTipIcon.SetActive(true);
                 saveTipText.gameObject.SetActive(true);
               
                 if (ComponentProvider.Instance.PlayerInputAvatar.IsSceneTeleportConfirmKeyPressed)
                 {
                     SetPlayerPosToZero();
                     
                     ComponentProvider.Instance.PlayerAvatar.LoadPlayerData();
                     ComponentProvider.Instance.PlayerAvatar.EnableHealthBar();
                     
                     bgCanvas.enabled = false;
                     progressCanvas.enabled = false;
                    
                     sceneFaderImage.SetActive(true);
                         
                     async.allowSceneActivation = true;
             
                     saveTipIcon.SetActive(false);
                     
                     ComponentProvider.Instance.PlayerInputAvatar.DisableSceneTeleportInput();
                     ComponentProvider.Instance.PlayerInputAvatar.EnableGameplayInput();
                 }
             }

             if (async.allowSceneActivation && teleportCor != null)
             {
                 StopCoroutine(teleportCor);
             }
             
             yield return null;
         }

     }

     public void SetPlayerPosToZero()
     {
         ComponentProvider.Instance.PlayerPos.position = Vector3.zero;
     }
 }

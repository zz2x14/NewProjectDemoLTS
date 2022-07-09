using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterGuideContentUI : SingletonTool<ChapterGuideContentUI>
{
    [SerializeField] private TextMeshProUGUI contentText;

    public void UpdateChapterGuideContent(string content)
    {
        contentText.text = content;
    }

}

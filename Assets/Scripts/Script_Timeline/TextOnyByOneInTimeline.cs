using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class TextOnyByOneInTimeline : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float wordSpeed;
    [TextArea] [SerializeField] private string textContent;

    private WaitForSeconds wordSpeedWFS;
    private StringBuilder contentSB = new StringBuilder();

    private StringBuilder curContentSB = new StringBuilder();
        
    private int wordsNum;

    private void Awake()
    {
        wordSpeedWFS = new WaitForSeconds(wordSpeed);
    }

    public void StartTextCor()
    {
        StartCoroutine(nameof(TextOneByOneCor));
    }

    IEnumerator TextOneByOneCor()
    {
        curContentSB.Clear();
        contentSB.Append(textContent);
        wordsNum = 0;
        
        while (wordsNum < textContent.Length)
        {
            yield return wordSpeedWFS;

            curContentSB.Append(contentSB[wordsNum]);
            text.text = curContentSB.ToString();
            wordsNum++;
        }
        
        StopAllCoroutines();
    }

}

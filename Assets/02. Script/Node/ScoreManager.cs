using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [Header("# Score")]
    [SerializeField] Text txtScore;

    [Header("# Goblin Fire")]
    [SerializeField] List<GameObject> goblinFire;
    [SerializeField] int goblinFireCriteria;
    [SerializeField] Text goblinTxt;

    ComboManager theCombo;
    int currentScore = 0;
    int currentGoblinFireCount = 0;
    int goblinFireCount = 0;

    private void Awake()
    {
        for(int i=0; i<goblinFire.Count; i++)
            if(goblinFire[i].activeSelf)
                goblinFire[i].SetActive(false);
    }

    private void Start()
    {
        theCombo = FindFirstObjectByType<ComboManager>();
        currentScore = 0;
        goblinFireCount = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int score)
    {
        // 콤보 증가
        theCombo.IncreaseCombo();

        // 점수 반영
        currentScore += score;
        currentGoblinFireCount += score;
        Debug.Log(currentScore);
        CheckGoblinCounter();
        ShowGoblinFire();

        txtScore.text = string.Format("{0:#,##0}", currentScore);
    }

    void CheckGoblinCounter()
    {
        if(currentGoblinFireCount >= goblinFireCriteria)
        {
            currentGoblinFireCount -= goblinFireCriteria;
            if(goblinFireCount >= goblinFire.Count)
                goblinFireCount = goblinFire.Count;
            else
                goblinFireCount++;
        }
        goblinTxt.text = goblinFireCount.ToString();
    }

    void ShowGoblinFire()
    {
        for (int i = 0; i < goblinFire.Count; i++)
        {
            if(i < goblinFireCount)
            {
                goblinFire[i].SetActive(true);
            }
            else
            {
                goblinFire[i].SetActive(false);
            }
        }
    }
}

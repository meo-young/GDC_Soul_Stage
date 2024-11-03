using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] List<GameObject> stageList;
    [SerializeField] List<GameObject> focusCircleList;

    private int currentStage;
    private int reserveCount;
    private int level;

    StageDescription stageDes;

    private void Awake()
    {
        stageDes = FindFirstObjectByType<StageDescription>();

        currentStage = 0;
        level = GameManager.instance.level;
    }
    private void Start()
    {
        ShowAvailableStage();
        DeactiveAllFocusCircle();
        CheckCurrentFocusStage();
    }

    private void Update()
    {
        GetInput();
        CheckCurrentFocusStage();
        stageDes.ShowTitleNode(currentStage);
        stageDes.ShowCurrentGhost(currentStage);
        stageDes.ShowCurrentNode(currentStage);
        NextStage();
    }

    void NextStage()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (currentStage)
            {

            }
        }
    }
    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageFocusMove);
            reserveCount = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageFocusMove);
            reserveCount = 1;
        }


        if (reserveCount > level)
            return;

        currentStage = reserveCount;
    }
    void CheckCurrentFocusStage()
    {
        DeactiveAllFocusCircle();
        if (currentStage > level)
            return;

        focusCircleList[currentStage].SetActive(true);
    }

    void DeactiveAllFocusCircle()
    {
        for(int i=0; i<focusCircleList.Count; i++)
        {
            focusCircleList[i].gameObject.SetActive(false);
        }
    }

    void ShowAvailableStage()
    {
        for (int i = 0; i < stageList.Count; i++)
        {
            if (i <= GameManager.instance.level)
            {
                stageList[i].SetActive(true);
            }
        }
    }
}

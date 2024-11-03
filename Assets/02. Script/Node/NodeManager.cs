using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    double currentTime = 0d; // 리듬 게임은 오차 적은게 중요해서 float보단 double
    [SerializeField]  List<double> noteTimings; // 노드 생성 타이밍 리스트
    [SerializeField] Transform tfNoteAppear = null; // 노트 생성 위치 오브젝트

    private int noteIndex; // 현재 체크 중인 노드의 인덱스
    

    TimingManager theTimingManager;
    StageManager theStageManager;

    void Start()
    {
        theTimingManager = FindFirstObjectByType<TimingManager>();
        theStageManager = FindFirstObjectByType<StageManager>();
    }

    void Update()
    {
        if (!theStageManager.startFlag)
            return;
        

        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
            Debug.Log((currentTime-4.1).ToString("F2"));

        // 현재 시간과 타이밍 리스트의 값이 같거나 더 크면 노드를 생성
        if (noteIndex < noteTimings.Count && currentTime >= noteTimings[noteIndex])
        {
            SpawnNote();
            noteIndex++;
        }
    }


    private void SpawnNote()
    {
        GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
        t_note.transform.position = tfNoteAppear.position;
        t_note.SetActive(true);
        t_note.transform.localScale = new Vector3(1f, 1f, 0f);

        theTimingManager.boxNoteList.Add(t_note);
    }
}

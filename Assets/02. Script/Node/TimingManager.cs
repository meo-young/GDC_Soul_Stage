using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();
    ComboManager comboManager;
    ScoreManager scoreManager;

    [SerializeField] int perfectScore;
    [SerializeField] int goodScore;

    [SerializeField] GameObject perfectVFX;
    [SerializeField] GameObject goodVFX;
    [SerializeField] Transform timingPos;

    [HideInInspector] public int multiplier;

    [SerializeField] Text perfectTxt;
    [SerializeField] Text goodTxt;
    [SerializeField] Text badTxt;

    [SerializeField] Text showPerfect;
    [SerializeField] Text showGood;
    [SerializeField] Text showBad;
    [SerializeField] float fadeDuration = 2.0f; // 페이드가 완료되는 시간

    [SerializeField] GameObject perfectEffect;
    [SerializeField] GameObject goodEffect;
    [SerializeField] GameObject badEffect;
    [SerializeField] Transform effectPos;


    private int perfectCounter;
    private int goodCounter;
    private int badCounter;

    private Coroutine currentCoroutine;


    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        comboManager = FindFirstObjectByType<ComboManager>();
        perfectCounter = 0;
        goodCounter  = 0;
        badCounter = 0;

        DeactiveText();
        multiplier = 1;
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            switch (boxNoteList[i].GetComponent<Node>().type)
            {
                case ScoreType.Perfect:
                    DeactiveText();
                    SetActiveTrueText(showPerfect);
                    Instantiate(perfectEffect, effectPos.position, Quaternion.identity);
                    perfectCounter++;
                    perfectTxt.text = perfectCounter.ToString();
                    Instantiate(perfectVFX, timingPos.position, Quaternion.identity);
                    scoreManager.IncreaseScore(perfectScore * multiplier);
                    CheckNodeType(boxNoteList[i]);
                    boxNoteList[i].GetComponent<Node>().HideNote();
                    boxNoteList.RemoveAt(i);
                    break;
                case ScoreType.Good:
                    DeactiveText();
                    SetActiveTrueText(showGood);
                    Instantiate(goodEffect, effectPos.position, Quaternion.identity);
                    goodCounter++;
                    goodTxt.text = goodCounter.ToString();
                    Instantiate(goodVFX, timingPos.position, Quaternion.identity);
                    scoreManager.IncreaseScore(goodScore * multiplier);
                    CheckNodeType(boxNoteList[i]);
                    boxNoteList[i].GetComponent<Node>().HideNote();
                    boxNoteList.RemoveAt(i);
                    break;
                case ScoreType.Bad:
                    DeactiveText();
                    SetActiveTrueText(showBad);
                    Instantiate(badEffect, effectPos.position, Quaternion.identity);
                    badCounter++;
                    badTxt.text = badCounter.ToString();
                    comboManager.ResetCombo();
                    boxNoteList[i].GetComponent<Node>().HideNote();
                    boxNoteList.RemoveAt(i);
                    break;
            }
        }
    }

    void CheckNodeType(GameObject node)
    {
        switch(node.tag)
        {
            case "Piano":
                node.GetComponent<IInteractable>().Interact();
                break;
            case "Drum":
                node.GetComponent<IInteractable>().Interact();
                break;
            case "Guitar":
                node.GetComponent<IInteractable>().Interact();
                break;
            case "Bass":
                node.GetComponent<IInteractable>().Interact();
                break;
        }
    }

    void DeactiveText()
    {
        SetActiveFalseText(showBad);
        SetActiveFalseText(showPerfect);
        SetActiveFalseText(showGood);
    }

    void SetActiveFalseText(Text text)
    {
        Color color = text.color;
        color.a = 0;
        text.color = color;
    }

    void SetActiveTrueText(Text text)
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine); // 이전 코루틴 중단
        }
        currentCoroutine = StartCoroutine(SetActiveFalseTextCoroutine(text));
    }

    IEnumerator SetActiveFalseTextCoroutine(Text text)
    {
        yield return new WaitForSeconds(2.0f);

        Color color = text.color;
        float startAlpha = color.a; // 초기 알파 값
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 완전히 투명하게 만듭니다.
        text.color = new Color(color.r, color.g, color.b, 0);
        text.gameObject.SetActive(false);
    }
}

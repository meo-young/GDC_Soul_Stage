using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [HideInInspector] public bool startFlag;
    [SerializeField] Text counterText;
    [SerializeField] float stageError;

    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject spaceBarObject;
    private float stageCounter;

    private float startTimeCounter;

    private void Awake()
    {
        stageCounter = 0;
        Time.timeScale = 0f;
        startFlag = false;
        startTimeCounter = 0;
        if(counterText.gameObject.activeSelf)
            counterText.gameObject.SetActive(false);
    }

    private void Update()
    {
        stageCounter += Time.deltaTime;
        if(stageCounter > 180)
        {
            startFlag = false;
            FinishCurrentStage();
        }
        if (startFlag)
            return;

        if (startTimeCounter < 0)
        {
            StartCoroutine(SoundManager.Instance.PlayStageBGM(SoundManager.BGM.BGM_Stage1, stageError));
            Time.timeScale = 1f;
            startFlag = true;
            counterText.gameObject.SetActive(false);
        }

        if (startTimeCounter > 0)
        {
            counterText.text = Mathf.Ceil(startTimeCounter).ToString();
            startTimeCounter -= Time.unscaledDeltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTimeCounter = 3;
            spaceBarObject.SetActive(false);
            counterText.gameObject.SetActive(true);
        }
    }

    void FinishCurrentStage()
    {
        resultPanel.transform.localScale = Vector3.one;
        Invoke(nameof(MoveToNextStage), 3.0f);
    }

    void MoveToNextStage()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Final")
        {
            SceneManager.LoadScene("Title");
            AudioListener.pause = true;
        }
        else
        {
            Application.Quit();
        }
    }

}

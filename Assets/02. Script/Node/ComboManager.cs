using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    [SerializeField] TMP_Text txtCombo;
    [SerializeField] float fadeDuration = 2.0f; // 페이드가 완료되는 시간
    [SerializeField] int clapComboCounter;
    [SerializeField] int shoutComboCounter;
    [SerializeField] int announcerComboCounter;
    [HideInInspector] public int comboMultiplier;

    int currentCombo = 0;
    private Coroutine currentCoroutine;

    private void Start()
    {
        comboMultiplier = 1;
        txtCombo.gameObject.SetActive(false);
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num * comboMultiplier;
        txtCombo.text = string.Format("{0:#,##0}" + " Combo", currentCombo);

        CheckComboEffect();

        if (currentCombo >= 2)  // 2콤보 이상
        {
            SetActiveText();
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine); // 이전 코루틴 중단
            }
            currentCoroutine = StartCoroutine(SetActiveFalseText());
        }
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txtCombo.text = "0";
        if(txtCombo.gameObject.activeSelf)
            txtCombo.gameObject.SetActive(false);
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    void CheckComboEffect()
    {
        if (currentCombo == clapComboCounter)
        {
            SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_Clap);
        }
        else if (currentCombo == shoutComboCounter)
        {
            SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_LoudNoise);
        }
        else if (currentCombo == announcerComboCounter)
        {
            Debug.Log("Announcer Combo");
        }
    }

    void SetActiveText()
    {
        txtCombo.gameObject.SetActive(true);
        Color color = txtCombo.color;
        color.a = 1;
        txtCombo.color = color;
    }

    IEnumerator SetActiveFalseText()
    {
        yield return new WaitForSeconds(2.0f);

        Color color = txtCombo.color;
        float startAlpha = color.a; // 초기 알파 값
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {

            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            txtCombo.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        // 완전히 투명하게 만듭니다.
        txtCombo.color = new Color(color.r, color.g, color.b, 0);
        txtCombo.gameObject.SetActive(false);
    }
}

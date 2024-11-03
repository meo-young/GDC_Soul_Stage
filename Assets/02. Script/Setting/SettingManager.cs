using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Tooltip("�ɼ� â")]
    [SerializeField] GameObject optionPanel;
    [Tooltip("�Ҹ� ������ ���� Audio Mixer")]
    [SerializeField] AudioMixer audioMixer;
    [Tooltip("��ü �Ҹ� ������ ���� Slider")]
    [SerializeField] Slider m_MusicMasterSlider;
    [Tooltip("��ü �Ҹ� ������ ���� Slider")]
    [SerializeField] Slider m_MusicSFXSlider;
    [Tooltip("��ü �Ҹ� ������ ���� Slider")]
    [SerializeField] Slider m_MusicBGMSlider;

    private float currentMasterVolume;
    private float currentSFXVolume;
    private float currentBGMVolume;

    private float beforeTimeScale;

    private StageManager stageManager;

    private void Start()
    {
        if(optionPanel.activeSelf)
            optionPanel.SetActive(false);

        stageManager = FindFirstObjectByType<StageManager>();
    }
    private void Update()
    {
        if (stageManager != null)
            if (!stageManager.startFlag)
                return;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionPanel.activeSelf)
                ResumeGame();
            else
                PauseGame();
        }
    }


    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }

    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void ResumeGame()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageSelect);
        Time.timeScale = beforeTimeScale;
        AudioListener.pause = false; 

        optionPanel.SetActive(false);
    }

    public void PauseGame()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageSelect);
        beforeTimeScale = Time.timeScale;
        //GetCurrentSetting();

        Time.timeScale = 0f;
        AudioListener.pause = true;

        optionPanel.SetActive(true);
    }

    public void GetCurrentSetting()
    {
        audioMixer.GetFloat("Master", out currentMasterVolume);
        audioMixer.GetFloat("SFX", out currentSFXVolume);
        audioMixer.GetFloat("BGM", out currentBGMVolume);

        SetMasterVolume(currentBGMVolume);
        SetSFXVolume(currentSFXVolume);
        SetBGMVolume(currentBGMVolume);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        // �����Ϳ��� ���� ���� ���� ���� ���� ��� �����͸� ����
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ӿ����� Application.Quit() ȣ��
        Application.Quit();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }
}

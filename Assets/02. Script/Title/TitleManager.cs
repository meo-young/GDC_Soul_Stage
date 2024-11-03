using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] string startScene;
    

    private void Start()
    {
        Time.timeScale = 1.0f;
        SoundManager.Instance.PlayBgm(SoundManager.BGM.BGM_Title);
    }

    private void Update()
    {
        if(!SoundManager.Instance.bgmPlayer.isPlaying)
        {
            AudioListener.pause = false;
            SoundManager.Instance.PlayBgm(SoundManager.BGM.BGM_Title);

        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SoundManager.Instance.PlayBgm(SoundManager.BGM.BGM_Title);
    }
    public void StartGame()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageSelect);
        SceneManager.LoadScene(startScene);
    }


    public void ExitGame()
    {
        SoundManager.Instance.PlaySfx(SoundManager.SFX.SFX_StageSelect);
        Application.Quit();
    }
}

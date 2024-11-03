using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        Init();
    }


    public enum AudioType { BGM, SFX }

    [Header("#BGM")]
    public AudioClip[] bgmClips;                     
    public AudioSource bgmPlayer;                              // BGM 플레이어는 단일
                                                               // 배경음악(BGM) 클립
    public enum BGM
    {
        BGM_Title = 0,
        BGM_Stage1 = 1,
        BGM_Stage2 = 2,
        BGM_Stage3 = 3,
        BGM_Stage4 = 1,
    }

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public int channels;                                // SFX 사운드 채널
    public AudioSource[] sfxPlayers;                           // SFX는 동시에 여러개가 실행됨
    int channelIndex;

    public enum SFX
    {
        // UI
        SFX_StageSelect = 0,
        SFX_Clap = 1,
        SFX_LoudNoise = 2

    }

    private void Init()
    {

        bgmPlayer.playOnAwake = false;                         

        // 용량 최적화
        bgmPlayer.dopplerLevel = 0.0f;
        bgmPlayer.reverbZoneMix = 0.0f;



        for (int idx = 0; idx < sfxPlayers.Length; idx++)
        {
            sfxPlayers[idx].playOnAwake = false;
            sfxPlayers[idx].dopplerLevel = 0.0f;
            sfxPlayers[idx].reverbZoneMix = 0.0f;
        }
    }

    // BGM 사용을 위한 함수
    public void PlayBgm(BGM bgm)
    {
        if (bgmPlayer == null) return;
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
    }

    public IEnumerator PlayStageBGM(BGM bgm, float delay)
    {
        yield return new WaitForSeconds(delay);

        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.Play();
    }

    public void StopBgm()
    {
        if (bgmPlayer != null) bgmPlayer.Stop();
    }

    // 효과음 사용을 위한 함수
    public void PlaySfx(SFX sfx)
    {
        // 쉬고 있는 하나의 sfxPlayer에게 clip을 할당하고 실행
        for (int idx = 0; idx < sfxPlayers.Length; idx++)
        {
            int loopIndex = (idx + channelIndex) % sfxPlayers.Length;    // 채널 개수만큼 순회하도록 채널인덱스 변수 활용

            if (sfxPlayers[loopIndex].isPlaying) continue;               // 진행 중인 sfxPlayer는 쭉 진행

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}

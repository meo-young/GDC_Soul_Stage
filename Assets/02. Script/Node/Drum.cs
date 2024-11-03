using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Audio;

public class Drum : MonoBehaviour, IInteractable
{
    StageManager stageManager;
    [SerializeField] int effectTime;
    [SerializeField] float fastMultifiler;
    [SerializeField] Animator anim;

    ComboManager comboManager;

    private void Awake()
    {
        stageManager = FindFirstObjectByType<StageManager>();
        comboManager = FindFirstObjectByType<ComboManager>();
        anim = GameObject.FindWithTag("Drum Ghost").GetComponent<Animator>();
    }
    public void Interact()
    {
        Time.timeScale = fastMultifiler;
        comboManager.comboMultiplier = 3;
        anim.SetBool("Run", true);
        Invoke(nameof(SetOriginTimeScale), effectTime);
    }

    void SetOriginTimeScale()
    {
        if (!stageManager.startFlag)
            return;

        anim.SetBool("Run", false);
        comboManager.comboMultiplier = 1;
        Time.timeScale = 1f;
    }
}

using UnityEngine;

public class Piano : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject lightEffect;
    [SerializeField] int effectTime;
    [SerializeField] Animator anim;

    TimingManager timingManager;

    private void Awake()
    {
        lightEffect = GameObject.FindWithTag("Light");
        timingManager = FindFirstObjectByType<TimingManager>();
        anim  = GameObject.FindWithTag("Piano Ghost").GetComponent<Animator>();
    }
    public void Interact()
    {
        RecoverTransparency();
        anim.SetBool("Run", true);
        Invoke(nameof(SetActiveFalseLight), effectTime);
    }

    void SetActiveFalseLight()
    {
        if (lightEffect != null)
            SetTransparent();
    }

    void RecoverTransparency()
    {
        anim.SetBool("Run", false);

        Color color = lightEffect.GetComponent<SpriteRenderer>().color;
        color.a = 1f;
        lightEffect.GetComponent<SpriteRenderer>().color = color;

        timingManager.multiplier = 2;
    }

    void SetTransparent()
    {
        Color color = lightEffect.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        lightEffect.GetComponent<SpriteRenderer>().color = color;

        timingManager.multiplier = 1;
    }
}

using UnityEngine;

public class Bass : MonoBehaviour, IInteractable
{
    [SerializeField] float effectTime;
    [SerializeField] Animator anim;

    private void Start()
    {
        anim = GameObject.FindWithTag("Bass Ghost").GetComponent<Animator>();
    }
    public void Interact()
    {
        anim.SetBool("Run", true);
        Invoke(nameof(ShowMusicEffect), effectTime);
    }

    void ShowMusicEffect()
    {
        anim.SetBool("Run", false);
    }
}

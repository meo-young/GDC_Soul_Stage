using UnityEngine;

public class Guitar : MonoBehaviour, IInteractable
{
    [SerializeField] float effectTime;
    [SerializeField] GameObject fireEffect;
    private Transform fireSpawnPos;
    private Animator anim;

    private void Start()
    {
        anim = GameObject.FindWithTag("Guitar Ghost").GetComponent<Animator>();
        fireSpawnPos = GameObject.FindWithTag("Perfect").GetComponent<Transform>();
    }
    public void Interact()
    {
        anim.SetBool("Run", true);
        GameObject fireVFX = Instantiate(fireEffect, fireSpawnPos.position, Quaternion.identity);
        Destroy(fireVFX, effectTime);
        Invoke(nameof(ShowMusicEffect), effectTime);
    }
    void ShowMusicEffect()
    {
        anim.SetBool("Run", false);
    }
}

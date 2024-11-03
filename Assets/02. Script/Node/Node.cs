using UnityEngine;
using UnityEngine.UI;

public enum ScoreType
{
    Perfect,
    Good,
    Bad,
    None
}
public class Node : MonoBehaviour
{
    [SerializeField] float speed = 1f; // 회전 속도
    [SerializeField] float angle = 242; // 현재 각도
    [SerializeField] float radius;

    [HideInInspector] public ScoreType type;

    private TimingManager timingManager;
    private SpriteRenderer noteImage;
    private ComboManager comboManager;
    void Awake()
    {
        timingManager = FindFirstObjectByType<TimingManager>();
        noteImage = GetComponent<SpriteRenderer>();
        comboManager = FindFirstObjectByType<ComboManager>();
    }

    private void OnEnable()
    {
        noteImage.enabled = true;
        angle = 242;
        type = ScoreType.None;
    }

    private void Update()
    {
        angle += speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        UpdateNodePosition();
    }


    #region function

    public void HideNote()
    {
        noteImage.enabled = false;
    }
    void UpdateNodePosition()
    {
        Vector3 center = Vector3.zero; // 중심 위치
        gameObject.transform.position = center + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
    }

    #endregion

    #region Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Perfect"))
        {
            type = ScoreType.Perfect;
        }
        if (collision.CompareTag("Good"))
        {
            type = ScoreType.Good;
        }
        if(collision.CompareTag("Bad"))
        {
            type = ScoreType.Bad;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Perfect"))
        {
            type = ScoreType.Good;
        }
        if (collision.CompareTag("Good"))
        {
            type = ScoreType.Bad;
        }
        if(collision.CompareTag("Bad"))
        {
            if(noteImage.enabled)
                comboManager.ResetCombo();

            timingManager.boxNoteList.Remove(gameObject);
            ObjectPool.instance.noteQueue.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }
    #endregion
}

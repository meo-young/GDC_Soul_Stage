using System.Collections.Generic;
using UnityEngine;

public class StageDescription : MonoBehaviour
{
    [SerializeField] List<GameObject> nodeList;
    [SerializeField] List<GameObject> ghostList;
    [SerializeField] List<GameObject> lockList;
    [SerializeField] List<GameObject> titleNodeList;

    private void Awake()
    {
        DeactiveList(lockList);
        DeactiveList(titleNodeList);
    }

    public void ShowTitleNode(int index)
    {
        for (int i = 0; i < titleNodeList.Count; i++)
        {
            if (i < index)
            {
                titleNodeList[i].SetActive(true);
                lockList[i].SetActive(false);
            }
            else
            {
                lockList[i].SetActive(true);
                titleNodeList[i].SetActive(false);
            }
        }
    }


    public void ShowCurrentNode(int index)
    {
        DeactiveList(nodeList);

        nodeList[index].SetActive(true);
    }

    public void ShowCurrentGhost(int index)
    {
        DeactiveList(ghostList);

        ghostList[index].SetActive(true);
    }

    void DeactiveList(List<GameObject> list)
    {
        for(int i =0; i<list.Count; i++)
            list[i].SetActive(false);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int size;
    
    private Queue<GameObject> queue;

    private Transform parentTransform;

    public GameObject Prefab => prefab;
    public Transform ParentTransform
    {
        set => parentTransform = value;
    }

    public int Size => size;
    public int RuntimeSize => queue.Count;

    public void Init()
    {
        queue = new Queue<GameObject>();
        
        for (int i = 0; i < queue.Count; i++)
        {
            queue.Enqueue(Copy());
        }
    }

    private GameObject Copy()
    {
        GameObject copy = GameObject.Instantiate(prefab, parentTransform);
        copy.SetActive(false);
        return copy;
    }

    private GameObject GetAvailableGo()
    {
        GameObject availableGo;

        if (queue.Count > 0 && !queue.Peek().activeSelf)
        {
            availableGo = queue.Dequeue();
        }
        else
        {
            availableGo = Copy();
        }
        
        queue.Enqueue(availableGo);

        return availableGo;
    }

    public GameObject GetPreparedGo()
    {
        GameObject preparedGo = GetAvailableGo();
        preparedGo.SetActive(true);
        return preparedGo;
    }
    public GameObject GetPreparedGo(Vector3 pos)
    {
        GameObject preparedGo = GetAvailableGo();
        preparedGo.transform.position = pos;
        preparedGo.SetActive(true);
        return preparedGo;
    }
    public GameObject GetPreparedGo(Vector3 pos,Quaternion rotation)
    {
        GameObject preparedGo = GetAvailableGo();
        preparedGo.transform.position = pos;
        preparedGo.transform.rotation = rotation;
        preparedGo.SetActive(true);
        return preparedGo;
    }
    public GameObject GetPreparedGo(Vector3 pos,Quaternion rotation,Vector3 scale)
    {
        GameObject preparedGo = GetAvailableGo();
        preparedGo.transform.position = pos;
        preparedGo.transform.rotation = rotation;
        preparedGo.transform.localScale = scale;
        preparedGo.SetActive(true);
        return preparedGo;
    }
}

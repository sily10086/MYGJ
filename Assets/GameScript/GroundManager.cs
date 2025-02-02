using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    private static GroundManager _instance;
    public static GroundManager Instance => _instance;

    private Dictionary<int, List<GameObject>> grounds = new();
    
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject startGround;
    [SerializeField] private int maxGroundLayer;
    [SerializeField] private int currentGroundLayer = 0;
    [SerializeField] private List<Vector3> rayDirection;
    
    private void Awake()
    {
        _instance = this;
        grounds.Add(currentGroundLayer, new List<GameObject>{startGround});
        InitGrounds();
    }

    private async void Start()
    {
        await WhileCreateGround();
    }

    private void InitGrounds()
    {
        for (int i = 1; i <= maxGroundLayer; i++)
        {
            grounds.Add(i, new List<GameObject>());
        }
    }

    private async UniTask WhileCreateGround()
    {
        for (int i = 0; i < maxGroundLayer; i++)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            await CreateGround(i);
            //CreateGround(i);
        }
    }
    
    private async UniTask CreateGround(int layer)
    {
        var currentList = grounds[layer];
        var nextList = grounds[layer + 1];
        List<GameObject> tempNextList = new List<GameObject>(); // ��ʱ����

        foreach (var lastObj in currentList)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.02f));
            //await UniTask.Yield();
            foreach (var target in rayDirection)
            {
                var startPos = lastObj.transform.position;
                var direction = new Vector3(startPos.x + target.x, 
                    startPos.y + target.y, startPos.z + target.z);
                Ray ray = new Ray(startPos, target * 10f);

                if (!Physics.Raycast(ray, out RaycastHit hit, 100f))
                {
                    //Debug.Log("����·����û������");
                    // ��������
                    var obj = Instantiate(groundPrefab, transform, true);
                    obj.transform.position = direction;
                    obj.name = $"Ground.Layer{layer + 1}";
                    tempNextList.Add(obj); // ��ӵ���ʱ����
                }
            }
        }

        // ��ѭ����������ʱ���ϵ�������ӵ� nextList
        foreach (var obj in tempNextList)
        {
            nextList.Add(obj);
        }
    }
}

using System.Collections.Generic;
using GameScript.Card;
using GameScript.Ground;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameScript.PlayerInput
{
    public class PlayerController:MonoBehaviour
    {
        private GraphicRaycaster graphicRaycaster;
        private ICard _currentCard;
        
        private void Awake()
        {
            CameraManager.CameraManager.SetMainCamera(Camera.main);
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentCard != null)
                {
                    _currentCard.IOnPlaceCard(Input.mousePosition);
                    _currentCard = null;
                    return;
                }
                Ray ray = CameraManager.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
                bool hitIsGround = false;
                bool hitIsCard = false;

                // 1. 检测是否点击到 UI
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    hitIsCard = CheckIfClickedOnCard();
                }
                else
                {
                    // 2. 如果没有点击到 UI，检测 3D 物体（如 Ground）
                    if (CheckGround(ray))
                    {
                        hitIsGround = true;
                    }
                }

                // 3. 如果没有命中 Ground 或 UI，清空 CurrentGround
                if (!hitIsGround && !hitIsCard)
                {
                    GroundManager.Instance.SetCurrentGround(null);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (_currentCard != null)
                {
                    _currentCard.IOffClickCard();
                    _currentCard = null;
                }
            }
            
            if (_currentCard!=null)
            {
                _currentCard.IGetGameObject().transform.position = Input.mousePosition;
            }
        }

        private bool CheckIfClickedOnCard()
        {
            if (!graphicRaycaster)
            {
                // 获取 Canvas 上的 GraphicRaycaster 组件
                graphicRaycaster 
                    = GameObject.Find("UI(Clone)").GetComponent<GraphicRaycaster>();
            }
            
            if (!graphicRaycaster)
            {
                Debug.LogError("GraphicRaycaster component not found on EventSystem!");
                return false;
            }

            // 创建 PointerEventData
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            // 存储射线检测结果
            List<RaycastResult> uiHits = new List<RaycastResult>();

            // 执行射线检测
            graphicRaycaster.Raycast(pointerData, uiHits);

            // 遍历检测结果
            foreach (RaycastResult result in uiHits)
            {
                if (result.gameObject.CompareTag("Card"))
                {
                    Debug.Log("Clicked on a UI element with the 'Card' tag!");
                    _currentCard = result.gameObject.GetComponent<ICard>();
                    _currentCard.IOnClickCard();
                    // 在这里添加点击到 "Card" 标签 UI 的逻辑
                    return true;
                }
                if (result.gameObject.CompareTag("UI"))
                {
                    Debug.Log("Clicked on a UI element with the 'UI' tag!");
                    // 在这里添加点击到 "Card" 标签 UI 的逻辑
                    return true;
                }
            }
            return false;
        }
        private bool CheckGround(Ray ray)
        {
            RaycastHit[] hits = new RaycastHit[100]; // 定义一个足够大的数组来存储命中结果
            int hitCount = Physics.RaycastNonAlloc(ray, hits, 100f); // 获取命中结果的数量

            for (int i = 0; i < hitCount; i++) // 遍历命中结果
            {
                if (hits[i].collider.CompareTag("Ground")) // 检查是否带有 "Ground" 标签
                {
                    GroundManager.Instance.SetCurrentGround(hits[i].collider.gameObject);
                    return true;
                }
            }
            return false;
        }
    }
}
using GameScript.Ground;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameScript.PlayerInput
{
    public class PlayerController:MonoBehaviour
    {
        private void Awake()
        {
            CameraManager.CameraManager.SetMainCamera(Camera.main);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // 修改后的代码逻辑
                Ray ray = CameraManager.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
                bool hitIsGround = false;
                bool hitIsUI = false;

                // 1. 先检测是否点击到 UI（使用 EventSystem）
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    hitIsUI = true;
                }
                else
                {
                    // 2. 如果没有点击到 UI，再检测 3D 物体（如 Ground）
                    RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
                    foreach (var hit in hits)
                    {
                        if (hit.collider.CompareTag("Ground"))
                        {
                            GroundManager.Instance.SetCurrentGround(hit.collider.gameObject);
                            hitIsGround = true;
                            break; // 找到 Ground 后退出循环
                        }
                    }
                }

                // 3. 如果没有命中 Ground 或 UI，清空 CurrentGround
                if (!hitIsGround && !hitIsUI)
                {
                    GroundManager.Instance.SetCurrentGround(null);
                }
            }
        }
    }
}
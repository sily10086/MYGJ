using GameScript.Ground;
using UnityEngine;

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
                Ray ray = CameraManager.CameraManager.mainCamera.ScreenPointToRay(Input.mousePosition);
                // 获取所有命中物体的数组
                RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
                bool hitIsGround = false;
                if (hits.Length>0)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.collider.CompareTag("Ground"))
                        {
                            GroundManager.Instance.SetCurrentGround(hit.collider.gameObject);
                            hitIsGround = true;
                        }
                    }
                }
                if(!hitIsGround) GroundManager.Instance.SetCurrentGround(null);
            }
        }
    }
}
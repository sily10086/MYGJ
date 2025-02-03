using UnityEngine;

namespace GameScript.CameraManager
{
    public static class CameraManager
    {
        private static Camera _mainCamera;

        public static Camera mainCamera
        {
            get{return _mainCamera;}
        }

        public static void SetMainCamera(Camera camera)
        {
            _mainCamera = camera;
        }
    };
}
using cfg;
using UnityEngine;

namespace GameScript.Ground
{
    public class GroundMeshType:MonoBehaviour
    {
        [SerializeField] private GroundType _groundType;

        public GroundType GetGroundType()
        {
            return _groundType;
        }
    }
}
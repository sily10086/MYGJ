using UnityEngine;

namespace GameScript.Ground
{
    public interface IGround
    {
        GameObject IGetBuildGround();
        public void IOpenOutLineGround();
        public void ICloseOutLineGround();
        public void IChangeGroundType(GroundEnum type);
        public GroundEnum IGetGroundType();
    }
}
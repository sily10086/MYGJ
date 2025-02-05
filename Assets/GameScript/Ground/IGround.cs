using UnityEngine;

namespace GameScript.Ground
{
    public interface IGround
    {
        GameObject IGetBuildGround();
        void IOpenOutLineGround();
        void ICloseOutLineGround();
        void IChangeGroundType(GroundType type);
        GroundType IGetGroundType();
        int IGetGroundLevel();
        int IGetUsePopulation();
        void IAddPopulation();
    }
}
using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public interface IForceReceiver
    {
        public void ReceiveForce(float force, Vector3 direction, ForceMode2D mode);
    }
}
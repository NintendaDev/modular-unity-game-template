using UnityEngine;

namespace GameTemplate.Gameplay.Common
{
    public interface IPatrolPoint
    {
        public Transform GetNextPoint();
    }
}
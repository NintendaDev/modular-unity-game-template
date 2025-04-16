using UnityEngine;

namespace GameTemplate.Gameplay.GameObjects
{
    public interface IPatrolPoint
    {
        public Transform GetNextPoint();
    }
}
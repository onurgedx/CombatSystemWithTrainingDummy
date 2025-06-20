
using System.Collections.Generic;
using UnityEngine;
namespace CS
{
    public class ClosestLockableTargetCalculator
    {
        private IList<ILockableTarget> _lockableTargetList = new List<ILockableTarget>();
        private float _maximumLockDistance =Mathf.Infinity;

        public ClosestLockableTargetCalculator(IList<ILockableTarget> lockableTargetList )
        {
            _lockableTargetList = lockableTargetList; 
        }

        public ILockableTarget Calculate(Vector3 originPosition)
        {
            ILockableTarget closestLockableTarget = null;
            float closestDistance =_maximumLockDistance;

            foreach (ILockableTarget lockableTarget in _lockableTargetList)
            {
                float distance = (lockableTarget.Position - originPosition).magnitude;
                bool isCloser = distance < closestDistance;
                if (isCloser )
                {
                    closestLockableTarget = lockableTarget;
                    closestDistance = distance;
                }                
            }
            return closestLockableTarget;
        }
        

    }
}
using System;
using System.Threading.Tasks;
using UnityEngine;
namespace CS
{
    public class DummyConfigurationSystem
    {
        private DummyBehaviour[] _dummies;
        private BattleSystem _battleSystem;
        public event Action<IDummy> DummyCreated = delegate{ };


        public DummyConfigurationSystem(DummyBehaviour[] dummies, BattleSystem battleSystem)
        {
            _dummies = dummies;
            _battleSystem = battleSystem;
        }


        public void Configure()
        {
            foreach (DummyBehaviour dummyBehaviour in _dummies)
            {
                float health = 200;
                float reviveDuration = 5;
                Dummy dummy = new Dummy(health, reviveDuration, dummyBehaviour.transform.position + Vector3.up * 1.6f);
                dummyBehaviour.Init(dummy);
                //fix here
                dummy.Dead += async () => 
                { 
                    await Task.Delay(4000);
                    dummy.Revive();
                };
                _battleSystem.RecordDamageable(dummyBehaviour.Collider, dummy);
                DummyCreated.Invoke(dummy);                
            }
        }
    }
}
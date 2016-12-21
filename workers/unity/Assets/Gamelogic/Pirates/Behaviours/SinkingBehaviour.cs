using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Behaviours
{
    // Enable this MonoBehaviour on client workers only
    [EngineType(EnginePlatform.Client)]
    public class SinkingBehaviour : MonoBehaviour
    {

        [Require]
        private Health.Reader HealthReader;
        private bool alreadySunk = false;
        public Animation SinkingAnimation;

        private void OnEnable()
        {
            InitializeSinkingAnimation();
            HealthReader.ComponentUpdated += OnComponentUpdated;
        }

        private void OnDisable()
        {
            HealthReader.ComponentUpdated -= OnComponentUpdated;
        }

        private void OnComponentUpdated(Health.Update update)
        {
            if (update.currentHealth.HasValue)
            {
                if (!alreadySunk && update.currentHealth.Value <= 0)
                {
                    SinkingAnimation.Play();
                    alreadySunk = true;
                }
            }
        }

        private void InitializeSinkingAnimation()
        {
            /*
             * SinkingAnimation is triggered when the ship is first killed. But a worker which checks out
             * the entity after this time (for example, a client connecting to the game later) 
             * must not visualise the ship as still alive.
             * 
             * Therefore, on checkout, any sunk ships jump to the end of the sinking animation.
             */
            if (HealthReader.Data.currentHealth <= 0)
            {
                foreach (AnimationState state in SinkingAnimation)
                {
                    state.normalizedTime = 1;
                }
                SinkingAnimation.Play();
                alreadySunk = true;
            }
        }
    }
}
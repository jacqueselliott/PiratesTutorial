using Improbable.Ship;
using Improbable.Unity;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    // Enable this MonoBehaviour on FSim (server-side) workers only
    [EngineType(EnginePlatform.FSim)]
    public class OnCannonballHit : MonoBehaviour
    {
        // Enable this MonoBehaviour only on the worker with write access for the entity's Health component
        [Require]
        private Health.Writer HealthWriter;

        private void OnTriggerEnter(Collider other)
        {
            /*
             * Unity's OnTriggerEnter runs even if the MonoBehaviour is disabled, so non-authoritative FSims
             * must be protected against null writers
             */
            if (HealthWriter == null)
                return;

            // Ignore collision if this ship is already dead
            if (HealthWriter.Data.currentHealth <= 0)
                return;

            if (other != null && other.gameObject.tag == "Cannonball")
            {
                // Reduce health of this entity when hit
                int newHealth = HealthWriter.Data.currentHealth - 20;
                HealthWriter.Send(new Health.Update().SetCurrentHealth(newHealth));
            }
        }
    }
}
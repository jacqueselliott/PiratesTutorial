using Improbable.Ship;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic.Pirates.Cannons
{
    // This MonoBehaviour will be enabled on both client and server-side workers
    public class CannonFirer : MonoBehaviour
    {
        [Require]
        private ShipControls.Reader ShipControlsReader;
        [Require]
        private Health.Reader HealthReader;

        private Cannon cannon;

        private void Start()
        {
            // Cache entity's cannon gameobject
            cannon = gameObject.GetComponent<Cannon>();
        }

        private void OnEnable()
        {
            ShipControlsReader.ComponentUpdated += OnComponentUpdated;
        }

        private void OnDisable()
        {
            ShipControlsReader.ComponentUpdated -= OnComponentUpdated;
        }

        private void FireCannons(Vector3 direction)
        {
            if (HealthReader.Data.currentHealth <= 0)
            {
                return;
            }

            if (cannon != null)
            {
                cannon.Fire(direction);
            }
        }

        void OnComponentUpdated(ShipControls.Update update)
        {
            for (var i = 0; i < update.fireLeft.Count; i++)
            {
                FireCannons(-transform.right);
            }
            for (var i = 0; i < update.fireRight.Count; i++)
            {
                FireCannons(transform.right);
            }
        }
    }
}
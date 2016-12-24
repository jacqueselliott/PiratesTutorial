using UnityEngine;
using Improbable.Unity.Visualizer;
using Improbable.Unity;
using Improbable.Ship;

namespace Assets.GameLogic.Pirates.Behaviours
{
    [EngineType(EnginePlatform.Fsim)]
    public class SteerRandomly : MonoBehaviour
    {

        [Require]
        private ShipControls.Writer ShipControlsWriter;
        // Use this for initialization
        private void OnEnable()
        {
            InvokeRepeating("RandomizeSteering", 0, 5.0f);
        }

        // Update is called once per frame
        private void OnDisable()
        {
            CancelInvoke("RandomizeSteering");
        }

        private void RandomizeSteering()
        {
            ShipControlsWriter.Send(new ShipControls.Update()
                .SetTargetSpeed(Random.value*0.75f)
                .SetTargetSteering((Random.value*30.0f)-15.0f));
        }
    }
}

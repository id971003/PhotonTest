using UnityEngine.Scripting;
using Photon.Deterministic;

namespace Quantum.Astroids
{
    [Preserve]
    public unsafe class AsteroidsShipSystem : SystemMainThreadFilter<AsteroidsShipSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public Transform2D* Transform;
            public PhysicsBody2D* Body;
            
        }
        public override void Update(Frame f, ref Filter filter)
        {
            // gets the input for player 0
            //var input = frame.GetPlayerInput(0);

            Input* input = default;
            

            if (f.Unsafe.TryGetPointer(filter.Entity, out PlayerLink* playerLink))
            {
                input = f.GetPlayerInput(playerLink->PlayerRef);
            }

            UpdateShipMovement(f, ref filter, input);
        }
        private void UpdateShipMovement(Frame frame, ref Filter filter, Input* input)
        {
            FP shipAcceleration = 7;
            FP turnSpeed = 8;

            if (input->Up)
            {
                filter.Body->AddForce(filter.Transform->Up * shipAcceleration);
            }

            if (input->Left)
            {
                filter.Body->AddTorque(turnSpeed);
            }

            if (input->Right)
            {
                filter.Body->AddTorque(-turnSpeed);
            }

            filter.Body->AngularVelocity = FPMath.Clamp(filter.Body->AngularVelocity, -turnSpeed, turnSpeed);
        }
    }
}

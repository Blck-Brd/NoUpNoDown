// Created by Ronis Vision. All rights reserved
// 02.04.2021.

using RVModules.RVSmartAI.Content.AI.Tasks;

namespace RVHonorAI.Content.AI.Tasks
{
    public class SetTargetAsMoveTarget : AiAgentTask
    {
        #region Not public methods

        protected override void Execute(float _deltaTime) => MoveTarget = ContextAs<ITargetProvider>().Target.Transform;

        #endregion
    }
}
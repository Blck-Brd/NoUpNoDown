// Created by Ronis Vision. All rights reserved
// 13.10.2020.

using RVModules.RVSmartAI.Content.AI.Contexts;
using UnityEngine;

namespace RVModules.RVSmartAI.Content.AI.DataProviders
{
    public class MoveTargetPositionProvider : Vector3Provider
    {
        #region Not public methods

        protected override Vector3 ProvideData() => ContextAs<IMoveTargetProvider>().FollowTarget.position;

        #endregion
    }
}
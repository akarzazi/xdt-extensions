﻿
using XdtExtensions.Helpers;
using XdtExtensions.Microsoft.Web.XmlTransform;

namespace XdtExtensions
{
    internal class RemoveExt : Transform
    {
        public RemoveExt()
         : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Information)
        {
        }

        protected override void Apply()
        {
            CommonErrors.WarnIfMultipleTargets(Log, TransformNameShort, TargetNodes, ApplyTransformToAllTargetNodes);
            var targets = XmlDomHelpers.FindTargetsFromXPathArg(Arguments, TargetNode, nameof(RemoveExt));

            foreach (var target in targets)
            {
                target.ParentNode?.RemoveChild(target);
                break;
            }
        }
    }

    internal class RemoveAllExt : Transform
    {
        public RemoveAllExt()
            : base(TransformFlags.UseParentAsTargetNode, MissingTargetMessage.Information)
        {
            ApplyTransformToAllTargetNodes = true;
        }

        protected override void Apply()
        {
            var targets = XmlDomHelpers.FindTargetsFromXPathArg(Arguments, TargetNode, nameof(RemoveExt));

            foreach (var target in targets)
            {
                target.ParentNode?.RemoveChild(target);
            }
        }
    }
}

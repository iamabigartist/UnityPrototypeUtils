using System;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public class CustomSerVETreeHandlerAttribute : Attribute
{
	public Type TargetType { get; }
	public int Priority { get; }
	public CustomSerVETreeHandlerAttribute(Type target_type, int priority = 0)
	{
		TargetType = target_type;
		Priority = priority;
	}
}
}
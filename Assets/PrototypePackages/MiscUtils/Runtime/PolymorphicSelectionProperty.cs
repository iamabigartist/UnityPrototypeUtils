using System;
namespace PrototypePackages.MiscUtils
{
public interface IInstanceGenerator
{
	object Generate(Type type);
}

public class DefaultInstanceGenerator : IInstanceGenerator
{
	public object Generate(Type type)
	{
		return Activator.CreateInstance(type);
	}
}

public class PolymorphicSelectAttribute : Attribute
{
	public Type InstanceGenType;
	public PolymorphicSelectAttribute() { InstanceGenType = typeof(DefaultInstanceGenerator); }
	public PolymorphicSelectAttribute(Type InstanceGenType) { this.InstanceGenType = InstanceGenType; }
}
}
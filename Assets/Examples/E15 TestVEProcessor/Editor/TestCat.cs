using System;
using System.Collections.Generic;
using PrototypePackages.MiscUtils.Editor.UIElementUtils;
using UnityEditor;
using UnitySerVE;
using static PrototypePackages.MiscUtils.Editor.UIElementUtils.SerMarkVENode_UnitySerProp;
namespace Examples.E15_TestVEProcessor
{
[CustomSerVETreeHandler(typeof(Cat))]
public class CatVEProcessor : SerVETreeProcessor_UnitySerProp
{
	public override bool CanProcess(SerializedProperty ser_prop) => true;
	public override VENode ProcessTree(SerializedProperty ser_prop, VENode child_root) => new()
	{
		Mark(ser_prop, nameof(Cat.meow)),
		Mark(ser_prop, nameof(Cat.age))
	};
}
}
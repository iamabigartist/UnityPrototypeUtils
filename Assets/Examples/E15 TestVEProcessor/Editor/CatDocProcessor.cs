using System;
using System.Collections.Generic;
using System.Linq;
using PrototypePackages.MiscUtils.Editor.Odin;
using PrototypePackages.MiscUtils.Editor.Odin.AttributeDoc;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
namespace Examples.E15_TestVEProcessor.Editor
{
public class CatAttrProcessor : OdinAttributeProcessor<Cat>
{
	public override void ProcessSelfAttributes(InspectorProperty property, List<Attribute> attributes)
	{
		attributes.Add(new ModifyGUISkinAttribute());
	}
}
public class CatDocProcessor : OdinPropertyProcessor<Cat>
{
	public static string Name<T>() => typeof(T).FullName;

	public static void OnMeowChanged(Cat cat)
	{
		cat.att = cat.meow.Length;
		Debug.Log($"Meow changed! {cat.meow}");
	}

	public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
	{
		InspectorDocNode doc = new(new() {})
		{
			new("一般信息", new() { new BoxGroupAttribute { ShowLabel = true } })
			{
				new(new() { new LabelTextAttribute("名字") }, "name"),
				new(new() { new LabelTextAttribute("年龄"), new SuffixLabelAttribute("岁") }, "age")
			},
			new("猫咪信息", new() { new BoxGroupAttribute { ShowLabel = true } })
			{
				new(new() { new LabelTextAttribute("喵喵"), new OnValueChangedAttribute($"@{Name<CatDocProcessor>()}.OnMeowChanged(this)") }, "meow"),
				new(new() { new LabelTextAttribute("攻击力") }, "att")
			}
		};
		doc.Apply(propertyInfos.ToHashSet(), out _);
	}
}
}
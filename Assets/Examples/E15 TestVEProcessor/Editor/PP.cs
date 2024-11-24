using System;
using System.Collections.Generic;
using PrototypePackages.MiscUtils.Editor.Odin.AttributeDoc;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
namespace Examples.E15_TestVEProcessor.Editor
{

// public class PP : OdinPropertyProcessor<TestProcessorMechan>
// {
// 	InspectorPropertyInfo pa_p;
// 	public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
// 	{
// 		propertyInfos.Remove("AAA");
// 		propertyInfos.AddValue("ManualAAA", (ref TestProcessorMechan t) =>
// 		{
// 			return t.AAA;
// 		}, (ref TestProcessorMechan t, int v) =>
// 		{
// 			t.AAA = v;
// 			t.AA = v.ToString();
// 		});
//
// 		pa_p = propertyInfos.Find("pa");
// 		pa_p.GetEditableAttributesList().Add(new HideInInspector());
// 		propertyInfos.Find("pb").GetEditableAttributesList().Add(new HideInInspector());
// 		propertyInfos.AddValue("ManualXX", (ref TestProcessorMechan t) =>
// 		{
// 			return t.pa?.XX ?? "";
// 		}, (ref TestProcessorMechan t, string v) =>
// 		{
// 			t.pa ??= new();
// 			t.pa.XX = v;
// 		}, 0, SerializationBackend.Odin, new HorizontalGroupAttribute("XYZ"));
//
// 		propertyInfos.AddValue("ManualYY", (ref TestProcessorMechan t) =>
// 		{
// 			return t.pa?.YY ?? 0;
// 		}, (ref TestProcessorMechan t, int v) =>
// 		{
// 			t.pa ??= new();
// 			t.pa.YY = v;
// 		}, new HorizontalGroupAttribute("XYZ"));
//
// 		propertyInfos.AddValue("ManualZZ", (ref TestProcessorMechan t) =>
// 		{
// 			return t.pb?.ZZ ?? 0;
// 		}, (ref TestProcessorMechan t, int v) =>
// 		{
// 			t.pb ??= new();
// 			t.pb.ZZ = v;
// 		}, new HorizontalGroupAttribute("XYZ"));
//
// 		var list = propertyInfos.FindAll(p => p.GetAttribute<BoxGroupAttribute>() is not null);
// 		foreach (var p in list)
// 		{
// 			p.GetAttribute<BoxGroupAttribute>().GroupID = "AAABBB";
// 		}
// 	}
// }
}
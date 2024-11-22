using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public class SerVETreeGen_UnitySer : SerVETreeGenerator<SerializedProperty>
{
	public class ProcessorComparer : IComparer<(int priority, SerVETreeProcessor_UnitySerProp processor)>
	{
		public int Compare(
			(int priority, SerVETreeProcessor_UnitySerProp processor) x,
			(int priority, SerVETreeProcessor_UnitySerProp processor) y)
		{
			var compare_res = x.priority.CompareTo(y.priority);
			if (compare_res == 0) { compare_res = 1; }
			return compare_res;
		}
	}
	Dictionary<Type, SortedSet<(int priority, SerVETreeProcessor_UnitySerProp processor)>> ProcessorDict = new();
	PrimitiveSerProcessor MyPrimitiveProcessor = new();
	public SerVETreeGen_UnitySer()
	{
		foreach (var type in TypeCache.GetTypesWithAttribute<CustomSerVETreeHandlerAttribute>())
		{
			CollectProcessorsFromAssembly(type);
		}
	}
	public void CollectProcessorsFromAssembly(Type type)
	{
		if (type.IsAbstract || type.IsInterface) { return; }
		if (typeof(SerVETreeProcessor_UnitySerProp).IsAssignableFrom(type) &&
			type.GetCustomAttribute<CustomSerVETreeHandlerAttribute>() is {} attr)
		{
			var target_type = attr.TargetType;
			var priority = attr.Priority;
			var processor = (SerVETreeProcessor_UnitySerProp)Activator.CreateInstance(type);
			if (!ProcessorDict.ContainsKey(target_type)) { ProcessorDict[target_type] = new(new ProcessorComparer()); }
			ProcessorDict[target_type].Add((priority, processor));
		}
	}

	public SerVETreeProcessor_UnitySerProp TryGetProcessor(SerializedProperty ser_prop)
	{
		var type = ser_prop.boxedValue.GetType();
		if (!ProcessorDict.TryGetValue(type, out var processor_set)) { return null; }
		foreach (var (priority, processor) in processor_set)
		{
			if (processor.CanProcess(ser_prop)) { return processor; }
		}
		return null;
	}

	public override List<SerVETreeProcessor<SerializedProperty>> GetPropProcessorList(SerializedProperty ser_prop)
	{
		var processor_list = new List<SerVETreeProcessor<SerializedProperty>>();
		var cur_type = ser_prop.boxedValue.GetType();
		var is_primitive = MyPrimitiveProcessor.CanProcess(ser_prop);
		if (is_primitive)
		{
			processor_list.Add(MyPrimitiveProcessor);
			return processor_list;
		}
		while (cur_type != null)
		{
			if (TryGetProcessor(ser_prop) is {} processor)
			{
				processor_list.Add(processor);
			}
			cur_type = cur_type.BaseType;
		}
		if (processor_list.Count == 0)
		{
			processor_list.Add(new DefaultSerVETreeProcessor_UnitySerProp());
		}
		return processor_list;
	}
}
}
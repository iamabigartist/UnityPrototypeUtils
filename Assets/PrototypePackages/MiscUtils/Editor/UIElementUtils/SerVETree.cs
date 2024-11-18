using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public abstract class SerVETreeProcessor<TSerProp>
{
	public abstract VENode CreateTree(VENode root, TSerProp ser_prop);
}
public abstract class SerMarkVENode<TSerProp> : VENode
{
	public override VisualElement GenerateVisualElement() => Children.Single().GenerateVisualElement();
	public abstract TSerProp SerProp { get; }
	public List<SerVETreeProcessor<TSerProp>> Modifiers = new();
}
public abstract class SerVETreeGenerator<TSerProp>
{
	public abstract List<SerVETreeProcessor<TSerProp>> GetPropProcessorList(TSerProp ser_prop);
	public VENode GenerateTree(TSerProp root_ser_prop)
	{
		// 对于每一个序列化文档节点，首先遍历其生成类型，从子类型到父类型生成器包裹调用，然后再根据优先级调用修改器生成，最后返回根节点。
		throw new NotImplementedException();
	}
	void GenerateTree_Impl(SerMarkVENode<TSerProp> cur_mark_node)
	{
		var ser_prop = cur_mark_node.SerProp;
		var modifier_list = cur_mark_node.Modifiers;
		var provider_list = GetPropProcessorList(ser_prop);
		var root = new VENode();
		foreach (var provider in provider_list) { root = provider.CreateTree(root, ser_prop); }
		foreach (var modifier in modifier_list) { root = modifier.CreateTree(root, ser_prop); }
		foreach (var node in root.Flatten())
		{
			if (node is not SerMarkVENode<TSerProp> descendant_mark_node) { continue; }
			GenerateTree_Impl(descendant_mark_node);
		}
		cur_mark_node.Children.Add(root);
	}
}
}
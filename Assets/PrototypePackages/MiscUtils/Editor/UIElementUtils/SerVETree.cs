using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnitySerVE;
namespace PrototypePackages.MiscUtils.Editor.UIElementUtils
{
public abstract class SerVETreeProcessor<TSerProp>
{
	public abstract VENode ProcessTree(TSerProp ser_prop, VENode child_root);
}
public class SerMarkVENode<TSerProp> : VENode
{
	public override VisualElement CreateVE() => throw new NotSupportedException();
	public new VisualElement GenerateVE() => Children.Single().GenerateVE();
	public TSerProp SerProp;
	public List<SerVETreeProcessor<TSerProp>> ModifierList = new();
}
public abstract class SerVETreeGenerator<TSerProp>
{
	public abstract List<SerVETreeProcessor<TSerProp>> GetPropProcessorList(TSerProp ser_prop);
	public VENode GenerateTree(TSerProp root_ser_prop)
	{
		var root_mark_node = new SerMarkVENode<TSerProp> { SerProp = root_ser_prop };
		GenerateTree_Impl(root_mark_node);
		return root_mark_node;
	}
	void GenerateTree_Impl(SerMarkVENode<TSerProp> cur_mark_node)
	{
		// 对于每一个序列化文档节点，首先遍历其生成类型，从子类型到父类型生成器包裹调用，然后再根据优先级调用修改器生成，最后返回根节点。
		var ser_prop = cur_mark_node.SerProp;
		var provider_list = GetPropProcessorList(ser_prop);
		var modifier_list = cur_mark_node.ModifierList;
		VENode child_root = null;
		foreach (var provider in provider_list) { child_root = provider.ProcessTree(ser_prop, child_root); }
		foreach (var modifier in modifier_list) { child_root = modifier.ProcessTree(ser_prop, child_root); }
		foreach (var node in child_root.Flatten())
		{
			if (node is not SerMarkVENode<TSerProp> descendant_mark_node) { continue; }
			GenerateTree_Impl(descendant_mark_node);
		}
		cur_mark_node.Children.Add(child_root);
	}
}
}
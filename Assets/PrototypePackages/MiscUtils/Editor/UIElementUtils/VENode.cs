using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
public abstract class VENode : IEnumerable<VENode>
{
	public abstract VisualElement CreateVE();
	public virtual VisualElement PostGenerateVE(VisualElement root) => root;
	public virtual void AddChildrenVE(VisualElement root, List<VisualElement> children)
	{
		foreach (var child in children) { root.Add(child); }
	}
	public VisualElement GenerateVE()
	{
		var ve = CreateVE();
		var children = new List<VisualElement>();
		foreach (var child in this) { children.Add(child.GenerateVE()); }
		AddChildrenVE(ve, children);
		ve = PostGenerateVE(ve);
		return ve;
	}
	public List<VENode> Children = new();
	public void Add(VENode node) => Children.Add(node);
	public IEnumerator<VENode> GetEnumerator() => Children.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public IEnumerable<VENode> Flatten()
	{
		var stack = new Stack<VENode>(Children);
		while (stack.Count > 0)
		{
			var node = stack.Pop();
			yield return node;
			foreach (var child in node.Children) { stack.Push(child); }
		}
	}
}
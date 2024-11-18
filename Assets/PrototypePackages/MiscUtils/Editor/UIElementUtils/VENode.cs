using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
public class VENode : IEnumerable<VENode>
{
	public virtual VisualElement GenerateVisualElement() => new();
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
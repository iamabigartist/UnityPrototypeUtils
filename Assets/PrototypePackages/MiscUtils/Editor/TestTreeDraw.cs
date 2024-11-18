using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
namespace PrototypePackages.MiscUtils.Editor
{
public static class TestTreeDraw
{
	public class VENode : IEnumerable<VENode>
	{
		public event Func<VisualElement, VisualElement> AfterDraw;
		public List<VENode> ChildrenList = new();
		public void Add(VENode node) => ChildrenList.Add(node);
		public IEnumerator<VENode> GetEnumerator() => ChildrenList.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public virtual VisualElement OnGenerate() => new();
	}

	public abstract class TreeGen
	{
		public abstract VENode GenRoot();
		public void A()
		{
			VENode n = new()
			{
				new(),
				new()
			};
		}
	}
	public interface IDrawCallNodeGenArg {}
	public interface IDrawCallNode<TArg> where TArg : IDrawCallNodeGenArg
	{
		IDrawCallNode<TArg> Generate(TArg arg);
	}
	public abstract class Expander<TNode, TArg>
	{
		public abstract VisualElement Draw<TArg>(IDrawCallNode<TArg> node, TArg arg) where TArg : IDrawCallNodeGenArg;
	}
}
}
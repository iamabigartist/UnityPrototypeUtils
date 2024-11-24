using System.Collections;
using System.Collections.Generic;
namespace PrototypePackages.MiscUtils
{
public abstract class TreeNode<T> : IEnumerable<T> where T : TreeNode<T>
{
	public T Parent;
	public List<T> ChildrenList = new();
	public IEnumerator<T> GetEnumerator() => ChildrenList.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	public void Add(T child)
	{
		child.Parent = (T)this;
		ChildrenList.Add(child);
	}
	public void Remove(T child)
	{
		child.Parent = null;
		ChildrenList.Remove(child);
	}
	public IEnumerable<T> DFS()
	{
		foreach (var child in ChildrenList)
		{
			foreach (var node in child.DFS())
			{
				yield return node;
			}
		}
		yield return (T)this;
	}
}
}
using System.Collections.Generic;
using UnityEngine;
public class TupleComparer : IComparer<(int p, string info)>
{
	public int Compare(
		(int p, string info) x,
		(int p, string info) y)
	{
		var res = x.p.CompareTo(y.p);
		if (res == 0) { res = 1; }
		return res;
	}
}
public class TestSorted : MonoBehaviour
{
	SortedSet<(int p, string info)> set;
	void Start()
	{
		//尝试添加优先级相同但是信息不同的元素
		//然后查看添加结果
		set = new(new TupleComparer()) { (1, "a") };
		var b_added = set.Add((1, "b"));
		Debug.Log($"b_added: {b_added}");
	}

	void Update() {}
}
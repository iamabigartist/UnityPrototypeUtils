using System;
using System.Collections;
using System.Collections.Generic;
using Castle.DynamicProxy;
using UnityEditor;
using UnityEngine.UIElements;
namespace Examples.E16_TestListView
{
public class LoggingInterceptor : IInterceptor
{
	public void Intercept(IInvocation invocation)
	{
		Console.WriteLine($"Before executing {invocation.Method.Name}");
		invocation.Proceed();
		Console.WriteLine($"After executing {invocation.Method.Name}");
	}
}

public static class AOPListFactory
{
	static readonly ProxyGenerator ProxyGenerator = new();

	public static IList Create<T>(List<T> target)
	{
		return ProxyGenerator.CreateInterfaceProxyWithTarget<IList>(target, new LoggingInterceptor());
	}
}
public class TestListViewWnd : EditorWindow
{
	[MenuItem("Tools/TestListView")]
	public static void ShowWindow()
	{
		var wnd = GetWindow<TestListViewWnd>("TestListView");
	}

	public IList list;

	void Awake()
	{
		list = AOPListFactory.Create(new List<int> { 1, 2, 3, 4, 5 });
	}
	void CreateGUI()
	{
		var list_view = new ListView
		{
			bindItem = (e, i) => e.Q<Label>().text = i.ToString(),
			makeItem = () => new Label(),
			itemsSource = list
		};
		rootVisualElement.Add(list_view);
	}
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using InspectorPropertyInfo = Sirenix.OdinInspector.Editor.InspectorPropertyInfo;
namespace PrototypePackages.MiscUtils.Editor.Odin.AttributeDoc
{
public class PropSelector
{
	public delegate HashSet<InspectorPropertyInfo> SelectDelegate(HashSet<InspectorPropertyInfo> child_info_list);
	public SelectDelegate SelectFunc;
	public HashSet<InspectorPropertyInfo> Select(HashSet<InspectorPropertyInfo> cur_scope_child_info) => SelectFunc(cur_scope_child_info);
	public PropSelector(SelectDelegate func) => SelectFunc = func;
	public static implicit operator PropSelector(InspectorPropertyInfo info) => new(_ => new() { info });
	public static implicit operator PropSelector(string name) => new(child_info_list => new() { child_info_list.First(x => x.PropertyName == name) });
	public static implicit operator PropSelector(List<string> keyword_list) => new(child_info_list =>
	{
		child_info_list.RemoveWhere(x => !keyword_list.Exists(k => x.PropertyName.Contains(k)));
		return child_info_list;
	});
}
public class AttributeInstaller
{
	public delegate void InstallDelegate(InspectorDocNode node, InspectorPropertyInfo info);
	public InstallDelegate InstallFunc;
	public void Install(InspectorDocNode node, InspectorPropertyInfo info) => InstallFunc(node, info);
	public AttributeInstaller(InstallDelegate func) => InstallFunc = func;
	public static implicit operator AttributeInstaller(Attribute attribute) => new((_, info) =>
	{
		var list = info.GetEditableAttributesList();
		list.Add(attribute);
	});
	public static implicit operator AttributeInstaller(PropertyGroupAttribute group_attribute) => new((node, info) =>
	{
		group_attribute.GroupID = node.Path;
		group_attribute.GroupName = node.PathCurrentName;
		var list = info.GetEditableAttributesList();
		list.Add(group_attribute);
	});
}

public class InspectorDocNode : TreeNode<InspectorDocNode>
{
	public string name;
	string path
	{
		get
		{
			var parent_path = Parent?.path;
			var sep = parent_path is not null && name is not null ? "/" : "";
			var res_path = parent_path is null && name is null ? null : $"{parent_path}{sep}{name}";
			return res_path;
		}
	}
	public string PathCurrentName
	{
		get
		{
			var i = path.LastIndexOf('/');
			var res_name = i == -1 ? path : path[(i + 1)..];
			Debug.Log(res_name);
			return res_name;
		}
	}
	public string Path => path;
	public List<AttributeInstaller> m_AttributeInstallerList;
	public PropSelector m_PropSelector;
	public InspectorDocNode(string name, List<AttributeInstaller> attribute_installer_list, PropSelector prop_selector = null)
	{
		this.name = name;
		m_PropSelector = prop_selector;
		m_AttributeInstallerList = attribute_installer_list;
	}
	public InspectorDocNode(List<AttributeInstaller> attribute_installer_list, PropSelector prop_selector = null) : this(null, attribute_installer_list, prop_selector) {}
	public InspectorDocNode() : this(null, null, null) {}

	public void Apply(HashSet<InspectorPropertyInfo> full_info_list, out HashSet<InspectorPropertyInfo> cur_scope_info_list)
	{
		cur_scope_info_list = ChildrenList.Count == 0 ? new(full_info_list) : new();
		foreach (var child_node in ChildrenList)
		{
			child_node.Apply(full_info_list, out var cur_selected_info_list);
			cur_scope_info_list.UnionWith(cur_selected_info_list);
		}
		if (m_PropSelector is not null) { cur_scope_info_list = m_PropSelector.Select(cur_scope_info_list); }
		if (m_AttributeInstallerList is not null)
		{
			foreach (var info in cur_scope_info_list)
			{
				foreach (var installer in m_AttributeInstallerList)
				{
					installer.Install(this, info);
				}
			}
		}
	}
}
}
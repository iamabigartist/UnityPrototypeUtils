using System;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
namespace PrototypePackages.MiscUtils.Editor.Odin
{
public class ModifyGUISkinAttribute : Attribute {}
public class ModifyGUISkinAttributeDrawer : OdinAttributeDrawer<ModifyGUISkinAttribute>
{
	protected override void DrawPropertyLayout(GUIContent label)
	{
		GUI.skin.label.fontSize = 15;
		CallNextDrawer(label);
		GUI.skin.label.fontSize = 12;
	}
}
}
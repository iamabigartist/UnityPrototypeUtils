using System;
using System.Collections.Generic;
using UnityEngine;
namespace Examples.E15_TestVEProcessor
{
[Serializable]
public abstract partial class Animal
{
	public string name;
	public int age;

}
[Serializable]
public partial class Cat : Animal
{
	public string meow;
	public int att;
	[SerializeReference]
	public List<Cat> TreeChildren;
}
}
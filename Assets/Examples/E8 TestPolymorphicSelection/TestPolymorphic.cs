using System;
using PrototypePackages.PrototypeUtils;
using UnityEngine;
namespace Examples.E8_TestPolymorphicSelection
{
	[Serializable]
	public class Animal
	{
		public string name;
		public Color color;
	}
	[Serializable]
	public class Dog : Animal
	{
		public float speed;
	}

	[Serializable]
	public class Cat : Animal
	{
		public float attack;
	}

	public class TestPolymorphic : MonoBehaviour
	{
		[SerializeReference] [PolymorphicSelect]
		Animal[] animals;

		void Start() {}

		void Update() {}
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;
namespace PrototypePackages.PrototypeUtils.StateMachine0.StateMachine2
{
	public abstract class FSMInterface<T> where T : class
	{
		public T Data;
		public virtual void Start() {}
		public virtual void Update() {}
		public virtual void Terminate() {}
	}

	public class FSM<TState, TData, TInterface>
		where TData : class
		where TState : Enum
		where TInterface : FSMInterface<TData>

	{
		Dictionary<TState, TInterface> states;
		TInterface cur_state;
	}


	public class DogData
	{
		float HP;
		float Hunger;
	}
	public abstract class DogInterface : FSMInterface<DogData>
	{
		public virtual void Woof()
		{
			Debug.Log("Woof");
		}
		public virtual void Bark()
		{
			Debug.Log("Bark");
		}
	}

	public class DogActionFSM : DogInterface
	{
		public enum State
		{
			Idle,
			Walk,
			Run,
			Jump,
			Fall,
			Die
		}
		DogData data;
		FSM<State, DogData, DogInterface> fsm;
		
	}
}
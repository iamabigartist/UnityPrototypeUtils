using System;
using System.Collections.Generic;
namespace PrototypePackages.PrototypeUtils.StateMachine0.SM3
{
	public abstract class MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>
		where TThisMachine : StateMachine<TThisMachine, TInterfaceEnum, TStateEnum>
		where TInterfaceEnum : Enum
		where TStateEnum : Enum
	{
		public TThisMachine machine;
		public Dictionary<TInterfaceEnum, Action<object>> interfaces;
		protected void InitDriver(Dictionary<TInterfaceEnum, Action<object>> Interfaces)
		{
			interfaces = Interfaces;
		}

		public virtual void Enter() {}
		public virtual void Exit() {}
		public virtual void OnDriverUpdate() {}
		public abstract bool NextState(out TStateEnum next_state);

	}

	public abstract class StateMachine<TThisMachine, TInterfaceEnum, TStateEnum>
		where TThisMachine : StateMachine<TThisMachine, TInterfaceEnum, TStateEnum>
		where TInterfaceEnum : Enum
		where TStateEnum : Enum
	{
		Dictionary<TStateEnum, MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>> drivers;
		Dictionary<TInterfaceEnum, Action<object>> base_interfaces;
		public TStateEnum cur_state { get; private set; }
		MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum> cur_driver;

		protected void InitMachine(Dictionary<TInterfaceEnum, Action<object>> baseInterfaces, Dictionary<TStateEnum, MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>> Drivers, TStateEnum start_state)
		{
			base_interfaces = baseInterfaces;
			drivers = Drivers;
			cur_state = start_state;
			cur_driver = Drivers[cur_state];
			foreach (var driver in Drivers.Values) { driver.machine = this as TThisMachine; }
		}

		protected abstract void OnStateTransition(TStateEnum new_state);

		void TransitState(TStateEnum new_state)
		{
			cur_driver.Exit();
			OnStateTransition(new_state);
			cur_state = new_state;
			cur_driver = drivers[cur_state];
			cur_driver.Enter();
		}

		public void Run(TInterfaceEnum interface_name, object args)
		{
			cur_driver.interfaces[interface_name](args);
			base_interfaces[interface_name](args);
		}

		public abstract void OnMachineUpdate();

		public void Update()
		{
			cur_driver.OnDriverUpdate();
			if (cur_driver.NextState(out var next_state)) { TransitState(next_state); }
			OnMachineUpdate();
		}
	}
}
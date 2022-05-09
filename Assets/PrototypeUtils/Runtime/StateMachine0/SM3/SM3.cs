using System;
using System.Collections.Generic;
namespace PrototypeUtils.StateMachine0.SM3
{
	public abstract class MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>
		where TThisMachine : StateMachine<TThisMachine, TInterfaceEnum, TStateEnum>
		where TInterfaceEnum : Enum
		where TStateEnum : Enum
	{
		public TThisMachine machine_data;
		public readonly Dictionary<TInterfaceEnum, Action<object>> interfaces;
		protected MachineDriver(Dictionary<TInterfaceEnum, Action<object>> interfaces)
		{
			this.interfaces = interfaces;
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
		readonly Dictionary<TStateEnum, MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>> drivers;
		readonly Dictionary<TInterfaceEnum, Action<object>> base_interfaces;
		public TStateEnum cur_state { get; private set; }
		MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum> cur_driver;
		protected StateMachine(Dictionary<TInterfaceEnum, Action<object>> base_interfaces, Dictionary<TStateEnum, MachineDriver<TThisMachine, TInterfaceEnum, TStateEnum>> drivers, TStateEnum start_state)
		{
			this.base_interfaces = base_interfaces;
			this.drivers = drivers;
			cur_state = start_state;
			cur_driver = drivers[cur_state];
			foreach (var driver in drivers.Values) { driver.machine_data = this as TThisMachine; }
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
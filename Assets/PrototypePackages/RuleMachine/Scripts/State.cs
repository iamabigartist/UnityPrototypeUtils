using System;
using System.Collections.Generic;
namespace PrototypePackages.RuleMachine
{
	public class State<TEnum>
	{
		public TEnum Current;
		public TEnum Next;
		public Dictionary<(TEnum Current, TEnum Next), Action> OnTransitStateActions;
		public State(TEnum Initial, Dictionary<(TEnum Current, TEnum Next), Action> OnTransitStateActions)
		{
			Current = Initial;
			this.OnTransitStateActions = OnTransitStateActions;
			Next = Current;
		}

		static void CompareAndUpdate<T>(ref T destination, T value, Func<T, T, bool> comparer, out bool changed)
		{
			changed = !comparer(destination, value);
			if (changed) { destination = value; }
		}

		public bool TransitState(out (TEnum Current, TEnum Next) states)
		{
			states = (Current, Next);
			OnTransitStateActions.TryGetValue(states, out var action);
			CompareAndUpdate(ref Current, Next, (a, b) => a.Equals(b), out var changed);
			if (changed) { action!.Invoke(); }
			return changed;
		}
	}
}
using PrototypeUtils;
using PrototypeUtils.StateMachine0.SM3;
namespace Examples.E6_TestSM3
{
	public class BonePlating :
		StateMachine<BonePlating, BonePlating.DamageModifier, BonePlating.State>
	{
		public int max_plating_count = 3;
		public PeriodTimer activated_duration_timer;
		public PeriodTimer cooldown_timer;
		public int cur_plate_count;
		public enum DamageModifier
		{
			OnReceivedDamage
		}
		public enum State
		{
			Cooled,
			Activated,
			Cooling
		}
		public BonePlating(double activated_duration_ms, double cooldown_ms) : base(
			new()
			{
				{ DamageModifier.OnReceivedDamage, _ => {} }
			},
			new()
			{
				{ State.Cooled, new Cooled() },
				{ State.Activated, new Activated() },
				{ State.Cooling, new Cooling() }
			}, State.Cooled)
		{
			activated_duration_timer = new(activated_duration_ms);
			cooldown_timer = new(cooldown_ms);
			cur_plate_count = max_plating_count;
		}
		protected override void OnStateTransition(State new_state) {}
		public override void OnMachineUpdate()
		{

		}

	#region Interface

		public double ActivatedDurationMs
		{
			get => activated_duration_timer.period_ms;
			set => activated_duration_timer.period_ms = value;
		}

		public double CooldownMs
		{
			get => cooldown_timer.period_ms;
			set => cooldown_timer.period_ms = value;
		}

		public int CurPlateCount => cur_plate_count;

	#endregion


	#region StateDrivers

		public class Cooled : MachineDriver<BonePlating, DamageModifier, State>
		{
			public Cooled() : base(new()
			{
				{ DamageModifier.OnReceivedDamage, _ => {} }
			})
			{
			}

			void OnReceivedDamage()
			{
				machine_data.cur_plate_count--;
			}

			public override bool NextState(out State next_state)
			{

			}
		}
		public class Activated : MachineDriver<BonePlating, DamageModifier, State>
		{
			public Activated() : base(new()
			{
				{ DamageModifier.OnReceivedDamage, _ => {} }
			})
			{
			}
			public override bool NextState(out State next_state) {}
		}
		public class Cooling : MachineDriver<BonePlating, DamageModifier, State>
		{
			public Cooling() : base(new()
			{
				{ DamageModifier.OnReceivedDamage, _ => {} }
			})
			{
			}
			public override bool NextState(out State next_state) {}
		}

	#endregion

	}
}
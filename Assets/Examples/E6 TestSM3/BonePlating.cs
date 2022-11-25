using PrototypePackages.PrototypeUtils.Runtime.StateMachine0.SM3;
using PrototypePackages.TimeUtil.Scripts;
namespace Examples.E6_TestSM3
{
	public class Damage
	{
		public float value;
		public Damage(float value)
		{
			this.value = value;
		}
	}

	public partial class BonePlating :
		StateMachine<BonePlating, BonePlating.DamageModifier, BonePlating.State>
	{
		public PeriodTimer activated_duration_timer;
		public PeriodTimer cooldown_timer;
		int max_plating_count;
		int cur_plate_count;
		float damage_reduce_value;

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
		public BonePlating(double activated_duration_ms, double cooldown_ms, int max_plating_count, float damage_reduce_value)
		{
			InitMachine(
				new()
				{
					{ DamageModifier.OnReceivedDamage, _ => {} }
				},
				new()
				{
					{ State.Cooled, new Cooled() },
					{ State.Activated, new Activated() },
					{ State.Cooling, new Cooling() }
				}, State.Cooled);
			activated_duration_timer = new(activated_duration_ms);
			cooldown_timer = new(cooldown_ms);
			this.max_plating_count = max_plating_count;
			cur_plate_count = this.max_plating_count;
			this.damage_reduce_value = damage_reduce_value;
		}
		protected override void OnStateTransition(State new_state) {}
		public override void OnMachineUpdate()
		{

		}

	#region Process

		float GetReducedDamage(Damage source_damage) { return source_damage.value - damage_reduce_value; }
		void UseOnePlate(object param)
		{
			if (!(param is (Damage source_damage, Damage result_damage))) { return; }
			cur_plate_count--;
			result_damage.value = GetReducedDamage(source_damage);
		}

	#endregion

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
		public int MaxPlatingCount { get => max_plating_count; set => max_plating_count = value; }
		public float DamageReduceValue { get => damage_reduce_value; set => damage_reduce_value = value; }

	#endregion


	#region StateDrivers

		public class Cooled : MachineDriver<BonePlating, DamageModifier, State>
		{

			bool need_activate;
			public Cooled()
			{
				InitDriver(new()
				{
					{ DamageModifier.OnReceivedDamage, OnReceivedDamage }
				});
				need_activate = false;
			}

			void OnReceivedDamage(object param)
			{
				need_activate = true;
				machine.UseOnePlate(param);
			}

			public override bool NextState(out State next_state)
			{
				if (need_activate)
				{
					next_state = State.Activated;
					need_activate = false;
					return true;
				}
				else
				{
					next_state = default;
					return false;
				}
			}
		}
		public class Activated : MachineDriver<BonePlating, DamageModifier, State>
		{
			public Activated()
			{
				InitDriver(new()
				{
					{ DamageModifier.OnReceivedDamage, OnReceivedDamage }
				});
			}

			public override void Enter()
			{
				machine.activated_duration_timer.StartNewPeriod();
			}
			public override bool NextState(out State next_state)
			{
				if (machine.activated_duration_timer.IsTimeUp())
				{
					next_state = State.Cooling;
					return true;
				}
				else
				{
					next_state = default;
					return false;
				}
			}

			public void OnReceivedDamage(object param)
			{
				if (machine.cur_plate_count > 0)
				{
					machine.UseOnePlate(param);
				}
				else
				{
					machine.activated_duration_timer.SetTimeUp();
				}
			}
		}
		public class Cooling : MachineDriver<BonePlating, DamageModifier, State>
		{
			public Cooling()
			{
				InitDriver(new()
				{
					{ DamageModifier.OnReceivedDamage, _ => {} }
				});
			}

			public override void Enter()
			{
				machine.cooldown_timer.StartNewPeriod();
			}

			public override bool NextState(out State next_state)
			{
				if (machine.cooldown_timer.IsTimeUp())
				{
					next_state = State.Cooled;
					return true;
				}
				else
				{
					next_state = default;
					return false;
				}
			}
		}

	#endregion

	}
}
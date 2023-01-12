using System;
using System.Collections.Generic;
namespace PrototypePackages.PrototypeUtils.StateMachine0
{
    public abstract class Machine { }
    public abstract class NoneMachine : Machine { }
    public enum NoneState { }

    /// <summary>
    ///     A machine used as a state of other machines
    /// </summary>
    /// <typeparam name="TStatedMachine">The controlled machine that use this machine as a state</typeparam>
    public abstract class StateMachine<TStatedMachine, TStatedMachineStateEnum> : Machine
        where TStatedMachine : Machine
        where TStatedMachineStateEnum : Enum
    {
        public bool running { get; private set; }
        public readonly TStatedMachineStateEnum state_name;
        public TStatedMachine m_stated_machine;
        protected StateMachine(TStatedMachineStateEnum state_name) { this.state_name = state_name; }
        protected virtual void OnMachineEnter(TStatedMachineStateEnum last_state_name) { }
        protected virtual void OnMachineExit(TStatedMachineStateEnum next_state_name) { }
        protected virtual void OnMachineUpdate() { }

        public void EnterMachine(TStatedMachineStateEnum last_state_name)
        {
            OnMachineEnter( last_state_name );
            running = true;
        }
        public void ExitMachine(TStatedMachineStateEnum next_state_name)
        {
            OnMachineExit( next_state_name );
            running = false;
        }
        public void UpdateMachine()
        {
            if (!running) { return; }

            OnMachineUpdate();
        }
    }

    /// <summary>
    ///     <para>From <see cref="StateMachine{TStatedMachine}" />: <inheritdoc cref="StateMachine{TStatedMachine}" /></para>
    ///     <para>At the same time, a machine that use other machine as states.</para>
    /// </summary>
    /// <typeparam name="TCurrentMachine">
    ///     The real type of this machine, used to tell the states what type of machine they are controlling.
    /// </typeparam>
    /// <typeparam name="TStatedMachine">
    ///     <inheritdoc cref="StateMachine" />
    /// </typeparam>
    public abstract class StatedStateMachine
        <
            TCurrentMachine, TCurrentMachineStateEnum,
            TStatedMachine, TStatedMachineStateEnum
        >
        : StateMachine<TStatedMachine, TStatedMachineStateEnum>
        where TStatedMachine : Machine
        where TCurrentMachine : StatedStateMachine
        <
            TCurrentMachine, TCurrentMachineStateEnum,
            TStatedMachine, TStatedMachineStateEnum
        >
        where TStatedMachineStateEnum : Enum
        where TCurrentMachineStateEnum : Enum
    {
        public Dictionary<TCurrentMachineStateEnum, StateMachine<TCurrentMachine, TCurrentMachineStateEnum>> m_states;
        public readonly TCurrentMachineStateEnum none_state_name;
        public TCurrentMachineStateEnum current_state;

        protected StatedStateMachine(
            Dictionary<TCurrentMachineStateEnum, StateMachine<TCurrentMachine, TCurrentMachineStateEnum>> states,
            TCurrentMachineStateEnum none_state_name,
            TStatedMachineStateEnum state_name = default) : base( state_name )
        {
            m_states = states;
            this.none_state_name = none_state_name;
            current_state = this.none_state_name;

            foreach (var state in m_states.Values)
            {
                state.m_stated_machine = this as TCurrentMachine;
            }
        }

        protected abstract void OnTranslateState(TCurrentMachineStateEnum to_state_name);

        public void TranslateState(TCurrentMachineStateEnum to_state_name)
        {
            OnTranslateState( to_state_name );
            m_states[current_state].ExitMachine( to_state_name );
            m_states[to_state_name].EnterMachine( current_state );
            current_state = to_state_name;
        }

        public new void EnterMachine(TStatedMachineStateEnum last_state_name)
        {
            base.EnterMachine( last_state_name );
            m_states[current_state].EnterMachine( none_state_name );
        }

        public new void ExitMachine(TStatedMachineStateEnum next_state_name)
        {
            base.ExitMachine( next_state_name );
            m_states[current_state].ExitMachine( none_state_name );
        }

        public new void UpdateMachine()
        {
            base.UpdateMachine();
            m_states[current_state].UpdateMachine();
        }

    }
}

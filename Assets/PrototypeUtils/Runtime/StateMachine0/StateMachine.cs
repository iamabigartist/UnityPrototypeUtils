using System;
namespace PrototypeUtils.StateMachine0
{

#region Controlling Shell balala

// public abstract class Machine<TControllingMachine> : Machine
//     where TControllingMachine : Machine
// {
//     Machine<Machine<TControllingMachine>>[] StateMachines;
//     Machine<Machine<TControllingMachine>> CurStateMachine;
//     TControllingMachine ControllingMachine;
//
//     public void Update()
//     {
//         CurStateMachine.Update( this );
//     }
//     protected abstract void OnUpdate();
//     public abstract void Update(TControllingMachine machine);
// }

/// <summary>
///     <para>A shell, the outermost layer of a whole machine.</para>
///     <para>Provide the bottom external actions and the top operations</para>
/// </summary>
// public abstract class StateMachineShell<TInstance> : Machine
//     where TInstance : StateMachineShell<TInstance> { }
//
// public abstract class StateMachineNode<TControlling> : Machine { }
//
// public abstract class StateMachineNode<TInstance, TControlling> : Machine
//     where TInstance : StateMachineNode<TInstance, TControlling>
//     where TControlling : Machine
// {
//     TControlling controlling_machine;
//     (StateMachineNode<TInstance>[] list, StateMachineNode<TInstance> current) state;
//
//     public void Update()
//     {
//         if (state != default) { state.current.Update(); }
//
//         NodeUpdate();
//     }
//
//     protected abstract void NodeUpdate();
//
// }

#endregion
#region MachineBehaviour balala

    // public abstract class Machine { }
    // public abstract class NoneMachine : Machine { }
    // public abstract class MachineBehaviour<TParams>
    // {
    //     protected (MachineBehaviour<TParams>[] enumeration, MachineBehaviour<TParams> current) state;
    //     protected MachineBehaviour<TParams> controlling_machine;
    //     protected virtual void CurAct(TParams parameters) { }
    //     void Act(TParams parameters)
    //     {
    //         state.current.Act( parameters );
    //         CurAct( parameters );
    //     }
    //     public static implicit operator Action<TParams>(MachineBehaviour<TParams> behaviour)
    //     {
    //         return behaviour.Act;
    //     }
    // }
    // public abstract class MachineBehaviour
    // {
    //     protected (MachineBehaviour[] enumeration, MachineBehaviour current) state;
    //     protected MachineBehaviour controlling_machine;
    //     protected virtual void CurAct() { }
    //     public void Act()
    //     {
    //         state.current.Act();
    //         CurAct();
    //     }
    //     public static implicit operator Action(MachineBehaviour behaviour)
    //     {
    //         return behaviour.Act;
    //     }
    // }

// public abstract class MachineBehaviour<TParams, TReturn>
// {
//     delegate TReturn A(TParams aa);
//     protected (MachineBehaviour<TParams, TReturn>[] enumeration, MachineBehaviour<TDelegate> current) state;
//     protected MachineBehaviour<TParams, TReturn> controlling_machine;
//
//     protected event Func<int> ddel11;
//     protected Action ddel1;
//
//     protected virtual void CurAct(TParams parameters) { }
//     void Act(TParams parameters)
//     {
//         state.current.Act( parameters );
//         CurAct( parameters );
//
//     }
//     public static implicit operator Action<TParams>(MachineBehaviour<TParams> behaviour)
//     {
//         return behaviour.Act;
//     }
// }

    //
    // public class DebugMachine : Machine
    // {
    //     public class UpdateBehaviour : MachineBehaviour
    //     {
    //         string log;
    //         protected override void CurAct()
    //         {
    //             Debug.Log( log );
    //         }
    //         public UpdateBehaviour(string log)
    //         {
    //             Action<string> a = (s) => { };
    //             a( "" );
    //             this.log = log;
    //         }
    //     }
    //     public Action Update;
    //     public DebugMachine()
    //     {
    //         Update = new UpdateBehaviour( "Log of Update." );
    //         Update1 = new("Log of Update.");
    //     }
    //
    //     public UpdateBehaviour Update1;
    // }

#endregion
#region InterfaceBased

    public interface IMachineShell { }
    public interface IMachine<TIMachineShell>
        where TIMachineShell : IMachineShell { }
    public interface IState<TIMachineShell> : IMachine<TIMachineShell>
        where TIMachineShell : IMachineShell { }

#region Example

    public interface ILogShell : IMachineShell
    {
        void UpdateLog();
        void AddElement();
        void ResetElement();
        event Action<string> Log;
    }

#endregion

#endregion



}

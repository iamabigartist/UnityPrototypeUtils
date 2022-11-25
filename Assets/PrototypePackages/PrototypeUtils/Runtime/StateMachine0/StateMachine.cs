namespace PrototypePackages.PrototypeUtils.Runtime.StateMachine0
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
#region New

    // public abstract class StateMachine { }
    //
    // public abstract class StateMachine<TShellMachine> : StateMachine
    //     where TShellMachine : StateMachine { }
    //
    // public abstract class StateMachine<TShellMachine, TLowerMachine> : StateMachine<TShellMachine>
    //     where TShellMachine : StateMachine
    //     where TLowerMachine : StateMachine<TShellMachine>
    // {
    //     protected StateManager<TShellMachine> m_state_manager;
    //     protected TShellMachine m_shell;
    //     protected TLowerMachine m_lower_machine;
    //
    //     public void AssignShell(TShellMachine shell)
    //     {
    //         m_shell = shell;
    //     }
    //     public void AssignLowerMachine(TLowerMachine lower_machine)
    //     {
    //         m_lower_machine = lower_machine;
    //     }
    //
    //     public abstract void OnEnable();
    //     public abstract void OnDisable();
    //     public abstract void OnUpdate();
    //
    // }
    //
    // public class StateManager<TShellMachine>
    //     where TShellMachine : StateMachine
    // {
    //     protected StateMachine<TShellMachine, StateMachine<TShellMachine>>[] States;
    //     protected StateMachine<TShellMachine, StateMachine<TShellMachine>> CurrentState;
    //     public void AssignShell(TShellMachine shell)
    //     {
    //         foreach (var state in States)
    //         {
    //             state.AssignShell( shell );
    //         }
    //     }
    //     public void AssignLowerMachine(StateMachine<TShellMachine> current_machine)
    //     {
    //         foreach (var state in States)
    //         {
    //             state.AssignLowerMachine( current_machine );
    //         }
    //     }
    //
    // }
#region Onelevel

//     public abstract class StateMachine
// {
// }
//
//     public abstract class StateMachine<TShellMachine> : StateMachine
//         where TShellMachine : StateMachine
//     {
//         protected StateManager<TShellMachine> m_state_manager;
//         protected TShellMachine m_shell;
//
//         public void AssignShell(TShellMachine shell)
//         {
//             m_shell = shell;
//         }
//
//         public abstract void OnEnable();
//         public abstract void OnDisable();
//         public abstract void OnUpdate();
//     }
//
//     public class StateManager<TShellMachine>
//         where TShellMachine : StateMachine
//     {
//         protected StateMachine<TShellMachine>[] States;
//         protected StateMachine<TShellMachine> CurrentState;
//         public void AssignShell(TShellMachine shell)
//         {
//             foreach (var state in States)
//             {
//                 state.AssignShell( shell );
//             }
//         }
//
//     }

#endregion
#region Node

    // public abstract class Machine
    // {
    //     public abstract void OnMachineEnter(string last_state_name);
    //     public abstract void OnMachineExit(string next_state_name);
    //     public abstract void OnMachineUpdate();
    // }
    // public abstract class NoneMachine : Machine { }
    //
    // /// <summary>
    // ///     A machine used as a state of other machines
    // /// </summary>
    // /// <typeparam name="TStatedMachine">The controlled machine that use this machine as a state</typeparam>
    // public abstract class StateMachine<TStatedMachine> : Machine
    //     where TStatedMachine : Machine
    // {
    //     public readonly string state_name;
    //     public TStatedMachine m_stated_machine;
    //     protected StateMachine(string state_name) { this.state_name = state_name; }
    //     public virtual void OnStateEnter(string last_state_name)
    //     {
    //         OnMachineEnter( last_state_name );
    //     }
    //     public virtual void OnStateExit(string next_state_name)
    //     {
    //         OnMachineExit( next_state_name );
    //     }
    //     public virtual void OnMachineUpdate()
    //     {
    //         OnMachineUpdate();
    //     }
    // }
    //
    // /// <summary>
    // ///     <para>From <see cref="StateMachine{TStatedMachine}" />: <inheritdoc cref="StateMachine{TStatedMachine}" /></para>
    // ///     <para>At the same time, a machine that use other machine as states.</para>
    // /// </summary>
    // /// <typeparam name="TCurrentMachine">
    // ///     The real type of this machine, used to tell the states what type of machine they are controlling.
    // /// </typeparam>
    // /// <typeparam name="TStatedMachine">
    // ///     <inheritdoc cref="StateMachine" />
    // /// </typeparam>
    // public abstract class StatedStateMachine<TCurrentMachine, TStatedMachine> : StateMachine<TStatedMachine>
    //     where TStatedMachine : Machine
    //     where TCurrentMachine : StatedStateMachine<TCurrentMachine, TStatedMachine>
    // {
    //     public Dictionary<string, StateMachine<TCurrentMachine>> m_states;
    //     public string current_state_name;
    //
    //     protected StatedStateMachine(Dictionary<string, StateMachine<TCurrentMachine>> states, string state_name = null) : base( state_name )
    //     {
    //         m_states = states;
    //         current_state_name = string.Empty;
    //
    //         foreach (var state in m_states.Values)
    //         {
    //             state.m_stated_machine = this as TCurrentMachine;
    //         }
    //     }
    //
    //     public void TranslateState(string to_state_name)
    //     {
    //         m_states[current_state_name].OnStateExit( to_state_name );
    //         m_states[to_state_name].OnStateEnter( current_state_name );
    //         current_state_name = to_state_name;
    //     }
    //
    //     public sealed override void OnStateEnter(string last_state_name)
    //     {
    //         m_states[current_state_name].OnStateEnter( string.Empty );
    //         OnMachineEnter( last_state_name );
    //     }
    //
    //     public sealed override void OnStateExit(string next_state_name)
    //     {
    //         m_states[current_state_name].OnStateExit( string.Empty );
    //         OnMachineExit( next_state_name );
    //     }
    //
    //     public sealed override void OnMachineUpdate()
    //     {
    //         m_states[current_state_name].OnMachineUpdate();
    //         OnMachineUpdate();
    //     }
    //
    // }

#endregion

#region Example

    // public class AppleStory : StatedStateMachine<AppleStory, NoneMachine>
    // {
    //     public string story_text;
    //     public event Action<string> DrawStoryTextGUI;
    //     public event Func<string, bool> DrawButton;
    //     public event Action DrawDogStatesViewer;
    //     public event Action<int> GiveTheMountainApples;
    //     public AppleStory() : base( new()
    //     {
    //         { "Chapter1", new Story1( "Chapter1" ) },
    //         { "Chapter2", new Story2( "Chapter2" ) }
    //     } ) { }
    //     public override void OnMachineEnter(string last_state_name) { }
    //     public override void OnMachineExit(string next_state_name) { }
    //     public override void OnMachineUpdate()
    //     {
    //         DrawStoryTextGUI( story_text );
    //         DrawDogStatesViewer();
    //         EditorGUILayout.BeginHorizontal();
    //         var turn_back = DrawButton( "<" );
    //         var turn_next = DrawButton( ">" );
    //         EditorGUILayout.EndHorizontal();
    //     }
    // }
    //
    // public class Story1 : StatedStateMachine<Story1, AppleStory>
    // {
    //     public string story_1_summary;
    //     public string story_1_detail;
    //     public Story1(string state_name) : base( new()
    //     {
    //         { "Start", new Story1_1( "Start" ) },
    //         { "End", new Story1_2( "End" ) }
    //     }, state_name ) { }
    //     public override void OnMachineEnter(string last_state_name) { throw new NotImplementedException(); }
    //     public override void OnMachineExit(string next_state_name) { throw new NotImplementedException(); }
    //     public override void OnMachineUpdate()
    //     {
    //         m_stated_machine.story_text = $"\t{story_1_summary}\n\n\t{story_1_detail}";
    //     }
    // }
    //
    // public class Story1_1 : StateMachine<Story1>
    // {
    //     public string story_1_1_detail;
    //     public override void OnMachineEnter(string last_state_name) { }
    //     public override void OnMachineExit(string next_state_name) { }
    //     public override void OnMachineUpdate()
    //     {
    //         m_stated_machine.story_1_detail = story_1_1_detail;
    //     }
    //     public Story1_1(string state_name) : base( state_name ) { }
    // }
    // public class Story1_2 : StateMachine<Story1>
    // {
    //     public string story_1_2_detail;
    //     public override void OnMachineEnter(string last_state_name) { }
    //     public override void OnMachineExit(string next_state_name) { }
    //     public override void OnMachineUpdate()
    //     {
    //         m_stated_machine.story_1_detail = story_1_2_detail;
    //     }
    //     public Story1_2(string state_name) : base( state_name ) { }
    // }
    //
    // public class Story2 : StateMachine<AppleStory>
    // {
    //     public override void OnMachineEnter(string last_state_name) { }
    //     public override void OnMachineExit(string next_state_name) { }
    //     public override void OnMachineUpdate() { }
    //     public Story2(string state_name) : base( state_name ) { }
    // }

#endregion

#endregion



}

// using System;
// using System.Collections.Generic;
// using UnityEngine;
// using VivoxPlayground.MyVivoxFramework.Util;
// namespace VivoxPlayground.MyVivoxFramework.PublicChatManagement
// {
//     public interface IChatter
//     {
//         string user_vivox_account_uri { get; }
//         string character_name { get; }
//     }
//
//     public enum ChatMode
//     {
//         /// <summary>
//         ///     自由讨论
//         /// </summary>
//         Free,
//
//         /// <summary>
//         ///     轮流发言
//         /// </summary>
//         Turn,
//
//         /// <summary>
//         ///     报名发言
//         /// </summary>
//         Enroll,
//
//         None
//     }
//
//     public class PublicChatMachine<TChatter> :
//         StatedStateMachine
//         <
//             PublicChatMachine<TChatter>, ChatMode,
//             NoneMachine, NoneState
//         >
//         where TChatter : class, IChatter
//     {
//
//     #region Config
//
//         public readonly float enroll_cool_down_ms;
//         public readonly float chat_once_time_length_ms;
//
//     #endregion
//
//     #region State
//
//     #region MainChatter
//
//         public TChatter cur_main_chatter;
//
//         public PeriodTimer main_chatter_timer;
//
//         public bool MainChatterTimeOut => main_chatter_timer.IsTimeUp();
//
//     #endregion
//
//     #endregion
//
//     #region Data
//
//     #region Chatters
//
//         public string[] character_name_chat_order_table;
//         /// <summary>
//         /// A external dict reference to get the cur_chatters.
//         /// </summary>
//         public Dictionary<string, TChatter> cur_chatters;
//
//     #endregion
//
//     #endregion
//
//     #region Process
//
//         void ResetChannel()
//         {
//             SetMuteAll( true );
//             cur_main_chatter = null;
//         }
//
//     #endregion
//
//     #region Util
//
//         /// <summary>
//         ///     if the chat states loop sequentially, use this to get next state of the machine, now used for testing and display
//         ///     when it is without a call for specific state change.
//         /// </summary>
//         public ChatMode GetNextState()
//         {
//             return current_state switch
//             {
//                 ChatMode.Free   => ChatMode.Turn,
//                 ChatMode.Turn   => ChatMode.Enroll,
//                 ChatMode.Enroll => ChatMode.Free,
//                 _               => ChatMode.None
//             };
//         }
//
//         public void SetMuteAll(bool is_mute)
//         {
//
//             string text = $"cur_chatters: {cur_chatters.ToListString()}";
//             Log( text );
//
//             foreach (var chatter in cur_chatters.Values)
//             {
//                 SetSingleChatterMute( (chatter, is_mute) );
//             }
//         }
//
//     #endregion
//
//     #region Interface
//
//     #region MachineEnrty
//
//         public PublicChatMachine(
//             float enroll_cool_down_ms,
//             float chat_once_time_length_ms,
//             string[] character_order_table,
//             bool round_auto_restart,
//             Dictionary<string, TChatter> cur_chatters,
//             Action<string> Log,
//             Action<(TChatter chatter, bool is_mute)> SetSingleChatterMute,
//             Action<(ChatMode prev_chatMode, ChatMode new_chatMode)> StateChangeNotification,
//             Action<TChatter> MainChatterSetNotification,
//             Action<TChatter> MainChatterRemovedNotification,
//             Action RoundEndNotification_Turn,
//             Action<TChatter> ChatterEnqueueNotification_Enroll,
//             Action<TChatter> ChatterDequeueNotification_Enroll,
//             Action<TChatter> AllowViceChatterNotification_Enroll)
//             : base(
//                 new()
//                 {
//                     { ChatMode.Free, new FreeChatState<TChatter>( ChatMode.Free ) },
//                     { ChatMode.Turn, new TurnChatState<TChatter>( ChatMode.Turn, round_auto_restart ) },
//                     { ChatMode.Enroll, new EnrollChatState<TChatter>( ChatMode.Enroll ) }
//                 }, ChatMode.None )
//         {
//             this.enroll_cool_down_ms = enroll_cool_down_ms;
//             this.chat_once_time_length_ms = chat_once_time_length_ms;
//             main_chatter_timer = new(this.chat_once_time_length_ms);
//
//             character_name_chat_order_table = character_order_table;
//             this.cur_chatters = cur_chatters;
//
//             this.Log = Log;
//             this.SetSingleChatterMute = SetSingleChatterMute;
//             this.StateChangeNotification = StateChangeNotification;
//             this.MainChatterSetNotification = MainChatterSetNotification;
//             this.MainChatterRemovedNotification = MainChatterRemovedNotification;
//             this.RoundEndNotification_Turn = RoundEndNotification_Turn;
//             this.ChatterEnqueueNotification_Enroll = ChatterEnqueueNotification_Enroll;
//             this.ChatterDequeueNotification_Enroll = ChatterDequeueNotification_Enroll;
//             this.AllowViceChatterNotification_Enroll = AllowViceChatterNotification_Enroll;
//         }
//
//         protected override void OnMachineEnter(NoneState last_state_name)
//         {
//             Log( "PCM OnMachineEnter" );
//             cur_main_chatter = null;
//             current_state = ChatMode.Free;
//         }
//         protected override void OnMachineExit(NoneState next_state_name)
//         {
//             Log( "PCM OnMachineExit" );
//         }
//         protected override void OnTranslateState(ChatMode to_state_name)
//         {
//             StateChangeNotification( (current_state, to_state_name) );
//             ResetChannel();
//         }
//         protected override void OnMachineUpdate()
//         {
//             if (cur_main_chatter != null)
//             {
//                 if (MainChatterTimeOut)
//                 {
//                     RemoveMainChatter();
//                 }
//             }
//         }
//
//     #endregion
//
//     #region Behaviour
//
//         //Need by the machine, provided from outside.
//     #region External
//
//         public Action<string> Log;
//
//         public Action<(TChatter chatter, bool is_mute)> SetSingleChatterMute;
//
//         public Action<(ChatMode prev_chatMode, ChatMode new_chatMode)> StateChangeNotification;
//
//         public Action<TChatter> MainChatterSetNotification;
//         public Action<TChatter> MainChatterRemovedNotification;
//
//         public Action RoundEndNotification_Turn;
//
//         public Action<TChatter> ChatterEnqueueNotification_Enroll;
//
//         public Action<TChatter> ChatterDequeueNotification_Enroll;
//
//         public Action<TChatter> AllowViceChatterNotification_Enroll;
//
//     #endregion
//
//     #region Internal
//
//     #region HigherMachine
//
//         public void RemoveMainChatter()
//         {
//             var removed_chatter = cur_main_chatter;
//             SetSingleChatterMute( (cur_main_chatter, true) );
//             cur_main_chatter = null;
//             MainChatterRemovedNotification( removed_chatter );
//         }
//
//         public void SetMainChatter(TChatter chatter)
//         {
//             cur_main_chatter = chatter;
//             main_chatter_timer.StartNewPeriod();
//             SetSingleChatterMute( (cur_main_chatter, true) );
//             MainChatterSetNotification( chatter );
//         }
//
//         //不应该由上层机器决定是否移除现在的主要聊天者？，上层机器只在
//         public void ChangeMainChatter(TChatter chatter)
//         {
//             if (cur_main_chatter != null)
//             {
//                 RemoveMainChatter();
//             }
//
//             SetMainChatter( chatter );
//         }
//
//     #endregion
//
//         //Need to be called from outside, may via messages.
//     #region Shell
//
//         // //BUGGED 2 公聊系统的列表同步应当以频道为准，而非登录。因此这两个函数应该作为频道加入participant的回调函数。//
//         // public void OnChatterEnterRoom(TChatter chatter)
//         // {
//         //     cur_chatters[chatter.character_name] = chatter;
//         // }
//         //
//         // public void OnChatterExitRoom(TChatter chatter)
//         // {
//         //     cur_chatters.Remove( chatter.character_name );
//         // }
//
//         public bool MainChatterSkipChatTime(TChatter target_chatter)
//         {
//             bool match = target_chatter == cur_main_chatter;
//
//             if (match) { RemoveMainChatter(); }
//
//             return match;
//         }
//
//         public Action StartNewRound_Turn;
//
//         public Action<TChatter> QueueUpChatter_Enroll;
//
//         public Action<TChatter> MainChatterAllowViceChatter_Enroll;
//
//     #endregion
//
//     #endregion
//
//     #endregion
//
//     #endregion
//
//     }
//
//     public class FreeChatState<TChatter>
//         : StateMachine<PublicChatMachine<TChatter>, ChatMode>
//         where TChatter : class, IChatter
//     {
//         public FreeChatState(ChatMode state_name) : base( state_name ) { }
//         protected override void OnMachineEnter(ChatMode last_state_name)
//         {
//             m_stated_machine.SetMuteAll( false );
//         }
//     }
//
//     public class TurnChatState<TChatter>
//         : StateMachine<PublicChatMachine<TChatter>, ChatMode>
//         where TChatter : class, IChatter
//     {
//         int cur_table_index;
//         bool rounding;
//         public bool auto_restart_round;
//         public bool query_exist_more_than_1_player => m_stated_machine.cur_chatters.Count > 1;
//
//         public TurnChatState(ChatMode state_name, bool auto_restart_round) : base( state_name )
//         {
//             this.auto_restart_round = auto_restart_round;
//         }
//
//         bool TryGetChatterByIndexName(int index, out TChatter chatter)
//         {
//             var cur_name =
//                 m_stated_machine.character_name_chat_order_table[index];
//             var find = m_stated_machine.cur_chatters.TryGetValue( cur_name, out chatter );
//
//             return find;
//         }
//
//         bool GetIndexNearestChatter(int index, out int name_index, out TChatter next_chatter)
//         {
//             for (; index < m_stated_machine.character_name_chat_order_table.Length; index++)
//             {
//                 if (TryGetChatterByIndexName( index, out var chatter ))
//                 {
//                     name_index = index;
//                     next_chatter = chatter;
//
//                     return true;
//                 }
//             }
//
//             name_index = cur_table_index;
//             next_chatter = null;
//
//             return false;
//         }
//
//         void SetNewTurnMainChatter(int index, TChatter chatter)
//         {
//             m_stated_machine.ChangeMainChatter( chatter );
//             cur_table_index = index;
//         }
//
//         bool NextTurn()
//         {
//             var round_continue =
//                 GetIndexNearestChatter( cur_table_index + 1, out var name_index, out var next_chatter );
//
//             if (round_continue)
//             {
//                 SetNewTurnMainChatter( name_index, next_chatter );
//             }
//             else
//             {
//                 RoundEnd();
//             }
//
//             return round_continue;
//         }
//
//         void StartNewRound()
//         {
//             rounding = true;
//             cur_table_index = -1;
//             NextTurn();
//         }
//
//         void RoundEnd()
//         {
//             rounding = false;
//             m_stated_machine.RoundEndNotification_Turn();
//         }
//
//         protected override void OnMachineEnter(ChatMode last_state_name)
//         {
//             m_stated_machine.StartNewRound_Turn += StartNewRound;
//             StartNewRound();
//         }
//         protected override void OnMachineExit(ChatMode next_state_name)
//         {
//             m_stated_machine.StartNewRound_Turn -= StartNewRound;
//             RoundEnd();
//         }
//         protected override void OnMachineUpdate()
//         {
//             //有几个问题，
//             //首先rounding true的情况下一定能够调用timeout吗？
//             //那么就要保证从 rounding false 转换到 true请况的时候, main chatter 需要被正常设置。
//             //其次，没有任何发言频繁 round end 的情况怎么解决？ A：通过检测当前玩家数量再开始新的一轮来决定。
//
//
//             if (!query_exist_more_than_1_player) return;
//
//             if (rounding)
//             {
//                 if (m_stated_machine.cur_main_chatter == null)
//                 {
//                     NextTurn();
//                 }
//             }
//             else
//             {
//                 if (auto_restart_round)
//                 {
//                     StartNewRound();
//                 }
//             }
//
//
//         }
//     }
//
//     public class EnrollChatState<TChatter>
//         : StateMachine<PublicChatMachine<TChatter>, ChatMode>
//         where TChatter : class, IChatter
//     {
//         public Queue<TChatter> chat_queue;
//         public List<TChatter> cur_vice_chatters;
//
//         void NextDequeue()
//         {
//             var next_chatter = chat_queue.Dequeue();
//             m_stated_machine.ChatterDequeueNotification_Enroll( next_chatter );
//             m_stated_machine.ChangeMainChatter( next_chatter );
//         }
//
//         public void OnQueueUpChatter(TChatter chatter)
//         {
//             chat_queue.Enqueue( chatter );
//             m_stated_machine.ChatterEnqueueNotification_Enroll( chatter );
//         }
//
//         public void AddViceChatters(TChatter chatter)
//         {
//             m_stated_machine.SetSingleChatterMute( (chatter, false) );
//             cur_vice_chatters.Add( chatter );
//         }
//
//         public void ClearViceChatters()
//         {
//             foreach (var vice_chatter in cur_vice_chatters)
//             {
//                 m_stated_machine.SetSingleChatterMute( (vice_chatter, true) );
//             }
//
//             cur_vice_chatters.Clear();
//         }
//
//         public void OnMainChatterAllowViceChatter(TChatter chatter)
//         {
//             AddViceChatters( chatter );
//             m_stated_machine.AllowViceChatterNotification_Enroll( chatter );
//         }
//
//         public void OnMainChatterRemoveNotification(TChatter chatter)
//         {
//             ClearViceChatters();
//         }
//
//         public EnrollChatState(ChatMode state_name) : base( state_name )
//         {
//             chat_queue = new();
//             cur_vice_chatters = new();
//         }
//
//         protected override void OnMachineEnter(ChatMode last_state_name)
//         {
//             m_stated_machine.QueueUpChatter_Enroll += OnQueueUpChatter;
//             m_stated_machine.MainChatterAllowViceChatter_Enroll += OnMainChatterAllowViceChatter;
//             m_stated_machine.MainChatterRemovedNotification += OnMainChatterRemoveNotification;
//         }
//
//         protected override void OnMachineExit(ChatMode next_state_name)
//         {
//             chat_queue.Clear(); //是否重置？如果不重置，代笔上次状态的一种保留，没有必要保留排队信息，或者有必要再删除此行。
//             m_stated_machine.QueueUpChatter_Enroll -= OnQueueUpChatter;
//             m_stated_machine.MainChatterAllowViceChatter_Enroll -= OnMainChatterAllowViceChatter;
//             m_stated_machine.MainChatterRemovedNotification -= OnMainChatterRemoveNotification;
//         }
//         protected override void OnMachineUpdate()
//         {
//             if (m_stated_machine.cur_main_chatter == null)
//             {
//                 if (chat_queue.Count > 0)
//                 {
//                     NextDequeue();
//                 }
//             }
//
//         }
//     }
// }

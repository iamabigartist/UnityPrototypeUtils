using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using PrototypePackages.PrototypeUtils.Runtime.StateMachine0;
using PrototypePackages.TimeUtil.Scripts;
namespace Examples.E5_StateController
{
    public interface IChatter
    {
        string user_name { get; }
        string character_name { get; }
        PeriodTimer chat_cooldown_timer { get; }
    }

    public class PublicChatSystem<TChatter>
        where TChatter : class, IChatter
    {

    #region Config

        public readonly float enroll_cool_down;
        public readonly float chat_once_time_length;

    #endregion

    #region State

        public enum ChatMode
        {
            /// <summary>
            ///     自由讨论
            /// </summary>
            Free,

            /// <summary>
            ///     轮流发言
            /// </summary>
            Turn,

            /// <summary>
            ///     报名发言
            /// </summary>
            Enroll
        }

        public class EnrollChatQueue : IChatterEnumerator<TChatter>
        {
            public Queue<TChatter> chat_queue;

        #region Enumerator

            object IEnumerator.Current => Current;
            public void Dispose() { }

        #endregion

        #region Interface

            public EnrollChatQueue() { chat_queue = new(); }

            public bool MoveNext()
            {
                return chat_queue.TryDequeue( out _ );
            }

            public void Reset()
            {
                chat_queue.Clear();
            }

            public TChatter Current => chat_queue.Peek();

            public void Enqueue(TChatter chatter)
            {
                chat_queue.Enqueue( chatter );
            }

        #endregion

        }

        public class CharacterNameOrderTableChatQueue : IChatterEnumerator<TChatter>
        {
            public string[] character_name_chat_order_table;
            public int cur_table_index;
            public Dictionary<string, TChatter> cur_chatters;

        #region Enumerator

            object IEnumerator.Current => Current;
            public void Dispose() { }

        #endregion

        #region Interface

            public CharacterNameOrderTableChatQueue(string[] character_name_chat_order_table)
            {
                this.character_name_chat_order_table = character_name_chat_order_table;
                cur_chatters = new();
                cur_table_index = 0;
            }

            public bool MoveNext()
            {
                var no_more_chatter = cur_table_index == character_name_chat_order_table.Length - 1;

                if (!no_more_chatter)
                {
                    cur_table_index++;
                }

                return no_more_chatter;
            }

            public void Reset()
            {
                cur_table_index = 0;
            }

            public TChatter Current
            {
                get
                {
                    cur_chatters.TryGetValue( character_name_chat_order_table[cur_table_index], out var cur_chatter );

                    return cur_chatter;
                }
            }

            public bool CurrentChatterAvailable => Current != null;

        #endregion

        }

        ChatMode cur_chat_mode;
        TChatter cur_main_chatter;
        float main_chatter_start_time;
        Timer main_chatter_timer;
        EnrollChatQueue enroll_chat_queue;
        CharacterNameOrderTableChatQueue character_name_order_table_chat_queue;

    #endregion

    #region Data

    #endregion

    #region Process

        void FreeModeInit() { }
        void FreeModeUpdate() { }
        void FreeModeTerminate() { }

        void TurnModeInit() { }
        void TurnModeUpdate() { }
        void TurnModeTerminate() { }

        void EnrollModeInit() { }
        void EnrollModeUpdate() { }
        void EnrollModeTerminate() { }

    #endregion

    #region Interface

    #region Entry

        public PublicChatSystem(SetSingleChatter SetMute, Action<string> Log, UpdateStateCallback StateChangeNotification, string[] character_order_table)
        {
            cur_chat_mode = ChatMode.Free;
            this.SetMute = SetMute;
            this.Log = Log;
            this.StateChangeNotification = StateChangeNotification;
            enroll_chat_queue = new();
            character_name_order_table_chat_queue = new(character_order_table);
            cur_main_chatter = null;
        }

        public void Update()
        {
            switch (cur_chat_mode)
            {
                case ChatMode.Free:
                    FreeModeUpdate();

                    break;

                case ChatMode.Turn:
                    TurnModeUpdate();

                    break;

                case ChatMode.Enroll:
                    EnrollModeUpdate();

                    break;
            }
        }

    #endregion

    #region Action

    #region External

        public delegate void SetSingleChatter(TChatter chatter, bool is_mute);
        public event SetSingleChatter SetMute;
        public event Action<string> Log;
        public delegate void UpdateStateCallback(ChatMode prev_chatMode, ChatMode new_chatMode);
        public event UpdateStateCallback StateChangeNotification;

    #endregion

    #region Internal

        public void TerminateCurState()
        {
            switch (cur_chat_mode)
            {
                case ChatMode.Free:
                    FreeModeTerminate();

                    break;

                case ChatMode.Turn:
                    TurnModeTerminate();

                    break;

                case ChatMode.Enroll:
                    EnrollModeTerminate();

                    break;
            }
        }
        public void InitToState(ChatMode mode)
        {
            cur_chat_mode = mode;

            switch (cur_chat_mode)
            {
                case ChatMode.Free:
                    FreeModeTerminate();

                    break;

                case ChatMode.Turn:
                    TurnModeTerminate();

                    break;

                case ChatMode.Enroll:
                    EnrollModeTerminate();

                    break;
            }
        }

        public void ResetToState(ChatMode new_mode)
        {
            TerminateCurState();

        }

        public void QueueUpUser(TChatter chatter)
        {
            if (cur_chat_mode != ChatMode.Enroll)
            {
                Log( $"Currently not in the state {nameof(ChatMode.Enroll)}, can not queue up." );

                return;
            }
        }

    #endregion

    #endregion

    #endregion

    }



    public class PublicChatMachine<TChatter> :
        StatedStateMachine
        <
            PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode,
            NoneMachine, NoneState
        >
        where TChatter : class, IChatter
    {


    #region Config

        public readonly float enroll_cool_down_ms;
        public readonly float chat_once_time_length_ms;

    #endregion

    #region State

        public enum ChatMode
        {
            /// <summary>
            ///     自由讨论
            /// </summary>
            Free,

            /// <summary>
            ///     轮流发言
            /// </summary>
            Turn,

            /// <summary>
            ///     报名发言
            /// </summary>
            Enroll,

            None
        }

    #region MainChatter

        public TChatter cur_main_chatter;

        public PeriodTimer main_chatter_timer;

        public bool MainChatterTimeOut => main_chatter_timer.IsTimeUp();

    #endregion

    #endregion

    #region Data

    #region Chatters

        public string[] character_name_chat_order_table;

        public List<TChatter> cur_chatters;

    #endregion

    #endregion

    #region Process

        public void ResetChannel()
        {
            SetMuteAll( true );
            cur_main_chatter = null;
        }

        public void RemoveMainChatter()
        {
            if (cur_main_chatter != null)
            {
                SetSingleChatterMute( (cur_main_chatter, true) );
                cur_main_chatter = null;
            }
        }

        public void ChangeMainChatter(TChatter chatter)
        {
            RemoveMainChatter();
            cur_main_chatter = chatter;
            main_chatter_timer.StartNewPeriod();
            MainChatterChangeNotification( chatter );
        }

    #endregion

    #region Util

        public void SetMuteAll(bool is_mute)
        {
            foreach (var chatter in cur_chatters)
            {
                SetSingleChatterMute( (chatter, is_mute) );
            }
        }

    #endregion

    #region Interface

    #region MachineEnrty

        public PublicChatMachine(
            float enroll_cool_down_ms,
            float chat_once_time_length_ms,
            string[] character_order_table,
            Action<string> Log,
            Action<(TChatter chatter, bool is_mute)> SetSingleChatterMute,
            Action<(ChatMode prev_chatMode, ChatMode new_chatMode)> StateChangeNotification)
            : base(
                new()
                {
                    { ChatMode.Free, new FreeChatState<TChatter>( ChatMode.Free ) },
                    { ChatMode.Turn, new TurnChatState<TChatter>( ChatMode.Turn ) },
                    { ChatMode.Enroll, new EnrollChatState<TChatter>( ChatMode.Enroll ) }
                }, ChatMode.None )
        {
            this.enroll_cool_down_ms = enroll_cool_down_ms;
            this.chat_once_time_length_ms = chat_once_time_length_ms;
            main_chatter_timer = new(this.chat_once_time_length_ms);

            character_name_chat_order_table = character_order_table;
            cur_chatters = new();

            this.Log = Log;
            this.SetSingleChatterMute = SetSingleChatterMute;
            this.StateChangeNotification = StateChangeNotification;

        }

        protected override void OnMachineEnter(NoneState last_state_name)
        {
            cur_main_chatter = null;
            current_state = ChatMode.Free;
        }
        protected override void OnMachineExit(NoneState next_state_name) { }
        protected override void OnMachineUpdate() { }
        protected override void OnTranslateState(ChatMode to_state_name)
        {
            ResetChannel();
        }

    #endregion

    #region Behaviour

    #region External

        public Action<string> Log;
        public Action<(TChatter chatter, bool is_mute)> SetSingleChatterMute;
        public Action<(ChatMode prev_chatMode, ChatMode new_chatMode)> StateChangeNotification;

        public Action<TChatter> MainChatterChangeNotification;

        public Action RoundEndNotification;
        public Action<TChatter> ChatterEnQueueNotification;
        public Action<TChatter> ChatterDequeueNotification;

    #endregion

    #region Internal

        public Func<TChatter, bool> ChatterEnterRoom;
        public Func<TChatter, bool> ChatterExitRoom;
        public Func<TChatter, bool> QueueUpChatter;
        public Func<TChatter, bool> MainChatterAllowViceChatter;
        public Action MainChatterSkipChatTime;

    #endregion

    #endregion

    #endregion

    }

    public class FreeChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : class, IChatter
    {
        public FreeChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name ) { }
        protected override void OnMachineEnter(PublicChatMachine<TChatter>.ChatMode last_state_name)
        {
            m_stated_machine.SetMuteAll( false );
        }
    }

    public class TurnChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : class, IChatter
    {
        int cur_table_index;
        bool rounding;
        public bool auto_restart_round;
        public TurnChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name ) { }


        bool TryGetChatterByIndexName(int index, out TChatter chatter)
        {
            var cur_name =
                m_stated_machine.character_name_chat_order_table[index];
            var cur_chatter = m_stated_machine.cur_chatters.Find( cur_chatter => cur_chatter.character_name == cur_name );

            var find = cur_chatter != null;

            chatter = cur_chatter;

            return find;
        }

        bool GetIndexNearestChatter(int index, out int name_index, out TChatter next_chatter)
        {
            for (; index < m_stated_machine.character_name_chat_order_table.Length; index++)
            {
                if (TryGetChatterByIndexName( index, out var chatter ))
                {
                    name_index = index;
                    next_chatter = chatter;

                    return true;
                }
            }

            name_index = cur_table_index;
            next_chatter = null;

            return false;
        }

        void SetNewTurnMainChatter(int index, TChatter chatter)
        {
            m_stated_machine.ChangeMainChatter( chatter );
            cur_table_index = index;
        }

        bool NextTurn()
        {
            var round_end =
                GetIndexNearestChatter( cur_table_index + 1, out var name_index, out var next_chatter );

            if (!round_end)
            {
                SetNewTurnMainChatter( name_index, next_chatter );
            }

            return round_end;
        }

        void StartNewRound()
        {
            rounding = true;
            cur_table_index = 0;

            var ever_exist =
                GetIndexNearestChatter( cur_table_index, out var name_index, out var first_chatter );

            if (ever_exist)
            {
                SetNewTurnMainChatter( name_index, first_chatter );
            }
            else
            {
                RoundEnd();
                //Do nothing.
            }
        }

        void RoundEnd()
        {
            rounding = false;
            m_stated_machine.RoundEndNotification();
        }

        protected override void OnMachineEnter(PublicChatMachine<TChatter>.ChatMode last_state_name)
        {
            StartNewRound();
        }
        protected override void OnMachineExit(PublicChatMachine<TChatter>.ChatMode next_state_name)
        {
            RoundEnd();
        }
        protected override void OnMachineUpdate()
        {
            if (rounding)
            {
                if (m_stated_machine.MainChatterTimeOut)
                {
                    var round_end = NextTurn();

                    if (round_end)
                    {
                        if (auto_restart_round)
                        {
                            StartNewRound();
                        }
                        else
                        {
                            RoundEnd();
                        }
                    }
                }
            }

        }
    }

    public class EnrollChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : class, IChatter
    {
        public Queue<TChatter> chat_queue;
        public List<TChatter> cur_vice_chatters;


        public void AddViceChatters(TChatter chatter)
        {
            m_stated_machine.SetSingleChatterMute( (chatter, false) );
            cur_vice_chatters.Add( chatter );
        }

        public void ClearViceChatters()
        {
            foreach (var vice_chatter in cur_vice_chatters)
            {
                m_stated_machine.SetSingleChatterMute( (vice_chatter, true) );
            }

            cur_vice_chatters.Clear();
        }

        public bool OnMainChatterAllowViceChatter(TChatter chatter)
        {
            var success = chat_queue.Contains( chatter );

            if (success) { AddViceChatters( chatter ); }

            return success;
        }

        public EnrollChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name )
        {
            chat_queue = new();
            cur_vice_chatters = new();
        }

        protected override void OnMachineEnter(PublicChatMachine<TChatter>.ChatMode last_state_name)
        {
            m_stated_machine.MainChatterAllowViceChatter += OnMainChatterAllowViceChatter;
        }

        protected override void OnMachineExit(PublicChatMachine<TChatter>.ChatMode next_state_name)
        {
            m_stated_machine.MainChatterAllowViceChatter -= OnMainChatterAllowViceChatter;
        }
    }
}

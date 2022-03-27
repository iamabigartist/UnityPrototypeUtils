using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using PrototypeUtils;
using PrototypeUtils.StateMachine0;
namespace Examples.E5_StateController
{
    public interface IChatter
    {
        string user_name { get; }
        string character_name { get; }
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
        where TChatter : IChatter
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

        TChatter cur_main_chatter;

        PeriodTimer main_chatter_timer;

    #endregion

    #endregion

    #region Data

    #region Chatters

        public string[] character_name_chat_order_table;

        public Dictionary<string, TChatter> cur_chatters;

    #endregion

    #endregion

    #region Interface

    #region MachineEnrty

        public PublicChatMachine(float enroll_cool_down_ms, float chat_once_time_length_ms) : base(
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
        }

        protected override void OnMachineEnter(NoneState last_state_name) { }
        protected override void OnMachineExit(NoneState next_state_name) { }
        protected override void OnMachineUpdate() { }

    #endregion

    #region Behaviour

        public delegate void UpdateStateCallback(ChatMode prev_chatMode, ChatMode new_chatMode);

    #region External

        public event Action<string> Log;
        public event Action<(TChatter chatter, bool is_mute)> SetSingleChatterMute;
        public event Action<(ChatMode prev_chatMode, ChatMode new_chatMode)> StateChangeNotification;

    #endregion

    #region Internal

        public Action<TChatter> QueueUpChatter;
        public Action<TChatter> MainChatterAllowViceChatter;
        public Action MainChatterSkipChatTime;

    #endregion

    #endregion

    #endregion

    }

    public class FreeChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : IChatter
    {
        public FreeChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name ) { }
    }

    public class TurnChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : IChatter
    {
        public int cur_table_index;
        public TurnChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name ) { }
    }

    public class EnrollChatState<TChatter>
        : StateMachine<PublicChatMachine<TChatter>, PublicChatMachine<TChatter>.ChatMode>
        where TChatter : IChatter
    {
        public Queue<TChatter> chat_queue;
        public EnrollChatState(PublicChatMachine<TChatter>.ChatMode state_name) : base( state_name ) { }
    }
}

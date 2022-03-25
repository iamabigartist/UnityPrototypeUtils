using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Timers;
namespace Examples.E5_StateController
{
    public interface IChatterEnumerator<TChatter> : IEnumerator<TChatter> where TChatter : class, IChatter { }
    public interface IChatter
    {
        string user_name { get; }
        string character_name { get; }
    }

    [SuppressMessage( "ReSharper", "PossibleNullReferenceException" )]
    public class PublicChatSystem<TChatter> where TChatter : class, IChatter
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

        void TurnModeInit()
        {
        }
        void TurnModeUpdate()
        {
        }
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
}

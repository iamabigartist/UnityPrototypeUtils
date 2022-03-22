using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        ChatMode chat_mode;

    #endregion

    #region Data

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

        EnrollChatQueue enroll_chat_queue;
        CharacterNameOrderTableChatQueue character_name_order_table_chat_queue;

    #endregion

    #region Process

        void FreeModeUpdate() { }
        void TurnModeUpdate() { }
        void EnrollModeUpdate() { }

    #endregion

    #region Interface

    #region Entry

        public PublicChatSystem(SetSingleChatter SetMute, Action<string> Log, UpdateState StateChangeNotification)
        {
            chat_mode = ChatMode.Free;
            this.SetMute = SetMute;
            this.Log = Log;
            this.StateChangeNotification = StateChangeNotification;
        }

        public void Update()
        {
            switch (chat_mode)
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

    #region ExternalBehaviour

        public delegate void SetSingleChatter(TChatter chatter, bool is_mute);
        public event SetSingleChatter SetMute;
        public event Action<string> Log;
        public delegate void UpdateState(ChatMode prev_chatMode, ChatMode new_chatMode);
        public event UpdateState StateChangeNotification;

    #endregion

    #region Behaviour

        public void ChangeState() { }

        public void QueueUpUser(TChatter chatter)
        {
            if (chat_mode != ChatMode.Enroll)
            {
                Log( $"Currently not in the state {nameof(ChatMode.Enroll)}, can not queue up." );

                return;
            }
        }

    #endregion

    #endregion

    }
}

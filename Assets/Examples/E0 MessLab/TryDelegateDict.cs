using System;
using System.Collections.Generic;
using System.Diagnostics;
using PrototypePackages.PrototypeUtils;
using PrototypePackages.TimeUtils;
using UnityEngine;
using Debug = UnityEngine.Debug;
namespace Examples.E0_MessLab
{
    public class DelegateBinder
    {
        public Delegate value;
        public Delegate destination;
        public void Bind() { }
    }
    public class MachineMeta { }


    public class DelegateContainer
    {
        public Type type;
        public Delegate value;
        public static DelegateContainer FromDelegate<T>(T value) where T : Delegate
        {
            DelegateContainer container = new DelegateContainer();
            container.type = typeof(T);
            container.value = value;

            return container;
        }
        public static object ConvertDelegate(Delegate value)
        {
            return value;
        }
    }

    public class TryDelegateDict : MonoBehaviour
    {
        public event Action act1;
        public event Action act2;
        Dictionary<MulticastDelegate, MulticastDelegate> table;


        void BindActs()
        {
            var de1 = new DelegateContainer()
            {
                type = typeof(Action),
                value = new Action( () => { Debug.Log( "De1" ); } )
            };

            var de2 = new DelegateContainer()
            {
                type = typeof(Action),
                value = new Action( () => { Debug.Log( "De2" ); } )
            };
            var type = de2.type;
            dynamic value1 = de1.value;
            (value1 as Action).Invoke();

            dynamic value2 = de2.value;
            (value2 as Action).Invoke();

            de1.value = Delegate.Combine( de1.value, de2.value );
            (de1.value as Action).Invoke();
        }

        int times;
        TaskWatch task_watch;
        event Action m_action;
        Delegate m_action_Delegate;
        Delegate m_handler_Delegate;
        void Handler()
        {
            Debug.Log( "Handler Execute" );
        }

        void Start()
        {
            table = new();
            BindActs();
            m_action_Delegate = m_action;
            m_handler_Delegate = (Action)Handler;
            task_watch = new(() =>
            {
                m_action_Delegate = Delegate.Combine( m_action_Delegate, m_handler_Delegate );
                m_action_Delegate = Delegate.Remove( m_action_Delegate, m_handler_Delegate );
            }, Util.MsToTicks( 50.0 ));
        }

        public void Update()
        {
            task_watch.Run();
        }

        void OnGUI()
        {
            UIUtils.ScaleIMGUI( (5f, 5f).V() );
            GUILayout.Label( $"{nameof(task_watch.time_per_msecond)}: {task_watch.time_per_msecond}" );
            GUILayout.Label( $"{nameof(task_watch.msecond_per_time)}: {task_watch.msecond_per_time}" );
        }



    }
}

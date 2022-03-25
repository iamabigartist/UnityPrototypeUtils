using System;
using UnityEngine;
namespace Examples.E5_StateController
{
    public class StateMachine
    {
        static float inner_a()
        {
            return 1.0f;
        }
        protected float inner_field;
        public class SSS
        {
            protected StateMachine reference;
            void A()
            {
                Debug.Log( inner_a() );
            }
            public void Method1()
            {
                Debug.Log( reference.inner_field );
            }

            public void InnerSteal(StateMachine stateMachine)
            {
                Debug.Log( stateMachine.inner_field );
            }
        }
    }
    public class Thief : StateMachine.SSS
    {

        void Method2()
        {
            Method1();
            InnerSteal( new() );
        }
    }

    public class A<T>{}
}

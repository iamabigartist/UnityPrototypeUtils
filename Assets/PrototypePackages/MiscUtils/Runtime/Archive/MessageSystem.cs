// using System.Collections.Generic;
// using com.unity.MiGuGame;
// using Unity.Collections;
// using Unity.Entities;
// using Unity.NetCode;
// namespace VivoxPlayground.MyVivoxFramework.Lab
// {
//     // [AlwaysUpdateSystem]
//     [DisableAutoCreation]
//     public abstract partial class MessageSystem<TMessage> : SystemBase
//         where TMessage : struct, IRpcCommand
//     {
//         struct ReceiveMessageJob : IJobEntityBatch
//         {
//             [ReadOnly] public EntityTypeHandle EntityTH;
//             [ReadOnly] public ComponentTypeHandle<TMessage> MessageTH;
//             [ReadOnly] public ComponentTypeHandle<ReceiveRpcCommandRequestComponent> ReqTH;
//             public EntityCommandBuffer ecb;
//             public MessageHandler action;
//             public void Execute(ArchetypeChunk batchInChunk, int batchIndex)
//             {
//                 var entities = batchInChunk.GetNativeArray( EntityTH );
//                 var reqs = batchInChunk.GetNativeArray( ReqTH );
//                 var messages = batchInChunk.GetNativeArray( MessageTH );
//                 for (int i = 0; i < batchInChunk.Count; i++)
//                 {
//                     action( messages[i], reqs[i] );
//                 }
//                 ecb.AsParallelWriter().DestroyEntity( batchIndex, entities );
//             }
//         }
//         EntityCommandBufferSystem cur_ecbSystem;
//         Queue<TMessage> message_queue;
//         EntityQuery GenericQuery;
//         protected override void OnCreate()
//         {
//             if (Game.IsClientInstance)
//             {
//                 RequireSingletonForUpdate<NetworkIdComponent>();
//             }
//             GenericQuery = GetEntityQuery(
//                 ComponentType.ReadOnly<TMessage>(),
//                 ComponentType.ReadOnly<ReceiveRpcCommandRequestComponent>() );
//             message_queue = new Queue<TMessage>();
//             cur_ecbSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
//         }
//         protected override void OnUpdate()
//         {
//             var cur_ecb = cur_ecbSystem.CreateCommandBuffer();
//
//             foreach (var message in message_queue)
//             {
//                 var entity = cur_ecb.CreateEntity();
//                 cur_ecb.AddComponent<SendRpcCommandRequestComponent>( entity );
//                 cur_ecb.AddComponent( entity, message );
//             }
//             message_queue.Clear();
//
//             if (OnReceiveMessage != null)
//             {
//                 Dependency = new ReceiveMessageJob
//                 {
//                     EntityTH = GetEntityTypeHandle(),
//                     MessageTH = GetComponentTypeHandle<TMessage>( true ),
//                     ReqTH = GetComponentTypeHandle<ReceiveRpcCommandRequestComponent>( true ),
//                     ecb = cur_ecb,
//                     action = OnReceiveMessage
//                 }.Schedule( GenericQuery, Dependency );
//             }
//
//             cur_ecbSystem.AddJobHandleForProducer( Dependency );
//         }
//
//         public void SendMessage(TMessage message) { message_queue.Enqueue( message ); }
//         public delegate void MessageHandler(TMessage message, ReceiveRpcCommandRequestComponent req);
//         public event MessageHandler OnReceiveMessage;
//     }
// }

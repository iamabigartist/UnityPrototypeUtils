#if ECS
// using System;
// using Unity.Entities;
// using Unity.NetCode;
// namespace VivoxPlayground.MyVivoxFramework.Util
// {
//     public static class ECSUtil
//     {
//         [Obsolete( "Useless" )]
//         public class ECBSystemRef<TEntityCommandBufferSystem>
//             where TEntityCommandBufferSystem : EntityCommandBufferSystem
//         {
//             public ECBSystemRef(World world)
//             {
//                 system = world.GetOrCreateSystem<TEntityCommandBufferSystem>();
//             }
//             TEntityCommandBufferSystem system;
//             public void CreateECB(out EntityCommandBuffer ecb)
//             {
//                 ecb = system.CreateCommandBuffer();
//             }
//
//         }
//
//         public static TSystem CreateSystemFromMono<TSystem, TSystemGroup>(this World world)
//             where TSystem : ComponentSystemBase, new()
//             where TSystemGroup : ComponentSystemGroup
//         {
//             var system = world.CreateSystem<TSystem>();
//             var system_group = world.GetOrCreateSystem<TSystemGroup>();
//             system_group.AddSystemToUpdateList( system );
//             system_group.SortSystems();
//
//             return system;
//         }
//
//         public static void CreateEntity(this World world, out Entity entity)
//         {
//             entity = world.EntityManager.CreateEntity();
//         }
//
//         public static void AddComponentData<TComponent>(this World world, Entity e, TComponent component)
//             where TComponent : struct, IComponentData
//         {
//             world.EntityManager.AddComponentData( e, component );
//         }
//
//         public static void SendMessage<TMessage>(this World world, TMessage m, Entity target_connection = default)
//             where TMessage : struct, IRpcCommand
//         {
//             world.CreateEntity( out var e );
//             world.AddComponentData( e, m );
//             world.AddComponentData( e, new SendRpcCommandRequestComponent() { TargetConnection = target_connection } );
//         }
//
//         public static void SendMessage<TMessage>(this EntityCommandBuffer ecb, TMessage m, Entity target_connection = default)
//             where TMessage : struct, IRpcCommand
//         {
//             var e = ecb.CreateEntity();
//             ecb.AddComponent( e, m );
//             ecb.AddComponent( e, new SendRpcCommandRequestComponent() { TargetConnection = target_connection } );
//         }
//
//         public static void SendRequest<TMessage>(
//             this EntityCommandBuffer ecb,
//             RpcSessionProcessor processor,
//             TMessage request,
//             Action<IRpcResponse> receive_callback,
//             Entity target_connection = default)
//             where TMessage : struct, IRpcCommand, IRpcRequest
//         {
//             ecb.SendMessage( request, target_connection );
//             processor.BufferRequest( request, receive_callback );
//         }
//
//         public static void SendRequest<TMessage>(
//             this World world,
//             RpcSessionProcessor processor,
//             TMessage request,
//             Action<IRpcResponse> receive_callback,
//             Entity target_connection = default)
//             where TMessage : struct, IRpcCommand, IRpcRequest
//         {
//             world.SendMessage( request, target_connection );
//             processor.BufferRequest( request, receive_callback );
//         }
//     }
// }
#endif


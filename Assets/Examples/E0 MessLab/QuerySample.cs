#if ECS

using Unity.Collections;
using Unity.Entities;
using UnityEngine;
namespace VivoxPlayground.Lab5_QueryEntityInMono
{
    public interface IMyComponent { }
    public struct Apple : IComponentData, IMyComponent
    {
        public bool eatable;
    }
    public struct Orange : IComponentData, IMyComponent
    {
        public int sweetness;
    }

    public class QuerySample : MonoBehaviour
    {
        World m_world;
        EntityManager m_entity_manager;
        void Start()
        {
            m_world = World.DefaultGameObjectInjectionWorld;
            m_entity_manager = m_world.EntityManager;
        }

        void AddEntity<T>(T data) where T : struct, IComponentData
        {
            var entity = m_entity_manager.CreateEntity();
            m_entity_manager.AddComponentData( entity, data );
        }
        EntityQuery Query<T>() where T : struct, IComponentData
        {
            EntityQuery entity_query = m_entity_manager.CreateEntityQuery( new EntityQueryDesc()
            {
                All = new[] { ComponentType.ReadWrite<T>() }
            } );

            return entity_query;
        }

        void LogData<T>(EntityQuery entityQuery) where T : struct, IComponentData
        {
            var component_data_array = entityQuery.ToComponentDataArray<T>( Allocator.Temp );
            var log = $"{typeof(T)}: \n";

            foreach (T component_data in component_data_array)
            {
                log += $"{component_data.ToString()}\n";
            }

            Debug.Log( log );
        }


        void Update()
        {
            AddEntity( new Apple { eatable = false } );
            var entity_query = Query<Apple>();
            LogData<Apple>( entity_query );

        }
    }
}
  #endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Testing: MonoBehaviour
{
    //public GameObject gameObject;

    //Entity gameObjectPrefab;

    //EntityManager entityManager;
    //NativeArray<Entity> objectPlayers;
    private void Start()
    {
        //entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        //var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        //gameObjectPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObject, settings);


        //Entity objectPlayer = entityManager.Instantiate(gameObjectPrefab);

        ////Transform transform = this.GetComponent<Transform>();
        ////entityManager.SetComponentData(objectPlayer, new Translation { Value = transform.position });
        ////entityManager.SetComponentData(objectPlayer, new Rotation { Value = transform.rotation });

        //objectPlayers = new NativeArray<Entity>(5, Allocator.Temp);
        //entityManager.Instantiate(gameObjectPrefab, objectPlayers);

        //for (int x = 0; x < objectPlayers.Length; x++)
        //{
        //    entityManager.SetComponentData(objectPlayers[x], new Translation { Value = new Unity.Mathematics.float3(transform.position.x + x + 1, transform.position.y + x + 1, transform.position.z) });
        //    entityManager.SetComponentData(objectPlayers[x], new Rotation { Value = transform.rotation });
        //    //entityManager.SetComponentData(objectPlayers[x], new PlayerManager { });
        //}

        //EntityArchetype entityArchetype = entityManager.CreateArchetype(
        //    typeof(LevelCompoment),
        //    typeof(Translation),
        //    typeof(LocalToWorld),

        //    );
        //NativeArray<Entity> entityArray = new NativeArray<Entity>(2, Allocator.Temp);
        //entityManager.CreateEntity(entityArchetype, entityArray);

        //for(int i = 0; i < entityArray.Length; i++)
        //{
        //    Entity entity = entityArray[i];
        //    entityManager.SetComponentData(entity, new LevelCompoment { level = Random.Range(10,20) });
        //    entityManager.SetComponentData(entity, new ObjectComponent { objectGame = gameObject });
        //    entityManager.SetSharedComponentData(entity, new RenderMesh{

        //    });
        //}

        // objectPlayers.Dispose();
    }

    //private void Update()
    //{


    //    for (int x = 0; x < objectPlayers.Length; x++)
    //    {
    //        entityManager.SetComponentData(objectPlayers[x], new Translation { Value = new Unity.Mathematics.float3(transform.position.x + x + 1, transform.position.y + x + 1, transform.position.z) });
    //        entityManager.SetComponentData(objectPlayers[x], new Rotation { Value = transform.rotation });
    //    }


    //}


}


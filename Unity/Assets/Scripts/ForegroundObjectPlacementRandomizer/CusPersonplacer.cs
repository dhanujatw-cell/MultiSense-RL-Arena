using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers.Utilities;
using System.Linq;

namespace UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers
{
    /// <summary>
    /// Creates a 2D layer of evenly spaced GameObjects from a given list of prefabs
    /// </summary>
    [AddRandomizerMenu("Perception/Custom Foreground Object Placement Randomizer")]
    public class CusPersonplacer : Randomizer
    {
        public int objectCountX = 2;
        public int objectCountY = 2;
        public float spacing = 2f;

        public FloatParameter depth = new FloatParameter();

        public GameObjectParameter prefabs;

        GameObject m_Container;
        CustomGameObjectOneWayCache m_GameObjectOneWayCache;

        protected override void OnAwake()
        {
            m_Container = new GameObject("Foreground Objects");
            m_Container.transform.parent = scenario.transform;
            m_GameObjectOneWayCache = new CustomGameObjectOneWayCache(
                m_Container.transform, prefabs.categories.Select(element => element.Item1).ToArray());
        }

        protected override void OnIterationStart()
        {
            var offsetX = ((20f - 1) * spacing) / 2f;
            var offsetY = 0.2f;

            for (int i = 0; i < objectCountX; i++)
            {
                for (int j = 0; j < objectCountY; j++)
                {
                    float placementDepth = depth.Sample();
                    var instance = m_GameObjectOneWayCache.GetOrInstantiate(prefabs.Sample());
                    instance.transform.position = new Vector3(offsetX + i * spacing, offsetY, placementDepth);
                }
            }
        }

        protected override void OnIterationEnd()
        {
            m_GameObjectOneWayCache.ResetAllObjects();
        }
    }
}


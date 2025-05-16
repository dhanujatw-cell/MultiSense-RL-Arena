using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine.Perception.Randomization.Parameters;
using UnityEngine.Perception.Randomization.Randomizers.Utilities;
using UnityEngine.Perception.Randomization.Samplers;
using System.Collections.Generic;


namespace UnityEngine.Perception.Randomization.Randomizers.SampleRandomizers
{
    /// <summary>
    /// Creates a 2D layer of of evenly spaced GameObjects from a given list of prefabs
    /// </summary>
    [Serializable]
    [AddRandomizerMenu("Perception/Custom Foreground Object Placement Randomizer")]
    public class CustomForegroundObjectPlacementRandomizer : Randomizer
    {
        public float minX = -7.5f;
        public float maxX = -7.5f;
        public float minY = -7.5f;
        public float maxY = -7.5f;
        private float _widthX => math.abs(minX) + math.abs(maxX);
        private float _widthY => math.abs(minY) + math.abs(maxY);
        
        private GameObject sampledInstance1;
        private GameObject sampledInstance2;
        private GameObject sampledInstance3; 
        private GameObject sampledInstance4;

        public FloatParameter depth = new FloatParameter();
        public List<GameObject> sampleInstances = new List<GameObject>();
        public List<int> randomNumbers = new List<int>(); 
        int seed_x = 123;
        System.Random random;

        /// <summary>
        /// The minimum distance between all placed objects
        /// </summary>
        public float separationDistance = 0.5f;

        /// <summary>
        /// The list of prefabs sample and randomly place
        /// </summary>
        public GameObjectParameter prefabs;
        public GameObject prefabs1;
	public GameObject prefabs2;
	public GameObject prefabs3;
	public GameObject prefabs4;

        GameObject m_Container;
        CustomGameObjectOneWayCache m_GameObjectOneWayCache;

        /// <inheritdoc/>
        protected override void OnAwake()
        {
            sampledInstance1 = prefabs1;//prefabs.Sample();
            sampledInstance2 = prefabs2;//prefabs.Sample();
            sampledInstance3 = prefabs3;//prefabs.Sample();
            sampledInstance4 = prefabs4;//prefabs.Sample();
           
            sampleInstances.Add(sampledInstance1);
            sampleInstances.Add(sampledInstance2);
            sampleInstances.Add(sampledInstance3);
            sampleInstances.Add(sampledInstance4);
  

            foreach (var instance in sampleInstances)
            {
                Debug.Log("xxxxxxxxxxxxxxx!" + instance.name);
            }
 
            m_Container = new GameObject("Foreground Objects");
            m_Container.transform.parent = scenario.transform;
            m_GameObjectOneWayCache = new CustomGameObjectOneWayCache(
                m_Container.transform, prefabs.categories.Select(element => element.Item1).ToArray());
        }

        /// <summary>
        /// Generates a foreground layer of objects at the start of each scenario iteration
        /// </summary>
        protected override void OnIterationStart()
        {
            var seed = (uint)24591;//SamplerState.NextRandomState();245201
            random = new System.Random(seed_x);
            var placementSamples = CustomPoissonDiskSampling.GenerateSamples(
                _widthX, _widthY, separationDistance, seed);
            var offset = new Vector3(minX, minY, 0f);  // this is to recenter the samples at origin
            Debug.Log("placementSamples!xxxxxx" + placementSamples);
            
            Debug.Log("number of people!" + placementSamples.Length);
            int currentValue = 4;
            // number of people in the scene  depend on the _Width,_widthY, separationDistance, seed values.placementSamples constains a list of initial positions.
            foreach (var sample in placementSamples)
            {   
                Debug.Log("placementSamples!" + sample);
                // Increase the value of the variable by 1
                var remainder = currentValue % 4;
                currentValue++;
                int randomNumber = random.Next(2, 6);
                randomNumbers.Add(randomNumber); // Append the random number to the list
                Debug.Log("xxxxxxxxAAAAA!" + randomNumber);
                float placementDepth = depth.Sample();
                int newplacementDepth =(int)placementDepth;
                
                var instance = m_GameObjectOneWayCache.GetOrInstantiate(sampleInstances[remainder]);
                instance.transform.position = (new Vector3(sample.x, sample.y, (newplacementDepth+randomNumber)) + offset);
                 Debug.Log("xxxxxxxxxxxxxxx!"  + (new Vector3(sample.x, sample.y, newplacementDepth+randomNumber) + offset)+"  "+newplacementDepth);
            }
            placementSamples.Dispose();
            Debug.Log("xxxxxxxxAAAAA!");
            Debug.Log("xxxxxxxxAAAAA!");
            Debug.Log("xxxxxxxxAAAAA!");
        }

        /// <summary>
        /// Deletes generated foreground objects after each scenario iteration is complete
        /// </summary>
        protected override void OnIterationEnd()
        {
            m_GameObjectOneWayCache.ResetAllObjects();
        }
    }
}


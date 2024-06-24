using System.Collections.Generic;
using UnityEngine;
using WhacAMole.Interfaces;

namespace WhacAMole.Controllers
{
    public class HoleController : MonoBehaviour
    {
        public List<ITubeAnimal> Holes => holes;
        private List<ITubeAnimal> holes = new List<ITubeAnimal> { };
        [SerializeField] private List<Transform> holeSections;
        [SerializeField] private GameObject molePrefab;

        /// <summary>
        /// Places new Holes.
        /// </summary>
        /// <param name="amount">The amount of holes that should be placed.</param>
        public void PlaceNewHoles(int amount)
        {
            PlaceHoles(amount);
        }

        /// <summary>
        /// Activates a new mole in a random inactive hole section for a specified active time.
        /// </summary>
        /// <param name="moleActiveTime"></param>
        public void ActivateNewHole(float moleActiveTime)
        {
            ITubeAnimal tubeToActive = GetRandomInactiveHole();
            if (tubeToActive != null)
            {
                tubeToActive.Show(moleActiveTime);
            }
            else
            {
                Debug.Log("There are no more moles to activate.");
            }
        }

        /// <summary>
        /// Destroys all instantiated holes and their child game objects.
        /// </summary>
        private void DestroyAllHoles()
        {
            for (int i = 0; i < holeSections.Count; i++)
            {
                for (int j = holeSections[i].childCount - 1; j >= 0; j--)
                {
                    Destroy(holeSections[i].GetChild(j).gameObject);
                }
            }
        }

        /// <summary>
        /// Retrieves a random inactive hole (mole) from the list.
        /// </summary>
        /// <returns>A random inactive hole.</returns>
        private ITubeAnimal GetRandomInactiveHole()
        {
            List<ITubeAnimal> availableHoles = new List<ITubeAnimal>();

            for (int i = 0; i < holes.Count; i++)
            {
                if (!holes[i].IsActive)
                {
                    availableHoles.Add(holes[i]);
                }
            }
            return availableHoles.Count == 0 ? null : availableHoles[Random.Range(0, availableHoles.Count)];
        }

        /// <summary>
        /// Instantiates new holes (moles) based on the amount provided.
        /// </summary>
        /// <param name="amountOfHoles">The amount of Holes that should be created.</param>
        private void PlaceHoles(int amountOfHoles)
        {
            for (int i = 0; i < amountOfHoles; i++)
            {
                holes.Add(Instantiate(molePrefab, holeSections[i % 4]).GetComponent<ITubeAnimal>());
            }
        }

        /// <summary>
        /// Resets the controller by destroying all instantiated holes and clearing the moles list
        /// </summary>
        public void Reset()
        {
            DestroyAllHoles();
            holes = new List<ITubeAnimal> { };
        }
    }
}
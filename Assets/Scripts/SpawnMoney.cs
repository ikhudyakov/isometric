using UnityEngine;

namespace ilei
{
    public class SpawnMoney : MonoBehaviour
    {
        [SerializeField] private GameObject[] spawnMoney;
        public GameObject money;
        public int i;

        private void Start()
        {
            spawnMoney = GameObject.FindGameObjectsWithTag("Spawn Money");
            InvokeRepeating("Create", 4, 2);
        }

        public void Create()
        {
            Instantiate(money, spawnMoney[Random.Range(0, 6)].transform.position, Quaternion.identity);
        }

        private void Update()
        {

        }
    }
}
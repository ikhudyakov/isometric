using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ilei
{
    public class Controller : MonoBehaviour, IController
    {
        private NavMeshAgent _agent;
        private Animator _animator;

        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public Animator Animator { get => _animator; set => _animator = value; }

        public virtual void Move()
        {

        }
    }

}
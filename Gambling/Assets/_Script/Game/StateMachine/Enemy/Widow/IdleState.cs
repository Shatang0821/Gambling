using Framework.Entity;
using Framework.FSM;
using FrameWork.Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.Enemy.StateEnum;
    public class IdleState : BaseState
    {
        EntityObject owner;
        
        int random;
        float distance;
        float attackrange = 3f; //UŒ‚‚É“ü‚é‹——£
        bool isHalf = false;
        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
        {
            owner = entityObject;
        }

        public override void Enter()
        {
            base.Enter();
            enemyData = ResManager.Instance.GetAssetCache<EntityData>("EntityData/EnemyData");
            Debug.Log(enemyData.HP);
            if (p_entity.Position.x <= owner.Position.x)
            {
                owner.Scale = new Vector3(-1, 1, 1);
            }
            else
            {
                owner.Scale = new Vector3(1, 1, 1);
            }
            random = Random.Range(1, 100);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            distance = owner.DistanceX(p_entity);
            Debug.Log(distance);

            if (distance > attackrange)
            {
                ChangeState(StateEnum.Move);
            }
            if (distance < attackrange)
            {
                if (enemyData.HP < MaxHp / 2)
                {
                    if (!isHalf) {
                        ChangeToSkillState(1004);
                        isHalf = true;
                        
                    }
                    else
                    {
                        LowHP(random);
                    }
                    
                }
                else
                {
                    HighHP(random);
                }

                
            }

            
            
        }

        void HighHP(int rand)
        {
            if(rand >= 1 && rand < 60) 
            {
                ChangeToSkillState(1005);
            }
            else
            {
                ChangeToSkillState(1001);
            }
        }

        void LowHP(int rand)
        {
            if (rand >= 1 && rand < 20)
            {
                ChangeToSkillState(1005);
            }
            else if (rand >= 20 && rand < 60)
            {
                ChangeToSkillState(1001);
            }
            else
            {
                ChangeToSkillState(1002);
            }
        }

    }
}



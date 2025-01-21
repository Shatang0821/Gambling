using Framework.Entity;
using Framework.FSM;
using FrameWork.Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;


namespace Game.StateMachine.Enemy.Hoarder
{
    using StateEnum = Game.Entity.EnemyHoarder.StateEnum;
    public class IdleState : BaseState
    {
        EntityObject owner;
        
        int random;
        float distance;
        bool isHalf = false;
        bool isWall;
        private float meleeRange = 3f; // ‹ß‹——£UŒ‚”ÍˆÍ
        private float rangedRange = 4.5f; // ’†‹——£UŒ‚”ÍˆÍ

        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
        {
            owner = entityObject;
        }

        public override void Enter()
        {
            base.Enter();
            enemyData = ResManager.Instance.GetAssetCache<EntityData>("EntityData/EnemyData");
            Debug.Log(isWall);
            if (enemyData.HP < MaxHp /2 && !isHalf)
            {
                isHalf = true;
            }
            if (p_entity.Position.x <= owner.Position.x)
            {
                owner.Scale = new Vector3(-1, 1, 1);
            }
            else
            {
                owner.Scale = new Vector3(1, 1, 1);
            }

            isWall = owner.IsWallDetected();

            if (isWall)
            {
                ChangeToSkillState(2002);
            }
            random = Random.Range(1, 100);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            distance = owner.DistanceX(p_entity);



            // ‹——£‚É‰‚¶‚ÄUŒ‚‚Ìí—Ş‚ğ‘I‘ğ
            if (distance <= meleeRange)
            {
                if (isHalf)
                {
                    MeleeLowHP(random);
                }
                else
                {
                    MeleeHighHP(random);
                }
            }
            else if (distance > meleeRange && distance <= rangedRange)
            {
                if (isHalf)
                {
                    RangeLowHP(random);
                }
                else
                {
                    RangeHighHP(random);
                }
            }

            else
            {
                ChangeState(StateEnum.Move); // ˆÚ“®ó‘Ô‚ÉˆÚs
            }


        }

        void MeleeHighHP(int rand)
        {
            if(rand >= 1 && rand < 80) 
            {
                ChangeToSkillState(2002);
            }
            else
            {
                ChangeToSkillState(2001);
            }
        }

        void RangeHighHP(int rand)
        {
            if (rand >= 1 && rand < 30)
            {
                ChangeToSkillState(2004);
            }
            else
            {
                ChangeToSkillState(2001);
            }
        }

        void MeleeLowHP(int rand)
        {
            if (rand >= 1 && rand < 20)
            {
                ChangeToSkillState(2002);
            }
            else if (rand >= 20 && rand < 60)
            {
                ChangeToSkillState(2001);
            }
            else
            {
                ChangeToSkillState(2003);
            }
        }

        void RangeLowHP(int rand)
        {
            if (rand >= 1 && rand < 10)
            {
                ChangeToSkillState(2002);
            }
            else if (rand >= 20 && rand < 60)
            {
                ChangeToSkillState(2003);
            }
            else
            {
                ChangeToSkillState(2004);
            }
        }
    }
}



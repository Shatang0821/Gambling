using Framework.Entity;
using Framework.FSM;
using FrameWork.Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;


namespace Game.StateMachine.Enemy.skeleton
{
    using StateEnum = Game.Entity.EnemyWidow.StateEnum;
    public class IdleState : BaseState
    {
        EntityObject owner;
        
        int random;
        float distance;
        bool isHalf = false;
        bool isWall;
        private float meleeRange = 3f; // �ߋ����U���͈�
        private float rangedRange = 4.5f; // �������U���͈�

        public IdleState(EntityObject entityObject, string animName, MyStateMachine stateMachine, Animator animator) : base(entityObject, animName, stateMachine, animator)
        {
            owner = entityObject;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log(isWall);
            if (enemyData.HP < MaxHp /2 && !isHalf)
            {
                ChangeToSkillState(1004);
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
                ChangeToSkillState(1002);
            }
            random = Random.Range(1, 100);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            distance = owner.DistanceX(p_entity);



            // �����ɉ����čU���̎�ނ�I��
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
                ChangeState(StateEnum.Move); // �ړ���ԂɈڍs
            }


        }

        void MeleeHighHP(int rand)
        {
            if(rand >= 1 && rand < 80) 
            {
                ChangeToSkillState(1005);
            }
            else
            {
                ChangeToSkillState(1001);
            }
        }

        void RangeHighHP(int rand)
        {
            if (rand >= 1 && rand < 10)
            {
                ChangeToSkillState(1005);
            }
            else
            {
                ChangeToSkillState(1001);
            }
        }

        void MeleeLowHP(int rand)
        {
            if (rand >= 1 && rand < 40)
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

        void RangeLowHP(int rand)
        {
            if (rand >= 1 && rand < 10)
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



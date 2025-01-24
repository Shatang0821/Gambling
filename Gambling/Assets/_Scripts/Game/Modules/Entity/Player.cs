using System;
using UnityEngine;

using FrameWork.Component;
using Framework.Entity;
using FrameWork.EventCenter;
using FrameWork.Resource;
using Game.Component;
using Game.Components;
using Game.Input;
using Game.SkillSystem;
using Game.StateMachine;
using Game.StateMachine.Player;
using Unity.VisualScripting;
using UnityEngine.Rendering;

namespace Game.Entity
{
    public class Player : EntityObject
    {
        public enum StateEnum
        {
            Idle,
            Move,
            Jump,
            Fall,
            Attack,
            Defence,
            Skill,
            Damaged,
            Die
        }
        private EntityStateMachine _playerStateMachine;
        private PlayerInputComponent _playerInputComponent;
        private HealthComponent _healthComponent;
        private SkillComponent _playerSkillComponent;
        
        //一時的に使う
        private void Awake()
        {
                                    AddEntityComponent(new MovementComponent());
                                    AddEntityComponent(new AttackComponent());
            _healthComponent      = AddEntityComponent(new HealthComponent());
                                    AddEntityComponent(new DefendComponent());
            _playerSkillComponent = AddEntityComponent(new SkillComponent());
            _playerInputComponent = AddEntityComponent(new PlayerInputComponent());
            
            _playerSkillComponent.InitSkill(this,
                ResManager.Instance.GetAssetCache<SkillList>("SkillData/Player_SkillDataTable"));

            MyData.HP = ResManager.Instance.GetAssetCache<EntityDataSo>("EntityData/PlayerData").EntityData.HP;
            _healthComponent.AddDamageAction(ApplyDamage);
            //_healthComponent.AddDeathAction(Test);
            // ステートマシンは最後に生成
            _playerStateMachine = CreateStateMachine();
            _playerStateMachine.InitState(StateEnum.Idle);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            _playerStateMachine.LogicUpdate();
            _playerSkillComponent.UpdateCooldown();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            _playerStateMachine.PhysicsUpdate();
        }

        private void OnEnable()
        {
            _playerInputComponent.OnEnable();
            _playerStateMachine.InitState(StateEnum.Idle);
        }

        private void OnDisable()
        {
            _playerInputComponent.OnDisable();
        }

        /// <summary>
        /// 制作メソッド 後ほど制作工場に依頼すること
        /// </summary>
        /// <returns></returns>
        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine(this);
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this,StateEnum.Idle.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Move, new MoveState(this,StateEnum.Move.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Jump, new JumpState(this,StateEnum.Jump.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Fall, new FallState(this,StateEnum.Fall.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this,StateEnum.Attack.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Skill, new SkillState(this,StateEnum.Skill.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamagedState(this,StateEnum.Damaged.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Die, new DieState(this,StateEnum.Die.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Defence, new DefenceState(this,StateEnum.Defence.ToString(),stateMachine,animator));
            return stateMachine;
        }

        private void ApplyDamage(float amount)
        {
            MyData.HP -= amount;
            if (MyData.HP <= 0)
            {
                MyData.HP = 0;
                _healthComponent.SetFlag("Die",true);
            }
        }
    
    }
}

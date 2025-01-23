using Framework.Entity;
using Game.StateMachine;
using UnityEngine;
using Game.StateMachine.Enemy.skeleton;
using FrameWork.Component;
using Game.Component;
using FrameWork.Resource;
using Game.Components;
using Game.SkillSystem;
using Game.StateMachine.Enemy;

namespace Game.Entity
{
    public class EnemyWidow : EntityObject
    {
        public enum StateEnum
        {
            Idle,
            Move,
            Attack,
            Skill,
            Damaged,
            Die
        }
        
        private EntityStateMachine _enemyStateMachine;
        private SkillComponent _enemySkillComponent;
        private HealthComponent _healthComponent;
        
        private void Awake()
        {
            var movementComponent = new MovementComponent();
            var attackComponent = new AttackComponent();
            var skillComponent = new SkillComponent();

            AddEntityComponent<MovementComponent>(movementComponent);
            AddEntityComponent<AttackComponent>(attackComponent);
            AddEntityComponent<DefendComponent>(new DefendComponent());
            _enemySkillComponent = AddEntityComponent<SkillComponent>(skillComponent);
            _healthComponent = AddEntityComponent<HealthComponent>(new HealthComponent());
            
            skillComponent.InitSkill(this,
                ResManager.Instance.GetAssetCache<SkillList>("SkillData/Enemy_SkillDataTable"));
            MyData.HP = ResManager.Instance.GetAssetCache<EntityDataSo>("EntityData/EnemyData").EntityData.HP;
            _healthComponent.AddDamageAction(ApplyDamage);
            _enemyStateMachine = CreateStateMachine();
            _enemyStateMachine.InitState(StateEnum.Idle);
        }

        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine(this);
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new StateMachine.Enemy.skeleton.IdleState(this,StateEnum.Idle.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Move, new StateMachine.Enemy.skeleton.WalkState(this, StateEnum.Move.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Attack, new StateMachine.Enemy.skeleton.AttackState(this,StateEnum.Attack.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Skill, new SkillState(this, StateEnum.Skill.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Damaged, new StateMachine.Enemy.skeleton.DamagedState(this,StateEnum.Damaged.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Die, new StateMachine.Enemy.skeleton.DieState(this,StateEnum.Die.ToString(), stateMachine, animator));

            return stateMachine;
        }

        private void Update()
        {
            _enemyStateMachine.LogicUpdate();
            _enemySkillComponent.UpdateCooldown();
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
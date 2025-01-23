using Framework.Entity;
using Game.StateMachine;
using UnityEngine;
using Game.StateMachine.Enemy.Hoarder;
using FrameWork.Component;
using Game.Component;
using FrameWork.Resource;
using Game.Components;
using Game.SkillSystem;
using Game.StateMachine.Enemy;
using Unity.VisualScripting;

namespace Game.Entity
{
    public class EnemyHoarder : EntityObject
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
        private DefendComponent _defendComponent;
        private void Awake()
        {
            var movementComponent = new MovementComponent();
            var attackComponent = new AttackComponent();
            var skillComponent = new SkillComponent();
            var healthComponent = new HealthComponent();

            AddEntityComponent<MovementComponent>(movementComponent);
            AddEntityComponent<AttackComponent>(attackComponent);
            _enemySkillComponent = AddEntityComponent<SkillComponent>(skillComponent);
            _healthComponent = AddEntityComponent<HealthComponent>(healthComponent);
            _defendComponent = AddEntityComponent(new DefendComponent());
            skillComponent.InitSkill(this,
                ResManager.Instance.GetAssetCache<SkillList>("SkillData/EnemyHoarder_SkillDataTable"));
            MyData.HP = ResManager.Instance.GetAssetCache<EntityDataSo>("EntityData/EnemyData").EntityData.HP;
            _healthComponent.AddDamageAction(ApplyDamage);
            _enemyStateMachine = CreateStateMachine();
            _enemyStateMachine.InitState(StateEnum.Idle);
        }

        protected EntityStateMachine CreateStateMachine()
        {
            var stateMachine = new EntityStateMachine(this);
            var animator = GetComponentInChildren<Animator>();
            stateMachine.RegisterState(StateEnum.Idle, new IdleState(this, StateEnum.Idle.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Move, new WalkState(this, StateEnum.Move.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this, StateEnum.Attack.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Skill, new SkillState(this, StateEnum.Skill.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamageState(this, StateEnum.Damaged.ToString(), stateMachine, animator));
            stateMachine.RegisterState(StateEnum.Die, new DieState(this, StateEnum.Die.ToString(), stateMachine, animator));

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
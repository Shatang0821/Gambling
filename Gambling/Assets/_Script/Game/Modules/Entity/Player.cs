using UnityEngine;

using FrameWork.Component;
using Framework.Entity;
using FrameWork.EventCenter;
using Game.Component;
using Game.Input;
using Game.StateMachine;
using Game.StateMachine.Player;

namespace Game.Entity
{
    public class Player : EntityObject
    {
        public enum StateEnum
        {
            Idle,
            Move,
            Attack,
            Defence,
            Skill,
            Damaged,
            Die
        }
        
        private EntityStateMachine _playerStateMachine;
        private PlayerInputComponent _playerInputComponent;
        
        //一時的に使う
        private void Awake()
        {
                                    AddEntityComponent(new MovementComponent());
                                    AddEntityComponent(new SkillComponent());
            _playerInputComponent = AddEntityComponent(new PlayerInputComponent());
            
            // ステートマシンは最後に生成
            _playerStateMachine = CreateStateMachine();
            _playerStateMachine.InitState(StateEnum.Idle);
        }
        
        //一時的に使う
        private void Update()
        {
            _playerStateMachine.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _playerStateMachine.PhysicsUpdate();
        }


        private void OnEnable()
        {
            _playerInputComponent.OnEnable();
            EventCenter.DebugEventTable();
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
            stateMachine.RegisterState(StateEnum.Attack, new AttackState(this,StateEnum.Attack.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Skill, new SkillState(this,StateEnum.Skill.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Damaged, new DamageState(this,StateEnum.Damaged.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Die, new DieState(this,StateEnum.Die.ToString(),stateMachine,animator));
            stateMachine.RegisterState(StateEnum.Defence, new DefenceState(this,StateEnum.Defence.ToString(),stateMachine,animator));
            return stateMachine;
        }


    
    }
}

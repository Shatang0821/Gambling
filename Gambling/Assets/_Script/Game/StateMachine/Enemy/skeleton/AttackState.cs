using Framework.Entity;
using UnityEngine;

namespace Game.StateMachine.Enemy.skeleton
{
    public class AttackState : BaseState
    {
        public AttackState(EntityObject entityObject, string animName, Framework.FSM.MyStateMachine stateMachine, UnityEngine.Animator animator) : base(entityObject, animName, stateMachine, animator)
        {

        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Vector2 size = new Vector2(1.5f, 2f); // 四角形範囲（横3、縦2の範囲）
            Vector2 offset = new Vector2(1.5f, 0.5f);
            // OverlapAttackメソッドを使用
            _targetSelectorComponent.OverlapAttack(entityObject.LocalPosition, size, offset);

        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

        }

        public override void Enter()
        {
            base.Enter();
            //_targetSelectorComponent.CloseRangeAttack(entityObject.LocalPosition, 30, 2, 10);

        }
    }


}

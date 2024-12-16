using System;
using System.Collections.Generic;
using Framework.Entity;
using UnityEngine;

namespace Game.SkillSystem
{
    public static class SkillFactory
    {
        private const string Namespace = "Game.SkillSystem.Actions";
        private static Dictionary<string, Type> actionTypeCache = new Dictionary<string, Type>();
        public static Skill CreateSkill(SkillData data, EntityObject owner)
        {
            List<ISkillAction> actions = new List<ISkillAction>();
            foreach (var actionData in data.Actions)
            {
                ISkillAction action = CreateAction(actionData, owner);
                if (action != null)
                {
                    actions.Add(action);
                }
                else
                {
                    Debug.LogWarning($"Failed to create action for {actionData.ActionType}");
                }
            }

            return new Skill(owner,data,actions);
        }
        
        private static ISkillAction CreateAction(SkillActionData actionData, EntityObject owner)
        {
            string className = $"{Namespace}.{actionData.ActionType}Action";

            // 
            if (!actionTypeCache.TryGetValue(className, out Type actionType))
            {
                actionType = Type.GetType(className);
                if (actionType == null)
                {
                    Debug.LogWarning($"Action class {className} not found.");
                    return null;
                }
                actionTypeCache[className] = actionType; // 
            }

            try
            {
                return (ISkillAction)Activator.CreateInstance(actionType);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to create action instance for {className}: {ex.Message}");
                return null;
            }
        }
    }
}
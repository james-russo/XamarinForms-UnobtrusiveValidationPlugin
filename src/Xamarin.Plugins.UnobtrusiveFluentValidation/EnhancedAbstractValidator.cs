using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using FluentValidation;
using FluentValidation.Internal;
using Xamarin.Plugins.UnobtrusiveFluentValidation;

namespace Xamarin.Plugins.FluentValidation
{
    /// <summary>
    /// Base class for a FluentValidation Validator that works with ValidatableProperties.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnhancedAbstractValidator<T> : AbstractValidator<T>
        where T : AbstractValidationViewModel
    {
        /// <summary>
        /// When using the ValidatableProperty type, use this method to avoid needing to supply the ".Value" 
        /// which is needed when using RuleForProp.
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IRuleBuilderInitial<T, TType> RuleForProp<TType>(Expression<Func<T, ValidatableProperty<TType>>> expression, CascadeMode cascadeMode = CascadeMode.Continue)
        {
            var lambdaExpression = (MemberExpression)expression.Body;

            var propName = lambdaExpression.Member.Name;

            var originalPropertyType = typeof(T).GetRuntimeProperty(propName);
           
            var actualPropertyToValidate = typeof(ValidatableProperty<TType>).GetRuntimeProperty("Value");

            var generatedExpression = Expression.Parameter(typeof(T), "a");

            var navigationPropertyAccess = Expression.MakeMemberAccess(generatedExpression, originalPropertyType);
           
            var lambdaNameMemberAccess = Expression.MakeMemberAccess(navigationPropertyAccess, actualPropertyToValidate);

            var lambdaAccess = Expression.Lambda<Func<T, TType>>(lambdaNameMemberAccess, new List<ParameterExpression>() { generatedExpression });

            var generatedMember = lambdaAccess.GetMember();

           	var compiled = generatedMember == null || ValidatorOptions.DisableAccessorCache ? lambdaAccess.Compile() : AccessorCache<T>.GetCachedAccessor(generatedMember, lambdaAccess);

			var rule = new PropertyRule(generatedMember, compiled.CoerceToNonGeneric(), lambdaAccess, () => cascadeMode, typeof(TType), typeof(T));

            AddRule(rule);

            IRuleBuilderOptions<T, TType> ruleBuilder = new RuleBuilder<T, TType>(rule, this);

            ruleBuilder = ruleBuilder.WithName(propName);

            return (IRuleBuilderInitial<T, TType>)ruleBuilder;
        }
    }
}
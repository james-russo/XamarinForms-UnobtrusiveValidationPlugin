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
        public IRuleBuilderInitial<T, TType> RuleForProp<TType>(Expression<Func<T, ValidatableProperty<TType>>> expression)
        {
            var propName = expression.Body.ToString().Split(new char[] { '.' })[1];

			var param = Expression.Parameter(typeof(T), "a");

            var navigationProperty = typeof(T).GetRuntimeProperty(propName);
           
            var name = typeof(ValidatableProperty<TType>).GetRuntimeProperty("Value");

            var navigationPropertyAccess = Expression.MakeMemberAccess(param, navigationProperty);
           
            var nameAccess = Expression.MakeMemberAccess(navigationPropertyAccess, name);

            var lambdaAccess = Expression.Lambda<Func<T, TType>>(nameAccess, new List<ParameterExpression>() { param });

            var member = lambdaAccess.GetMember();

           	var compiled = member == null || ValidatorOptions.DisableAccessorCache ? lambdaAccess.Compile() : AccessorCache<T>.GetCachedAccessor(member, lambdaAccess);

			var rule = new PropertyRule(member, compiled.CoerceToNonGeneric(), lambdaAccess, () => CascadeMode.Continue, typeof(TType), typeof(T));

			AddRule(rule);

            IRuleBuilderOptions<T, TType> ruleBuilder = new RuleBuilder<T, TType>(rule);

            ruleBuilder = ruleBuilder.WithName(propName);

            return (IRuleBuilderInitial<T, TType>)ruleBuilder;
        }
    }
}
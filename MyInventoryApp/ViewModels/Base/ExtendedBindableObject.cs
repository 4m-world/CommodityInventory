using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MyInventoryApp.ViewModels.Base
{
    public abstract class ExtendedBindableObject : BindableObject
    {
        protected void UpdateAndNotifyOnChange<T>(ref T store, T newValue, [CallerMemberName]string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(store, newValue))
            {
                store = newValue;

                OnPropertyChanged(propertyName);
            }
        }

        public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var name = GetMemberInfo(property).Name;
            OnPropertyChanged(name);
        }

        MemberInfo GetMemberInfo(Expression expression)
        {
            MemberExpression operand;
            LambdaExpression lambdaExpression = (LambdaExpression)expression;
            if (lambdaExpression.Body as UnaryExpression != null)
            {
                UnaryExpression body = (UnaryExpression)lambdaExpression.Body;
                operand = (MemberExpression)body.Operand;
            }
            else
            {
                operand = (MemberExpression)lambdaExpression.Body;
            }
            return operand.Member;
        }
    }
}

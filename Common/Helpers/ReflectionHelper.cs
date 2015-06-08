using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common.Helpers
{
    public static class ReflectionHelper
    {
        public static string GetPropertyName(Expression<Func<object>> propertyNameExpression)
        {
            var lambda = propertyNameExpression as LambdaExpression;
            MemberExpression memberExpression;
            var unaryExpression = lambda.Body as UnaryExpression;

            if (unaryExpression != null)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.Name;
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> propertyNameExpression)
        {
            var lambda = propertyNameExpression as LambdaExpression;
            MemberExpression memberExpression;
            var unaryExpression = lambda.Body as UnaryExpression;

            if (unaryExpression != null)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.Name;
        }

        public static IEnumerable<string> GetPropertyNames<T>(params Expression<Func<T, object>>[] propertyNameExpressions)
        {
            return propertyNameExpressions.Select(GetPropertyName);
        }

        public static string GetPropertyName<T>(Expression<Func<T, string>> propertyNameExpression)
        {
            var lambda = propertyNameExpression as LambdaExpression;
            MemberExpression memberExpression;
            var unaryExpression = lambda.Body as UnaryExpression;

            if (unaryExpression != null)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.Name;
        }

        public static Type GetPropertyType<T>(Expression<Func<T, object>> propertyNameExpression)
        {
            var lambda = propertyNameExpression as LambdaExpression;
            MemberExpression memberExpression;
            var unaryExpression = lambda.Body as UnaryExpression;

            if (unaryExpression != null)
            {
                memberExpression = (MemberExpression)unaryExpression.Operand;
            }
            else
            {
                memberExpression = (MemberExpression)lambda.Body;
            }

            var propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.PropertyType;
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(Func<TAttribute, TValue> attributeValueSelector,
                                                                   TValue defaultValue, IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            if (attributes == null)
            {
                return defaultValue;
            }

            TAttribute firstAttribute = null;
            foreach (TAttribute attribute in attributes.OfType<TAttribute>())
            {
                firstAttribute = attribute;
            }

            return firstAttribute == null ? defaultValue : attributeValueSelector(firstAttribute);
        }

        public static TAttribute GetAttribute<TAttribute>(IEnumerable<object> attributes) where TAttribute : Attribute
        {
            if (attributes == null)
            {
                return null;
            }

            return attributes.OfType<TAttribute>().FirstOrDefault();
        }

        public static Type GetType(string typeName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.FullName == typeName)
                    {
                        return type;
                    }
                }
            }

            throw new Exception(string.Format("Nie można odnaleźć typu \"{0}\"", typeName));
        }

        public static Type GetSmtSoftwareType(string typeName)
        {
            return
                AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName.StartsWith("SmtSoftware")).Select(
                    assembly => assembly.GetTypes().FirstOrDefault(t => t.FullName == typeName)).FirstOrDefault(
                        type => type != null);
        }

        public static Type[] GetSubclassesOfType(Type type)
        {
            return type.Assembly.GetTypes().Where(type.IsAssignableFrom).ToArray();
        }

        public static object GetPropertyValueByAttribute(object Object, Type attributeType)
        {
            if (Object == null)
                return null;

            var properties = Object.GetType().GetProperties();
            
            var propertiesToReturn = from propertyInfo in properties
                             let propertyAttributes = propertyInfo.GetCustomAttributes(attributeType, false)
                             where propertyAttributes.Length > 0
                             select propertyInfo.GetValue(Object, null);
            
            return propertiesToReturn.FirstOrDefault();
        }

        public static T GetAttributeOfType<T>(Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace MentorsBlog.Core.Common
{
    /// <summary>
    /// Вот я писал-писал эту хуету, а оно оказывается и так работает... ёбаный metanit, наебал меня
    /// </summary>
    [Obsolete]
    public abstract record ValidatableObject : IValidatableObject
    {
        private Lazy<Dictionary<Type, IEnumerable<PropertyInfo>>> _properties = new();

        protected IEnumerable<PropertyInfo> GetProperties<T>()
            where T : class
        {
            Type type = typeof(T);
            if (!_properties.Value.ContainsKey(type))
            {
                _properties.Value.Add(type, type.GetProperties());
            }

            return _properties.Value[type];
        }

        protected IEnumerable<G> GetAttributes<T, G>(string name)
            where T : class
            where G : Attribute
        {
            Type type = typeof(T);
            Type typeAttr = typeof(G);
            if (!_properties.Value.ContainsKey(type))
            {
                _properties.Value.Add(type, type.GetProperties());
            }

            return _properties.Value[type]
                .Where(x => x.Name == name)
                .SelectMany(x => x.GetCustomAttributes<G>());
        }

        protected IEnumerable<G> GetAttributes<T, G>()
            where T : class
            where G : Attribute
        {
            Type type = typeof(T);
            Type typeAttr = typeof(G);
            if (!_properties.Value.ContainsKey(type))
            {
                _properties.Value.Add(type, type.GetProperties());
            }

            return _properties.Value[type]
                .SelectMany(x => x.GetCustomAttributes<G>());
        }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        protected IEnumerable<ValidationResult> Validate<T>() where T : class
        {
            List<ValidationResult> errors = new();

            foreach (var prop in GetProperties<T>())
            {
                foreach (var attr in GetAttributes<T, ValidationAttribute>(prop.Name))
                {
                    var value = prop.GetValue(this);
                    if (!attr.IsValid(value))
                    {
                        errors.Add(new ValidationResult($"[{attr.ErrorMessageResourceType}] - [{attr.ErrorMessageResourceName}]: {attr.ErrorMessage}"));
                    }
                }
            }

            return errors;
        }
    }
}

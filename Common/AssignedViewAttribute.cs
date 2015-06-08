using System;
using System.Linq;
using System.Windows;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AssignedViewAttribute : Attribute
    {
        public string ResourceUri { get; set; }
        public string Key { get; set; }

        public string Control { get; set; }

        public AssignedViewAttribute()
        {
        }

        public AssignedViewAttribute(string control)
        {
            Control = control;
        }

        public DataTemplate ResolveTemplate()
        {
            if (string.IsNullOrEmpty(Control))
            {
                var uri = new Uri(ResourceUri, UriKind.RelativeOrAbsolute);

                var resources = new ResourceDictionary {Source = uri};
                return (DataTemplate) resources[Key];
            }

            var nameParts = Control.Split('.').ToArray();

            var controlName = nameParts.Last();
            var controlNamespace = string.Join(".", nameParts.Take(nameParts.Length - 1));

            var dataTemplateFromControl = DataTemplateHelper.CreateDataTemplateFromControl(controlNamespace, controlName);
            return dataTemplateFromControl;
        }
    }
}
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Common
{
    public class ModelBasedTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null)
            {
                var itemType = item.GetType();
                var documentDataTemplateAttribute = (AssignedViewAttribute) itemType.GetCustomAttributes(true).FirstOrDefault(a => a.GetType() == typeof (AssignedViewAttribute));

                if (documentDataTemplateAttribute != null)
                {
                    DataTemplate dataTemplate = documentDataTemplateAttribute.ResolveTemplate();
                    return dataTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
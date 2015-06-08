using System.Windows;
using System.Windows.Markup;

namespace Common
{
    public static class DataTemplateHelper
    {
        public static DataTemplate CreateDataTemplateFromControl(string @namespace, string control, string assembly = "Letspad")
        {
            var context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            context.XmlnsDictionary.Add("d", "http://schemas.microsoft.com/expression/blend/2008");
            context.XmlnsDictionary.Add("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            context.XmlnsDictionary.Add("ns", string.Format("clr-namespace:{0};assembly={1}", @namespace, assembly));
            
            var xamlText = string.Format(@"<DataTemplate><ns:{0}></ns:{0}></DataTemplate>", control);
            var xaml = XamlReader.Parse(xamlText, context);

            return (DataTemplate)xaml;
        }
    }
}
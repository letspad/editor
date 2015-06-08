using System;
using System.Linq;
using System.Windows.Controls.Ribbon;
using System.Windows.Media.Imaging;
using Common;
using Common.Interfaces;

namespace Letspad.Services
{
    public class RibbonService : IRibbonService
    {
        private readonly Ribbon _ribbon;

        public RibbonService(Ribbon ribbon)
        {
            _ribbon = ribbon;
        }

        public void RegisterRibbonCommand(string tabName, string groupName, string label, Action commandImplementation, Func<bool> active)
        {
            var tab = GetOrCreateTab(tabName);
            var group = GetOrCreateGroup(tab, groupName);

            var ribbonButton = new RibbonButton
            {
                Label = label,
                Command = new RelayCommand(o => commandImplementation(), o => active()),
                SmallImageSource = new BitmapImage(new Uri(@"./Images/Default_16x16.png", UriKind.Relative))
            };

            group.Items.Add(ribbonButton);
        }

        private RibbonGroup GetOrCreateGroup(RibbonTab tab, string groupHeader)
        {
            var group = tab.Items.OfType<RibbonGroup>().SingleOrDefault(t => (string)t.Header == groupHeader);

            if (group == null)
            {
                group = new RibbonGroup
                {
                    Header = groupHeader
                };

                tab.Items.Add(group);
            }

            return group;
        }

        private RibbonTab GetOrCreateTab(string tabHeader)
        {
            var tab = _ribbon.Items.OfType<RibbonTab>().SingleOrDefault(t => (string)t.Header == tabHeader);

            if (tab == null)
            {
                tab = new RibbonTab
                {
                    Header = tabHeader
                };

                _ribbon.Items.Add(tab);
            }

            return tab;
        }
    }
}

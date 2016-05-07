using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MoonSharp.Interpreter;

namespace WeakAurasTimerBarCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _wowRoot = @"C:\WoW";

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            Script lua = new Script(CoreModules.Preset_Complete);

            lua.DoFile(@"bit.lua");
            lua.DoFile(@"WoWStub.lua");

            lua.DoString("strsub, strsplit, strlower, strmatch, strtrim = string.sub, string.split, string.lower, string.match, string.trim");

            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibStub\LibStub.lua");
            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibCompress\LibCompress.lua");

            lua.DoString("local f = load(\"local i = 5; i = i + 1\"); f()");

            //lua.DoString("loadstring = load");
            lua.DoFile(@"WeakAurasStub.lua");
            //lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\Transmission.lua");
        }
    }
}

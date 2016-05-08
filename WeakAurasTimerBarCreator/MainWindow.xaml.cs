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
using DynamicLua;
//using MoonSharp.Interpreter;


namespace WeakAurasTimerBarCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _wowRoot = @"C:\WoW";
        dynamic lua;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            //Script lua = new Script(CoreModules.Preset_Complete);
            lua = new DynamicLua.DynamicLua();

            lua("loadstring = load");

            lua("math.mod = math.modf");
            //lua.DoFile(@"bit.lua");
            //lua.DoFile(@"bit = numberlua.lua");
            lua("package.path = '?.lua'");
            lua("bit = require 'numberlua'");

            lua.DoFile(@"WoWStub.lua");

            lua("strsub, strsplit, strlower, strmatch, strtrim = string.sub, string.split, string.lower, string.match, string.trim");

            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibStub\LibStub.lua");
            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibCompress\LibCompress.lua");
            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\AceSerializer-3.0\AceSerializer-3.0.lua");
            //lua.DoFile(_wowRoot + "\"\\Interface\\AddOns\\WeakAuras\\libs\\CallbackHandler - 1.0\\CallbackHandler-1.0.lua\"");
            //lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\AceComm-3.0\AceComm-3.0.lua");

            lua.DoFile(@"WeakAurasStub.lua");
            //lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\Transmission.lua");
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeCompressButton_Click(object sender, RoutedEventArgs e)
        {
            //var auraTable = lua.StringToTable(CompressedText.Text, true);
            //var auraText = lua.table.tostring(auraTable);
            var auraText = lua.TransportStringToDisplay(CompressedText.Text);
            DeCompressedText.Text = auraText;
        }
    }
}

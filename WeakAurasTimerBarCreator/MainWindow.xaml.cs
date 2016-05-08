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
            lua = new DynamicLua.DynamicLua();

            lua("loadstring = load");

            lua("math.mod = math.modf");
            lua("package.path = '?.lua'");
            lua("bit = require 'numberlua'");

            lua.DoFile(@"WoWStub.lua");

            lua("strsub, strsplit, strlower, strmatch, strtrim = string.sub, string.split, string.lower, string.match, string.trim");
            lua("sort = table.sort");

            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibStub\LibStub.lua");
            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\LibCompress\LibCompress.lua");
            lua.DoFile(_wowRoot + @"\Interface\AddOns\WeakAuras\libs\AceSerializer-3.0\AceSerializer-3.0.lua");

            lua.DoFile(@"WeakAurasStub.lua");
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            var transString = lua.DisplayToTransportString(DeCompressedText.Text);
            ReCompressedText.Text = transString;
        }

        private void DeCompressButton_Click(object sender, RoutedEventArgs e)
        {
            var auraText = lua.TransportStringToDisplay(CompressedText.Text);
            DeCompressedText.Text = auraText;
        }
    }
}

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
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Dialogs;
//using MoonSharp.Interpreter;


namespace WeakAurasTimerBarCreator
{
    public partial class MainWindow : Window
    {
        dynamic lua;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindowLoaded;
        }

        private string _wowFolder;
        public string WoWFolder
        {
            get { return _wowFolder; }
            set
            {
                if (!IsCorrectFolder(value))
                {
                    MessageBox.Show(value + @"\Interface\AddOns not found.  Incorrect WoW folder?");
                }
                else
                {
                    _wowFolder = value;
                    WoWFolderText.Text = value;
                }
            }
        }

        private bool IsCorrectFolder(string value)
        {
            var isCorrect = System.IO.File.Exists(value + @"\Interface\AddOns\WeakAuras\libs\LibStub\LibStub.lua");
            if (!isCorrect)
                Status.Text = "Incorrect Wow Folder";

            return isCorrect;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {

            // TODO: Set up defaults to find Wow
            // TODO: Save this between 
            WoWFolder = @"C:\WoW";

            lua = new DynamicLua.DynamicLua();

            lua("loadstring = load");

            lua("math.mod = math.modf");
            lua("package.path = '?.lua'");
            lua("bit = require 'numberlua'");

            lua.DoFile(@"WoWStub.lua");

            lua("strsub, strsplit, strlower, strmatch, strtrim = string.sub, string.split, string.lower, string.match, string.trim");
            lua("sort = table.sort");

            lua.DoFile(_wowFolder + @"\Interface\AddOns\WeakAuras\libs\LibStub\LibStub.lua");
            lua.DoFile(_wowFolder + @"\Interface\AddOns\WeakAuras\libs\LibCompress\LibCompress.lua");
            lua.DoFile(_wowFolder + @"\Interface\AddOns\WeakAuras\libs\AceSerializer-3.0\AceSerializer-3.0.lua");

            lua.DoFile(@"WeakAurasStub.lua");
        }

        private void DeCompressButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsCorrectFolder(WoWFolder))
            {
                Status.Text = "Incorrect Wow Folder";
            }
            var auraText = lua.TransportStringToDisplay(CompressedText.Text);
            DeCompressedText.Text = auraText;
            Status.Text = "Aura Decoded";
        }

        private void CompressButton_Click(object sender, RoutedEventArgs e)
        {
            var transString = lua.DisplayToTransportString(DeCompressedText.Text);
            ReCompressedText.Text = transString;
            Status.Text = "Aura Re-Coded";
        }

        private void FindReplaceButton_Click(object sender, RoutedEventArgs e)
        {
            var auraText = DeCompressedText.Text;
            var findText = FindText.Text ?? "";
            var replaceText = ReplaceText.Text ?? "";
            int count = new Regex(Regex.Escape(findText)).Matches(auraText).Count;
            var replacedAuraText = auraText.Replace(findText, replaceText);
            DeCompressedText.Text = replacedAuraText;
            Status.Text = "Replaced " + count + " occurences of \"" + findText + "\" with \"" + replaceText + "\"";
        }

        private void WowFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (!string.IsNullOrEmpty(WoWFolder))
            {
                dialog.InitialDirectory = WoWFolderText.Text;
            }
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result.HasFlag(CommonFileDialogResult.Ok))
            {
                WoWFolder = dialog.FileName;
            }
        }
    }
}

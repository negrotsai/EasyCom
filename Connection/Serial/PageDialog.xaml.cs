using EasyCom.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
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

namespace EasyCom
{
    /// <summary>
    /// Dialog_UpdateFinish.xaml 的互動邏輯
    /// </summary>
    public partial class PageDialog : Page, IPopupDialog
    {
        public PopupDialog PopupDialog { get; }
        public ConnectionTabData Tab { get; set; }
        public MaterialDesignThemes.Wpf.PackIconKind Icon { 
            get {
                return PackIcon_Icon.Kind;
            } 
            set {
                PackIcon_Icon.Kind = value;
            }
        }
        public string InfoTitle {
            get 
            {
                return Label_Title.Content as string;
            }
            set
            {
                Label_Title.Content = value;            
            }
        }

        public string InfoContent
        {
            get
            {
                return Label_Content.Content as string;
            }
            set
            {
                Label_Content.Content = value;
            }
        }

        public Action ButtonRedoAction { get; set; } = null;
        public PageDialog()
        {
            InitializeComponent();
            this.PopupDialog = new PopupDialog(this);

            this.Button_Confirm.Click += Button_Confirm_Click;
            this.Button_Cancel.Click += Button_Cancel_Click;
            this.Button_Redo.Click += Button_Redo_Click;
        }

        private void Button_Redo_Click(object sender, RoutedEventArgs e)
        {
            ButtonRedoAction?.Invoke();
            PopupDialog.Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            PopupDialog.OnCancelInvoke();
            PopupDialog.Close();
        }

        private void Button_Confirm_Click(object sender, RoutedEventArgs e)
        {
            PopupDialog.Close();
        }

        public void OnLoaded()
        {
            this.Label_Title.Content = InfoTitle;
            this.Label_Content.Content = InfoContent;
        }

        public void OnClose()
        {

        }
    }

    
}

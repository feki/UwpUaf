using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Fido.Uaf.Shared.Messages.Asm.Objects;
using UwpUaf.Asm.Api;

namespace UwpUaf.Client.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetInfoPage : Page
    {
        readonly ObservableCollection<AsmGetInfo> authenticatorInfoItems = new ObservableCollection<AsmGetInfo>();
        readonly IAsmApi asmApi;

        public ObservableCollection<AsmGetInfo> AuthenticatorInfoItems
        {
            get
            {
                return authenticatorInfoItems;
            }
        }

        public GetInfoPage()
        {
            this.InitializeComponent();
                
            asmApi = new AsmApi();
            DataContext = this;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            try
            {
                var asm = await asmApi.GetInfoAsync();
                foreach (var info in asm)
                {
                    authenticatorInfoItems.Add(info);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

                ErrorTbl.Text = ex.Message;
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using UwpUaf.Asm.Api;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UwpUaf.Client.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetInfoPage : Page
    {
        readonly IAsmApi asmApi;
        readonly ObservableCollection<AsmGetInfo> authenticatorInfoItems = new ObservableCollection<AsmGetInfo>();

        public GetInfoPage()
        {
            this.InitializeComponent();

            asmApi = new AsmApi();
            DataContext = this;
        }

        public ObservableCollection<AsmGetInfo> AuthenticatorInfoItems
        {
            get
            {
                return authenticatorInfoItems;
            }
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

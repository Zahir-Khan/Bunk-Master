using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

namespace Bunk_Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewD : PopupPage
    {
        
        public PopupViewD(int id)
        { 
            InitializeComponent();
            BindingContext = ViewModel.ClassVMInstance.DatesVMInstance;

            AbsoluteLayout absoluteLayout = new AbsoluteLayout() { BackgroundColor = Color.White, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center,Padding = new Thickness(20) };
            StackLayout stackLayout = new StackLayout() { BackgroundColor=Color.WhiteSmoke};
            Label label1 = new Label() { };
            Label label2 = new Label() { };
            Grid grid = new Grid() { };
            Button button = new Button() {Text="Done" };
            Entry entry1 = new Entry() {Placeholder="AB",Keyboard=Keyboard.Numeric };
            Entry entry2 = new Entry() {Placeholder="TOT", Keyboard = Keyboard.Numeric };
            DatePicker picker = new DatePicker() { };
            picker.MaximumDate = DateTime.Today;
            picker.MinimumDate = DateTime.Parse(ViewModel.ClassVMInstance.GetRow(id)[6]);

            entry1.SetBinding(Entry.TextProperty, new Binding("dt_ab"));
            entry2.SetBinding(Entry.TextProperty, new Binding("dt_tot"));
            picker.SetBinding(DatePicker.DateProperty, new Binding("date"));

            #region Font
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    entry1.FontFamily = "Handlee-Regular.ttf";
                    entry2.FontFamily = "Handlee-Regular.ttf";
                    button.FontFamily = "Handlee-Regular.ttf";
                    break;
                case Device.Android:
                    entry1.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry2.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    button.FontFamily = "Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                case Device.UWP:
                    entry1.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    entry2.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    button.FontFamily = "Assets/Fonts/Handlee-Regular.ttf#Handlee-Regular";
                    break;
                default:
                    break;
            }
            #endregion

            AbsoluteLayout.SetLayoutBounds(stackLayout, new Rectangle(.5, .5, 300, 150));
            AbsoluteLayout.SetLayoutFlags(stackLayout, AbsoluteLayoutFlags.PositionProportional);

            grid.RowDefinitions.Add(new RowDefinition { Height=50});
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 100 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 50 });

            grid.Children.Add(picker,0,0);
            grid.Children.Add(entry1, 1, 0);
            grid.Children.Add(entry2, 2, 0);

            stackLayout.Children.Add(grid);
            stackLayout.Children.Add(button);
            absoluteLayout.Children.Add(stackLayout);

            Content = absoluteLayout;

            button.Clicked += Button_Clicked;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            ViewModel.ClassVMInstance.DatesVMInstance.AddCommand.Execute(true);
            SendBackButtonPressed();
            PopupNavigation.Instance.PopAllAsync();
        }

        #region PopupCallbacks


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
        #endregion
    }
}
namespace Victuz
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Routes toevoegen
            Routing.RegisterRoute("Eventdetails", typeof(Views.EventDetailsView));
            Routing.RegisterRoute("Userprofile", typeof(Views.ProfileView));
        }
    }
}

namespace Rooster;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRouters();

    }
	private void RegisterRouters()
	{
		//Routing.RegisterRoute("awarm_set",typeof(NewPage1));
	}
}

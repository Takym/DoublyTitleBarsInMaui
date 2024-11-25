namespace DoublyTitleBarsInMaui
{
	public partial class App : Application
	{
		public App()
		{
			this.InitializeComponent();

			//this.MainPage = new AppShell();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
			var page = new AppShell();

			if (this.Windows.Count >= 1) {
				this.Windows[0].Page = page;
				return base.CreateWindow(activationState);
			}

			return new(page);
		}
	}
}

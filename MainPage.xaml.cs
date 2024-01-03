global using Grpc.Core;
global using Grpc.Net.Client;

namespace example_maui_grpc;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnClicked(object sender, EventArgs e)
	{
		if (NameEntry.Text == null) {
			ResponseLabel.Text = $"Error: Please set `Name`";
			SemanticScreenReader.Announce(ResponseLabel.Text);
			return;
		}

		string BaseAddress = "https://example-go-grpc.gentlepond-fcbb8d44.japaneast.azurecontainerapps.io";

		var baseUri = new Uri(BaseAddress);
		var channel = GrpcChannel.ForAddress(baseUri);
		var greeterClient = new Helloworld.Greeter.GreeterClient(channel);

		var request = new Helloworld.HelloRequest { Name = NameEntry.Text };
		var headers = new Metadata();

		// Active Directory B2C は未実装です。
		// トークンを取得したら以下のように設定してください。
		//
		// headers.Add("Authorization", $"Bearer {token}");

		try
		{
			var response = await greeterClient.SayHelloAsync(request, headers);
			var msg = response.Message;
			ResponseLabel.Text = $"Response: {response.Message}";
		}
		catch (Exception ex)
		{
			ResponseLabel.Text = $"Error: {ex.Message}";
		}

		await channel.ShutdownAsync();

		SemanticScreenReader.Announce(ResponseLabel.Text);
	}
}


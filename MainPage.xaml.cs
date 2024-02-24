global using Grpc.Core;
global using Grpc.Net.Client;
using Taomics.Praman;
using Taomics.Praman.Lifestylejournal;

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

		if (OriginEntry.Text == null) {
			ResponseLabel.Text = $"Error: Please set `API Origin`";
			SemanticScreenReader.Announce(ResponseLabel.Text);
			return;
		}

		var baseUri = new Uri(OriginEntry.Text);
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

	private async void OnClicked2(object sender, EventArgs e)
	{
		if (OriginEntry2.Text == null) {
			ResponseLabel2.Text = $"Error: Please set `API Origin`";
			SemanticScreenReader.Announce(ResponseLabel2.Text);
			return;
		}

		var baseUri = new Uri(OriginEntry2.Text);
		var channel = GrpcChannel.ForAddress(baseUri);
		var journalClient = new LifestyleJournalService.LifestyleJournalServiceClient(channel);

		var bedtime = new DateTimeOffset(2024, 2, 22, 23,0,0,TimeSpan.FromHours(9)); // JST 23:00
		var waketime = new DateTimeOffset(2024, 2, 23, 7,0,0,TimeSpan.FromHours(9)); // JST 07:00
		var request = new LifestyleJournalKeepingRequest
		{
			Date = new Date{ Year=2024, Month=2, Day=23},
			Sleep = new JournalSleep() {
				BedTime = bedtime.ToUnixTimeSeconds(),
				WakeTime = waketime.ToUnixTimeSeconds(),
				FallAsleepDifficulty = JournalSleep.Types.FallAsleepDifficulty.Immediate,
				SleepQuality = JournalSleep.Types.SleepQuality.Neutral,
			},
		};
		var headers = new Metadata();

		// Active Directory B2C は未実装です。
		// トークンを取得したら以下のように設定してください。
		//
		// headers.Add("Authorization", $"Bearer {token}");

		headers.Add("Authorization", "email user@example.com");

		try
		{
			var response = await journalClient.KeepJournalAsync(request, headers);
			ResponseLabel2.Text = $"Response: OK";
		}
		catch (Exception ex)
		{
			ResponseLabel2.Text = $"Error: {ex.Message}";
		}

		await channel.ShutdownAsync();

		SemanticScreenReader.Announce(ResponseLabel2.Text);
	}
}


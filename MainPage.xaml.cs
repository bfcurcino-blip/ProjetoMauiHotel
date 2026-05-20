namespace MauiHotel;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        dataEntrada.MinimumDate = DateTime.Today;
        dataSaida.MinimumDate = DateTime.Today.AddDays(1);
        dataSaida.Date = DateTime.Today.AddDays(1);
    }

    private async void AbrirSobre(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SobrePage());
    }

    private void AlterarAdultos(object sender, ValueChangedEventArgs e)
    {
        lblAdultos.Text = $"Adultos: {(int)e.NewValue}";
    }

    private void AlterarCriancas(object sender, ValueChangedEventArgs e)
    {
        lblCriancas.Text = $"Crianças: {(int)e.NewValue}";
    }

    private async void CalcularReserva(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNome.Text))
        {
            await DisplayAlert("Atenção", "Digite o nome do hóspede.", "OK");
            return;
        }

        if (pickerQuarto.SelectedIndex == -1)
        {
            await DisplayAlert("Atenção", "Selecione o tipo de quarto.", "OK");
            return;
        }

        if (dataSaida.Date <= dataEntrada.Date)
        {
            await DisplayAlert("Atenção", "A data de saída deve ser depois da data de entrada.", "OK");
            return;
        }

        int diarias = (dataSaida.Date.Value - dataEntrada.Date.Value).Days;
        int adultos = (int)stepAdultos.Value;
        int criancas = (int)stepCriancas.Value;

        double valorDiaria = 0;

        switch (pickerQuarto.SelectedIndex)
        {
            case 0:
                valorDiaria = 180;
                break;
            case 1:
                valorDiaria = 250;
                break;
            case 2:
                valorDiaria = 380;
                break;
        }

        double total = diarias * valorDiaria;

        lblResultado.Text =
            $"Reserva para {txtNome.Text}\n" +
            $"Diárias: {diarias}\n" +
            $"Adultos: {adultos}\n" +
            $"Crianças: {criancas}\n" +
            $"Total: R$ {total:F2}";
    }
}
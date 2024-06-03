namespace ExamenProgresoII;

public partial class RecargarsJAguiar : ContentPage
{
	public RecargarsJAguiar()
	{
		InitializeComponent();
	}


    private int selectedAmount;

    private void ValorRecarga(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var radioButton = sender as RadioButton;
            selectedAmount = int.Parse(radioButton.Content.ToString().Split(' ')[0]);
            DisplayAlert("Monto Seleccionado", $"Selecciono una recarga de {selectedAmount} dolares", "Confirmar","Cancelar");
        }
    }

    private async void RecargarBoton(object sender, EventArgs e)
    {
        var phoneNumber = IngresarFono.Text;
        var operatorName = pickerOperator.SelectedItem?.ToString();

        if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(operatorName) || selectedAmount == 0)
        {
            await DisplayAlert("Error", "Todos los campos deben estar llenos.", "OK");
            return;
        }

        var result = await DisplayAlert("Confirmar Recarga", $"¿Desea recargar {selectedAmount} dolares al numero {phoneNumber} con la operadora {operatorName}?", "Confirmar", "Cancelar");
        if (result)
        {
            await RealizarRecargaAsync(phoneNumber, selectedAmount);
            await DisplayAlert("Recarga Realizada", $"Se hizo una recarga de {selectedAmount} dolares al numero {phoneNumber}.", "Listo");
        }
    }

    private async Task RealizarRecargaAsync(string phoneNumber, int amount)
    {
        var date = DateTime.Now.ToString("dd/MM/yyyy");
        var content = $"Se hizo una recarga de {amount} dolares el: {date}";
        var fileName = Path.Combine(FileSystem.AppDataDirectory, $"{phoneNumber}.txt");

        using (var writer = new StreamWriter(fileName))
        {
            await writer.WriteLineAsync(content);
        }
    }
}
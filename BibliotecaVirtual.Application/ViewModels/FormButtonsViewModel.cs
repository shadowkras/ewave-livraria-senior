namespace BibliotecaVirtual.Application.ViewModels
{
    public class FormButtonsViewModel
    {
        public string FormKey { get; set; }

        public int? FormValue { get; set; }

        public FormButtonsViewModel(string key, int? value)
        {
            FormKey = key;
            FormValue = value;
        }
    }
}
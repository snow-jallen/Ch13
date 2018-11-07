namespace Ch13.Shared.ViewModel
{
    public class ChildControl
    {
        public ChildControl(string header, object viewModel)
        {
            Header = header;
            ViewModel = viewModel;
        }
        public string Header { get; set; }
        public object ViewModel { get; set; }
    }
}
using Microsoft.UI;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Syncfusion.UI.Xaml.DataGrid;
using Syncfusion.UI.Xaml.DataGrid.Renderers;
using System.Threading.Tasks;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DataGridSample
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Remove the existing Renderer
            this.dataGrid.CellRenderers.Remove("CheckBox");

            // Add the custom Renderer
            this.dataGrid.CellRenderers.Add("CheckBox", new GridCellCheckBoxRendererExt());
        }
    }

    //Renderer customization
    public class GridCellCheckBoxRendererExt : GridCellCustomCheckBoxRenderer
    {
        public GridCellCheckBoxRendererExt()
        {
            this.SupportsRenderOptimization = false;
            this.IsEditable = false;
        }

        public override void OnInitializeEditElement(DataColumnBase dataColumn, FontIcon uiElement, object dataContext)
        {
            var item = dataContext as OrderInfo;
            if (uiElement == null || item == null)
                return;
            UpdateIcon(uiElement, item.Status);
        }

        protected override void OnEditElementLoaded(object sender, RoutedEventArgs e)
        {
            var uiElement = sender as FontIcon;
            if (uiElement == null || uiElement.DataContext == null)
                return;
            uiElement.Tag = uiElement.DataContext;
            uiElement.Tapped += OnTapped;
        }

        private void OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is FontIcon icon && icon.Tag is OrderInfo item)
            {
                item.Status = !item.Status;
                // Update the icon
                UpdateIcon(icon, item.Status);
            }
        }

        protected override void OnEditElementUnloaded(object sender, RoutedEventArgs e)
        {
            var uiElement = sender as FontIcon;
            if (uiElement == null)
                return;
            uiElement.Tapped -= OnTapped;
        }

        protected override void OnUnwireEditUIElement(FontIcon uiElement)
        {
            if (uiElement != null)
                uiElement.Tapped -= OnTapped;
        }

        private void UpdateIcon(FontIcon icon, bool isChecked)
        {
            if (isChecked)
            {
                icon.Glyph = "\uE8FB"; // Tick
                icon.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                icon.Glyph = "\uE711"; // Cross
                icon.Foreground = new SolidColorBrush(Colors.Red);
            }
        }
    }

    // Custom CheckBoxRenderer
    public class GridCellCustomCheckBoxRenderer : GridVirtualizingCellRenderer<FontIcon, FontIcon>
    {
        public GridCellCustomCheckBoxRenderer()
        {

        }
    }
}

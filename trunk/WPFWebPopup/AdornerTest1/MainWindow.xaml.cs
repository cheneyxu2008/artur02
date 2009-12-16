using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace AdornerTest1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			this.InitializeComponent();

			// Insert code required on object creation below this point.
		}

	    BackgroundShade shade;
	    PopUp adorner;

        private void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Create adorner
            AdornerLayer layer = AdornerLayer.GetAdornerLayer(Grid);
            adorner = new PopUp(Grid);
            
            // Close adorner
            adorner.MouseLeftButtonDown += delegate
                                               {
                                                   layer.Remove(adorner);
                                               };
            ((FrameworkElement) adorner.AdornedElement).SizeChanged += AdornedElement_SizeChanged;

            // Create shaded background
            shade = new BackgroundShade();
            AdornedElement_SizeChanged(this, null);
            
            
            // Create text
            TextBlock text = new TextBlock();
            text.Foreground = Brushes.White;
            // Bind text to selected item
            Binding binding = new Binding("Name");
            binding.Source = Grid.SelectedItem;
            text.SetBinding(TextBlock.TextProperty, binding);
            // Register text
            shade.LayoutRoot.Children.Add(text);

            // Display adorner
            adorner.Content = shade;
            layer.Add(adorner);
        }

        /// <summary>
        /// Adorned element's size changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AdornedElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            shade.Width = adorner.AdornedElement.RenderSize.Width;
            shade.Height = adorner.AdornedElement.RenderSize.Height;
        }

        
	}
}
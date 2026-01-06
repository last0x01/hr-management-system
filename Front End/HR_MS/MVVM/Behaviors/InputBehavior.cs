using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HR_MS.MVVM.Behaviors
{
    public static class InputBehavior
    {
        // Attached Property to define the input type ( "Age" or "Salary" or "Phone" ..etc)
        public static readonly DependencyProperty InputTypeProperty =
            DependencyProperty.RegisterAttached(
                "InputType",
                typeof(string),
                typeof(InputBehavior),
                new PropertyMetadata("", OnChanged));

        // Getter for the attached property
        public static string GetInputType(DependencyObject obj) => (string)obj.GetValue(InputTypeProperty);

        // Setter for the attached property
        public static void SetInputType(DependencyObject obj, string value) => obj.SetValue(InputTypeProperty, value);

        // Called when the InputType property changes
        // Attaches or detaches the event handlers
        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not TextBox txtBox)
                return;

            // Attach or detach PreviewKeyDown to handle special keys like Space
            txtBox.PreviewKeyDown -= OnPreviewKeyDown;
            txtBox.PreviewKeyDown += OnPreviewKeyDown;

            // Attach or detach PreviewTextInput to handle text input validation
            txtBox.PreviewTextInput -= OnPreviewTextInput;
            txtBox.PreviewTextInput += OnPreviewTextInput;
        }

        // Handles text input (typing) for the TextBox
        // Allows only digits or a single dot for Salary
        private static void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is not TextBox txtBox)
                return;

            string InputType = GetInputType(txtBox);
            string Text = txtBox.Text;

            // Allow digits
            if (e.Text.All(char.IsDigit))
            {
                e.Handled = false;
                return;
            }

            // Allow one dot for Salary input
            if (InputType == "Salary" && e.Text == "." && !Text.Contains("."))
            {
                e.Handled = false;
                return;
            }

            // Block anything else
            e.Handled = true;
        }

        // Handles key down events (like Space)
        // Blocks Space key input for the TextBox
        private static void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is not TextBox txtBox)
                return;

            if (e.Key == Key.Space)
            {
                e.Handled = true; // Prevent space
            }
        }
    }
}
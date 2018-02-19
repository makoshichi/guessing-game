using System;
using System.Drawing;
using System.Windows.Forms;

namespace GuessingGameReproduction.GUI
{
    class InputBox 
    {
        private InputBox() { }

        public static string Show(string promptText)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();

            form.Text = "Guessing Game";
            label.Text = promptText;

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;

            label.SetBounds(9, 20, 444, 13);
            textBox.SetBounds(12, 36, 444, 20);
            buttonOk.SetBounds(382, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(468, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk });
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            return textBox.Text;
        }
    }
}
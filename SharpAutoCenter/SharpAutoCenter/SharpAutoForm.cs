using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * So, I'm not sure if this counts as a program header, but here goes...
 * Sales Bonus Calculator - Designed by Devon Cochane (SN 200244662)
 * Last modified 2016-10-14 @ 10:12pm
 * Rev 2
 * This program will take given infomation regarding car prices and calculate the total sales amount
*/

namespace SharpAutoCenter
{
    public partial class SharpAutoForm : Form
    {
        public SharpAutoForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// aboutToolStripMenuItem_Click Event Handler
        /// This will display information about the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program calculates the amount due on a New or Used Vehicle. Designed by Devon Cochrane.");
        }
        /// <summary>
        /// StandardFinishRadioButton_CheckedChanged Event Handler
        /// This will check if the Standard Finish radio button has been checked, and will call the "AdditionalOptions" method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StandardFinishRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// PearlizedFinishRadioButton_CheckChanged Event Handler
        /// This will check if the Pearlized Finish radio button has been checked, and will call the "AdditionalOptions" method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PearlizedFinishRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// CustomizedFInishRadioButton_CheckedChanged Event Handler
        /// This will check if the Customized Finish radio button has been checked, and will call the AdditionalOptions method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomizedFinishRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// CalculateButton_Click event handler
        /// This will check if the calculate button has been clicked, and will call the CalculateTotal method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateButton_Click(object sender, EventArgs e)
        {
            CalculateTotal();
        }
        /// <summary>
        /// ClearButton_Click event handler
        /// This will check if the clear button has been clicked, and will call the ResetFields method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            ResetFields();
        }
        /// <summary>
        /// ExitButton_Click event handler
        /// This will check if the exit button has been clicked, and will close the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        /// <summary>
        /// ResetFields method
        /// This method will reset all fields to appropriate values
        /// </summary>
        private void ResetFields()
        {

            BasePriceTextBox.Text = "$0.00";
            BasePriceTextBox.Focus();
            AdditionalOptionsTextBox.Text = "";
            SubTotalTextBox.Text = "";
            SalesTaxTextBox.Text = "";
            TotalTextBox.Text = "";
            TradeInAllowanceTextBox.Text = "$0.00";
            AmountDueTextBox.Text = "";
            StereoCheckBox.Checked = false;
            LeatherCheckBox.Checked = false;
            CompNavCheckBox.Checked = false;
            StandardFinishRadioButton.Checked = true;
        }
        /// <summary>
        /// exitToolStripMenuItem_Click event handler
        /// This will check if the exit menu function has been clicked, or if the shortcut key has been used, and will call the ExitButton method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitButton.PerformClick();
        }
        /// <summary>
        /// calculateToolStripMenuItem_Click event handler
        /// This will check if the calculate menu function has been clicked, or if the shortcut key has been used, and will call the CalculateButton method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalculateButton.PerformClick();
        }
        /// <summary>
        /// clearToolStripMenuItem_Click event handler
        /// This will check if the clear menu function has been clicked, or if the shortcut key has been used, and will call the ClearButton method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearButton.PerformClick();
        }
        /// <summary>
        /// fontToolStripMenuItem_Click
        /// This will check if the font menu function has been clicked, and will change the font accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this will open the font dialog and allow the user to change the font
            //this code was taken and modified from msdn.microsoft.com
            FontDialog myFontDialog = new FontDialog();
            if (myFontDialog.ShowDialog() == DialogResult.OK)
            {
                // Set the control's font.
                BasePriceTextBox.Font = myFontDialog.Font;
                AmountDueTextBox.Font = myFontDialog.Font;

            }
            
        }
        /// <summary>
        /// Calculate Total method
        /// This will calculate the total price, it also calls the additional options method
        /// </summary>
        private void CalculateTotal()
        {
            //local variables
            double _BasePrice;
            double _Subtotal;
            double _SalesTax;
            double _Total;
            double _TradeIn;
            double _AmountDue;
            double _AdditionalOptionsBox;

            //call the additional options method
            AdditionalOptions();

            try
            {
                _BasePrice = double.Parse(BasePriceTextBox.Text, System.Globalization.NumberStyles.Currency);
                if (_BasePrice <= 0)
                {
                    //input validation
                   MessageBox.Show("Base Price must be a positive number");
                   ResetFields();
                }
                _TradeIn = double.Parse(TradeInAllowanceTextBox.Text, System.Globalization.NumberStyles.Currency);
                if (_TradeIn <= 0)
                {
                    //input validation
                    MessageBox.Show("Trade-in Amount must be a positive number");
                    ResetFields();
                }
                //This grabs the value from the additional options text box and parses it into a double
                _AdditionalOptionsBox = double.Parse(AdditionalOptionsTextBox.Text, System.Globalization.NumberStyles.Currency);
                //this calculates the subtotale by adding the additional options and base price values
                _Subtotal = _AdditionalOptionsBox + _BasePrice;
                //this converts the subtotal to a string and updates the appropriate text box
                SubTotalTextBox.Text = _Subtotal.ToString("C2");
                //this calculates the sales tax on the subtotal, I couldn't get the "Function" method to work
                _SalesTax = _Subtotal * 0.13;
                //converts to string and populates text box
                SalesTaxTextBox.Text = _SalesTax.ToString("C2");
                //this will add the subtotal and salestax to find the total
                _Total = _Subtotal + _SalesTax;
                //converts to string and populates text box
                TotalTextBox.Text = _Total.ToString("C2");
                //calculates amount due
                _AmountDue = _Total - _TradeIn;
                //converts to string and populates text box
                AmountDueTextBox.Text = _AmountDue.ToString("C2");
            }
            catch (Exception exception)
            {
                //this is shown if the user manages to put in crap data
                MessageBox.Show("Invalid Data Entered", "Input Error");
                Debug.WriteLine(exception.Message);
                //resets all fields
                ResetFields();
            }
            
            //MessageBox.Show("" + AdditionalOptions);

        }
        /// <summary>
        ///AdditionalOptions method 
        ///This will calculate the total for the additional options. It checks to see what checkboxes are currently checked, and what radio button is currently checked
        /// </summary>
        private void AdditionalOptions()
        {
            //local variables
            double _AdditionalOptions = 0;
            const double _Stereo = 425.76;
            const double _Leather = 987.41;
            const double _CompNav = 1741.23;
            const double _Pearlized = 345.72;
            const double _Customized = 599.99;

            //the following if statements check to see what check boxes have been clicked, and updates the additionaloptions total appropriately

            if (StereoCheckBox.Checked)
            {
                _AdditionalOptions = _AdditionalOptions + _Stereo;
            }
            if (LeatherCheckBox.Checked)
            {
                _AdditionalOptions = _AdditionalOptions + _Leather;
            }
            if (CompNavCheckBox.Checked)
            {
                _AdditionalOptions = _AdditionalOptions + _CompNav;
            }
            if (PearlizedFinishRadioButton.Checked)
            {
                _AdditionalOptions = _AdditionalOptions + _Pearlized;
            }
            if (CustomizedFinishRadioButton.Checked)
            {
                _AdditionalOptions = _AdditionalOptions + _Customized;
            }
            AdditionalOptionsTextBox.Text = _AdditionalOptions.ToString("C2");
        }
        /// <summary>
        /// StereoCheckBox_CheckedChanged Event Handler
        /// This will check if the StereoCheckBox has been clicked, and will call the additional options method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StereoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// LeatherCHeckBox_CheckedChanged Event Handler
        /// This will check if the Leather check box has been clicked, and will call the additional options method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeatherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// CompNavCheckBoc_CheckedChanged Event Handler
        /// This will check if the CompNav check box has been clicked, and will call the additional options method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CompNavCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            AdditionalOptions();
        }
        /// <summary>
        /// blueToolStripMenuItem_Click event handler
        /// This will check if the "Blue" option on the color menu has been clicked or called by the key shortcut, and will change the background color
        /// of the appropriate text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasePriceTextBox.BackColor = Color.Blue;
            AmountDueTextBox.BackColor = Color.Blue;
        }
        /// <summary>
        /// redToolStripMenuItem_Click event handler
        /// This will check if the "red" option on the color menu has been clicked or called by the key shortcut, and will change the background color
        /// of the appropriate text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasePriceTextBox.BackColor = Color.Red;
            AmountDueTextBox.BackColor = Color.Red;
        }
        /// <summary>
        /// whiteToolStripMenuItem_Click event handler
        /// This will check if the "white" option on the color menu has been clicked or called by the key shortcut, and will change the background color
        /// of the appropriate text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BasePriceTextBox.BackColor = Color.White;
            AmountDueTextBox.BackColor = Color.White;
        }
    }
}

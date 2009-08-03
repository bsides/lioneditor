using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using PatcherLib.Utilities;
using FFTPatcher.Datatypes;
using FFTPatcher.Controls;

namespace FFTPatcher.Editors
{
    public partial class AllPropositionsEditor : UserControl
    {
        private NumericUpDownWithDefault[] priceSpinners;
        private NumericUpDownWithDefault[] bonusSpinners;

        public AllPropositionsEditor()
        {
            InitializeComponent();
            propositionEditor1.DataChanged += new EventHandler( propositionEditor1_DataChanged );
            priceSpinners = new NumericUpDownWithDefault[] {
                price1Spinner, price2Spinner, price3Spinner, price4Spinner,
                price5Spinner,  price6Spinner,price7Spinner,price8Spinner,
                price9Spinner };
            bonusSpinners = new NumericUpDownWithDefault[] {
                bonusesSpinner1, bonusesSpinner2, bonusesSpinner3, bonusesSpinner4,
                bonusesSpinner5,bonusesSpinner6,bonusesSpinner7,bonusesSpinner8,
                bonusesSpinner9, bonusesSpinner10, bonusesSpinner11, bonusesSpinner12,
                bonusesSpinner13, bonusesSpinner14, bonusesSpinner15,bonusesSpinner16
            };
        }

        void propositionEditor1_DataChanged( object sender, EventArgs e )
        {
            propositionListBox.BeginUpdate();
            var top = propositionListBox.TopIndex;
            CurrencyManager cm = (CurrencyManager)BindingContext[propositionListBox.DataSource];
            cm.Refresh();
            propositionListBox.TopIndex = top;
            propositionListBox.EndUpdate();

        }
        private AllPropositions props;
        public void UpdateView( AllPropositions props )
        {
            this.props = props;

            propositionListBox.SelectedIndexChanged -= propositionListBox_SelectedIndexChanged;

            propositionListBox.DataSource = props.Propositions;
            propositionListBox.SelectedIndexChanged += propositionListBox_SelectedIndexChanged;
            propositionListBox.SelectedIndex = 0;
            propositionEditor1.BindTo( propositionListBox.SelectedItem as Proposition, props.Prices );

            for (int i = 0; i < priceSpinners.Length; i++)
            {
                priceSpinners[i].SetValueAndDefault( props.Prices[i], props.Default.Prices[i] );
            }
            for (int i = 0; i < bonusSpinners.Length; i++)
            {
                bonusSpinners[i].SetValueAndDefault( props.Bonuses[i], props.Default.Bonuses[i] );
            }
        }

        void priceSpinner_ValueChanged( object sender, EventArgs e )
        {
            var spinner = sender as NumericUpDownWithDefault;
            int index = priceSpinners.IndexOf( spinner );
            if (index != -1)
            {
                props.Prices[index] = (ushort)spinner.Value;
            }

            propositionEditor1.NotifyNewPrices( props.Prices );
        }

        void bonusSpinner_ValueChanged( object sender, EventArgs e )
        {
            var spinner = sender as NumericUpDownWithDefault;
            int index = bonusSpinners.IndexOf( spinner );
            if (index != -1)
            {
                props.Bonuses[index] = (ushort)spinner.Value;
            }
        }

        void propositionListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            Proposition prop = propositionListBox.SelectedItem as Proposition;
            propositionEditor1.BindTo( prop, props.Prices );
        }
    }
}

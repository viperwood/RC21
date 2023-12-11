using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace RC21;

public partial class CreateANewPatientWindow : Window
{
    public CreateANewPatientWindow()
    {
        InitializeComponent();
        LoadComboBoxInsuranceCompany();
    }

    private void LoadComboBoxInsuranceCompany()
    {
        ComboBoxInsuranceCompany.Items = Helper.Database.Insurancecompanynames
            .Select(x => new { InsuranceCompanyName = x.Companiname }).ToList();
    }
    

    private void Adding(object? sender, RoutedEventArgs e)
    {



        ErrorText.Text = InputValidation;





        if (ComboBoxSociaTipe.SelectedIndex == -1)
        {
            ErrorText.IsVisible = true;
        }
        else
        {
            ErrorText.IsVisible = false;
        }

        if (ComboBoxInsuranceCompany.SelectedIndex == -1)
        {
            ErrorText.IsVisible = true;
        }
        else
        {
            ErrorText.IsVisible = false;
        }
    }
}
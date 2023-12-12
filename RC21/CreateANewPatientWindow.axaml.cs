using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RC21.Models;

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

    private InputValidation inputValidation = new InputValidation();

    private void Adding(object? sender, RoutedEventArgs e)
    {
        ErrorText.Text = "";

        inputValidation.FioTest = Fio.Text;
        if (inputValidation.FioTest != "")
        {
            ErrorText.Text = inputValidation.FioTest;
        }


        inputValidation.DataTest = Date.Text;
        if (inputValidation.DataTest != "")
        {
            ErrorText.Text = inputValidation.DataTest;
        }


        inputValidation.SeriesPassportTest = SeriesPassport.Text;
        if (inputValidation.SeriesPassportTest != "")
        {
            ErrorText.Text = inputValidation.SeriesPassportTest;
        }


        inputValidation.NumberPassportTest = NumberPassport.Text;
        if (inputValidation.NumberPassportTest != "")
        {
            ErrorText.Text = inputValidation.NumberPassportTest;
        }


        inputValidation.PhoneTest = Phone.Text;
        if (inputValidation.PhoneTest != "")
        {
            ErrorText.Text = inputValidation.PhoneTest;
        }


        inputValidation.EmailTest = Email.Text;
        if (inputValidation.EmailTest != "")
        {
            ErrorText.Text = inputValidation.EmailTest;
        }


        inputValidation.NumberPolisTest = NumberPolis.Text;
        if (inputValidation.NumberPolisTest != "")
        {
            ErrorText.Text = inputValidation.NumberPolisTest;
        }


        if (ComboBoxSociaTipe.SelectedIndex == -1)
        {
            ErrorText.Text = "Не выбран тип страхового полиса!";
        }

        if (ComboBoxInsuranceCompany.SelectedIndex == -1)
        {
            ErrorText.Text = "Не выбрано название страховой компании!";
        }

        if (ErrorText.Text == "")
        {
            AddingUserTable();
            AddingPatient();
            
        }
    }

    private void AddingPatient()
    {
        List<Usertable> usert = Helper.Database.Usertables.ToList();
        Patient patient = new Patient();
        patient.Passportnumber = Convert.ToInt32(NumberPassport.Text.Replace(" ", ""));
        patient.Id = Helper.Database.Patients.Count() + 1;
        var buf = usert.Where(x => x.Fullname == Fio.Text).Select(x => x.Id).ToList();
        patient.Userid = buf[0];
        patient.Passportseries = Convert.ToInt32(SeriesPassport.Text);
        if (ComboBoxSociaTipe.SelectedIndex == 0)
        {
            patient.Socialtype = "oms";
        }
        else
        {
            patient.Socialtype = "dms";
        }
        patient.Phone = Phone.Text;
        patient.Email = Email.Text;
        patient.Insurancepolicynumber = (int?)Convert.ToInt64(NumberPolis.Text.Replace(" ", ""));
        patient.Birthday = Convert.ToDateTime(Date.Text.Replace(",", "."));
        Helper.Database.Add(patient);
        Helper.Database.SaveChanges();
    }

    private void AddingUserTable()
    {
        Usertable usertable = new Usertable();
        usertable.Fullname = Fio.Text;
        usertable.Login = Email.Text;
        usertable.Id = Helper.Database.Usertables.Count() + 1;
        usertable.Password = Date.Text;
        usertable.Roleid = 5;
        usertable.Releasedate = DateTime.Now;
        Helper.Database.Add(usertable);
        Helper.Database.SaveChanges();
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        Close();
        
    }

    
}
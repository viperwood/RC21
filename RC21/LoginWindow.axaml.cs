using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using RC21.Models;

namespace RC21;

public partial class LoginWindow : Window
{
    private string _passwordUser;
    private bool Block = false;
    private DispatcherTimer _timer = new DispatcherTimer();
    public LoginWindow()
    {
        InitializeComponent();
        TextBoxPassword.PasswordChar = '•';
    }
    public LoginWindow(bool blockUser)
    {
        _timer.Interval = TimeSpan.FromSeconds(0);
        _timer.Tick += TimerClick;
        _timer.Start();
        TimerWorck.TimeBlock = DateTime.Now + new TimeSpan(0, 0, 30, 0 );
        Block = blockUser;
        InitializeComponent();
        TextBoxPassword.PasswordChar = '•';
    }

    private void TimerClick(object? sender, EventArgs e)
    {
        TimerWorck timerWorck = new TimerWorck();
        BlockTimerText.Text = timerWorck.TimerBlock();
        if (timerWorck.TimeEndBlockChack() == true)
        {
            Block = false;
            _timer.Stop();
        }
    }

    private void Reg(object? sender, RoutedEventArgs e)
    {
        string patientsText =
            File.ReadAllText(@"C:\Users\prdb\RiderProjects\RC21\RC21\patients.xml");
        XDocument xDocumentPatientsText = XDocument.Parse(patientsText);
        var patients = xDocumentPatientsText.Descendants("record")
            .Select(patientsElements => new
            {
                fullName = (string)patientsElements.Element("fullName")!,
                login = (string)patientsElements.Element("login")!,
                pwd = (string)patientsElements.Element("pwd")!,
                guid = (string)patientsElements.Element("guid")!,
                id = (int)patientsElements.Element("id")!,
                email = (string)patientsElements.Element("email")!,
                social_sec_number = (int)patientsElements.Element("social_sec_number")!,
                ein = (string)patientsElements.Element("ein")!,
                social_type = (string)patientsElements.Element("social_type")!,
                phone = (string)patientsElements.Element("phone")!,
                passport_s = (int)patientsElements.Element("passport_s")!,
                passport_n = (int)patientsElements.Element("passport_n")!,
                birthdate_timestamp = (long)patientsElements.Element("birthdate_timestamp")!,
                country = (string)patientsElements.Element("country")!,
                insurance_name = (string)patientsElements.Element("insurance_name")!,
                insurance_address = (string)patientsElements.Element("insurance_address")!,
                insurance_inn = (int)patientsElements.Element("insurance_inn")!,
                ipadress = (string)patientsElements.Element("ipadress")!,
                insurance_pc = (int)patientsElements.Element("insurance_pc")!,
                insurance_bik = (int)patientsElements.Element("insurance_bik")!,
                ua = (string)patientsElements.Element("ua")!
            }).ToList();
        
        String bloodText =
            File.ReadAllText(@"C:\Users\prdb\RiderProjects\RC21\RC21\blood.xml");
        XDocument xDocumentBlood = XDocument.Parse(bloodText);
        var blood = xDocumentBlood.Descendants("record")
            .Select(bloodElement => new
            {
                id = (int)bloodElement.Element("id")!,
                patient = (int)bloodElement.Element("patient")!,
                barcode = (int)bloodElement.Element("barcode")!,
                date = (long)bloodElement.Element("date")!
            }).ToList();

        String bloodServicesText =
            File.ReadAllText(@"C:\Users\prdb\RiderProjects\RC21\RC21\blood_services.xml");
        XDocument xDocumentDloodServices = XDocument.Parse(bloodServicesText);
        var bloodServices = xDocumentDloodServices.Descendants("record")
            .Select(bloodServicesElement => new
            {
                id = (int)bloodServicesElement.Element("blood")!,
                service = (int)bloodServicesElement.Element("service")!,
                result = (double)bloodServicesElement.Element("result")!,
                finished = (long)bloodServicesElement.Element("finished")!,
                accepted = (bool)bloodServicesElement.Element("accepted")!,
                status = (string)bloodServicesElement.Element("status")!,
                analyzer = (string)bloodServicesElement.Element("analyzer")!,
                user = (int)bloodServicesElement.Element("user")!
            }).ToList();

        
        
        
        
        


        
        
        for (int i = 0; i < bloodServices.Count; i++)
        {
            Ordertable ordertable = new Ordertable();
            TimeSpan a = new TimeSpan( i, 0 ,0 ,0);
            ordertable.Datecreate = DateTime.Now - a;
            Helper.Database.Add(ordertable);
        }
        Helper.Database.SaveChanges();
    }

    private void VisibleP(object? sender, RoutedEventArgs e)
    {
        _passwordUser = TextBoxPassword.Text;
        if (VisiblePassword.IsChecked == true)
        {
            TextBoxPassword.PasswordChar = '\0';
        }
        else
        {
            TextBoxPassword.PasswordChar = '•';
        }
    }

    private void Login(object? sender, RoutedEventArgs e)
    {
        if (Block == false)
        {
            DateTime datalogin = DateTime.Now;
            Releasedate _releasedate = new Releasedate();
            _releasedate.Datalogin = datalogin;
            _releasedate.Id = Helper.Database.Releasedates.Count() + 1;
            var login = Helper.Database.Usertables
                .Where(x => x.Login == LoginUser.Text && x.Password == TextBoxPassword.Text)
                .ToList();
            if (login.Count() == 1)
            {
                UsedUser.FullUser = login.ToList();
                _releasedate.Userid = UsedUser.FullUser[0].Id;
                _releasedate.Loginuser = UsedUser.FullUser[0].Login;
                _releasedate.Loginverification = true;
                Helper.Database.Add(_releasedate);
                Helper.Database.SaveChanges();
                MainWindow mainWindow = new MainWindow(login[0].Roleid);
                mainWindow.Show();
                Close();
            }
            else
            {
                var checklogin = Helper.Database.Usertables
                    .Where(x => x.Login == LoginUser.Text && x.Password != TextBoxPassword.Text)
                    .ToList();
                if (checklogin.Count() == 1)
                {
                    _releasedate.Userid = checklogin[0].Id;
                    _releasedate.Loginuser = checklogin[0].Login;
                    _releasedate.Loginverification = false;
                    Helper.Database.Add(_releasedate);
                    Helper.Database.SaveChanges();
                }
                ErorLogin.IsVisible = true;
            } 
        }
    }
}
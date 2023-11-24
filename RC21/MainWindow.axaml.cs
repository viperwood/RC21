using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

using System.Xml.Linq;

using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using RC21.Models;

namespace RC21;

public partial class MainWindow : Window
{
    private int? _roleUser;
    private DispatcherTimer _timer = new DispatcherTimer();
    public MainWindow()
    {
        InitializeComponent();
        Menu();
    }

    public MainWindow(int? role)
    {
        InitializeComponent();
        _roleUser = role;
        Menu();
        PersonalArea();
    }

    private void TimerTick(object? sender, EventArgs e)
    {
        TimerWorck timerWorck = new TimerWorck();
        Timers.Text = timerWorck.Timerlab();
        if (timerWorck.TimeBlockChack() == 1)
        {
            VarningWindow.IsVisible = true;
        }
        else if (timerWorck.TimeBlockChack() == 2)
        {
            LoginWindow loginWindow = new LoginWindow(true);
            loginWindow.Show();
            UsedUser.FullUser.Clear();
            _timer.Stop();
            Close();
        }
    }
    
    private void PersonalArea()
    {
        List<Roletable> roletables = Helper.Database.Roletables.ToList();
        List<Usertable> usertables = Helper.Database.Usertables.ToList();
        UserNameBlock.Text = UsedUser.FullUser[0].Fullname;
        var roleUser = usertables
            .Where(x => x.Login == UsedUser.FullUser[0].Login)
            .Join
            (
            roletables,
            u => u.Roleid,
            r => r.Id,
            (u, r) => new
            {
                rname = r.Namerole,
                uname = u.Login
            })
            .ToList();
        foreach (var asd in roleUser)
        {
            RoleUserBlock.Text = asd.rname;
            if (asd.rname == "Лаборант" || asd.rname == "Лаборант-иследователь")
            {
                TimerConect();
            }
        }
    }

    private void TimerConect()
    {
        TimerWorck.LoginTime = DateTime.Now + new TimeSpan(0, 2, 30, 0);
        _timer.Interval = TimeSpan.FromSeconds(0);
        _timer.Tick += TimerTick;
        _timer.Start();
    }
    
    private void Menu()
    {
        switch (_roleUser)
        {
            case 1:
                MenuAdmin.IsVisible = true;
                break;
            case 2:
                MenuLab.IsVisible = true;
                break;
            case 3:
                MenuLabRes.IsVisible = true;
                break;
            case 4:
                MenuAccountant.IsVisible = true;
                break;
        }
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        UsedUser.FullUser.Clear();
        LoginWindow loginWindow = new LoginWindow();
        loginWindow.Show();
        Close();
    }

    private void HistoriWindow(object? sender, RoutedEventArgs e)
    {
        CheckHistoriAdmin checkHistoriAdmin = new CheckHistoriAdmin(_roleUser);
        checkHistoriAdmin.Show();
        Close();
    }

    private void NewOrder(object? sender, RoutedEventArgs e)
    {
        CreateNewOrder createNewOrder = new CreateNewOrder();
        createNewOrder.Show();
        Close();
    }
}
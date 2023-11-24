using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RC21.Models;

namespace RC21;

public partial class CheckHistoriAdmin : Window
{
    private int? _role;
    public CheckHistoriAdmin()
    {
        InitializeComponent();
        Histori();
    }
    public CheckHistoriAdmin(int? role)
    {
        InitializeComponent();
        Histori();
        _role = role;
        ComboBoxLogin.SelectionChanged += ComboBoxLoginEvent;
    }

    
    
    
    private void Histori()
    {
        List<Releasedate> releasedates = Helper.Database.Releasedates.ToList();
        List<Usertable> usertables = Helper.Database.Usertables.ToList();
        ComboBoxLogin.Items = usertables.Select(x => new { SortLogin = x.Login}).ToList();
        ListHistori.Items = releasedates.ToList();
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow(_role);
        mainWindow.Show();
        Close();
    }

    private void ComboBoxDataEvent(object? sender, SelectionChangedEventArgs e)
    {
        List<Releasedate> releasedates = Helper.Database.Releasedates.ToList();
        

        if (ComboBoxData.SelectedIndex != -1)
        {
            if (ComboBoxData.SelectedIndex == 0)
            {
                releasedates = releasedates.OrderBy(x => x.Datalogin).ToList();
            }
            else
            {
                releasedates = releasedates.OrderByDescending(x => x.Datalogin).ToList();
            }
        }
        
        ListHistori.Items = releasedates.ToList();
    }

    private void ComboBoxLoginEvent(object? sender, RoutedEventArgs e)
    {
        List<Releasedate> releasedates = Helper.Database.Releasedates.ToList();
        var a = releasedates.Where(x => x.Userid == ComboBoxLogin.SelectedIndex);
        ListHistori.Items = a.ToList();

    }
}
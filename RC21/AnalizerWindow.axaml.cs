using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using RC21.Models;

namespace RC21;

public partial class AnalizerWindow : Window
{
    private int? _role;
    public AnalizerWindow()
    {
        InitializeComponent();
    }
    
    public AnalizerWindow(int? role)
    {
        InitializeComponent();
        Analizer.Items = Helper.Database.Analizertipes.Select(x => new
        {
            x.Analizername
        }).ToList();
        _role = role;
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow(_role);
        mainWindow.Show();
        Close();
    }

    private void AnalizerNameButton(object? sender, RoutedEventArgs e)
    {
        Order.Items = Helper.Database.Analyzers
            .Select(x => new
        {
            x.Nameanalyzer,
            x.Barcode
        })
            .Where(x => Analizer.SelectedIndex + 1 == x.Nameanalyzer)
            .ToList();
    }
}
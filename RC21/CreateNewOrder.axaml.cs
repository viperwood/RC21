using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using RC21.Models;

namespace RC21;

public partial class CreateNewOrder : Window
{
    private List<string> _listFromSelectServises = new List<string>();
    private List<Servicetipe> _servicetipes = Helper.Database.Servicetipes.ToList();
    private int? _role;
    private int _compani;
    
    public CreateNewOrder()
    {
        InitializeComponent();
        ListOfServises();
    }
    
    public CreateNewOrder(int? roleUser)
    {
        InitializeComponent();
        ListOfServises();
        SelectOfTheNumberBarcode();
        SelectOfTheFullNameClients();
        _role = roleUser;
    }
    
    private void SelectOfTheFullNameClients()
    {
        List<Usertable> usertables = Helper.Database.Usertables.ToList();
        FullNameClients.Items = usertables.Where(x => x.Roleid == 5).Select(x => x.Fullname).OrderBy(x => x);
    }
    
    private void SelectOfTheNumberBarcode()
    {
        Filter.Items = Helper.Database.Analyzers.Select(x=>x.Barcode.ToString()).OrderBy(x=> x);
    }
    
    private void ListOfServises()
    {
        ServiceBox.Items = _servicetipes.Select(x => new
        {
            x.Nameservice
        }).ToList();
    }

    private void LoadServiseList()
    {
        ServiceList.Items = _listFromSelectServises.Select(x => new
        {
            SelectedServises = x
        }).ToList();
    }
    

    private void mines(object? sender, RoutedEventArgs e)
    { 
        _listFromSelectServises.Remove(_listFromSelectServises[ServiceList.SelectedIndex]);
        LoadServiseList();
    }

    private void plus(object? sender, RoutedEventArgs e)
    {
        if (ServiceBox.SelectedIndex != -1)
        {
            _listFromSelectServises.Add(_servicetipes[ServiceBox.SelectedIndex].Nameservice!);
            LoadServiseList();
        }
    }

    private void Beack(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow(_role);
        mainWindow.Show();
        Close();
    }

    private void Further(object? sender, RoutedEventArgs e)
    {
        if ((Filter.Text != "" || Filter.Text != null) && (FullNameClients.Text != null || FullNameClients.Text != "") && _listFromSelectServises.Count != 0)
        {
            ErrorDate.IsVisible = false;
            List<Usertable> usertables = Helper.Database.Usertables.Where(x => x.Roleid == 5).ToList();
            bool errorUserName = false;
            for (int i = 0; i < usertables.Count(); i++)
            {
                if (FullNameClients.Text == usertables[i].Fullname)
                {
                    errorUserName = false;
                    break;
                }
                else
                {
                    errorUserName = true;
                }
            }

            if (errorUserName == true)
            {
                CreateANewPatientWindow createANewPatientWindow = new CreateANewPatientWindow();
                createANewPatientWindow.Show();
                
                
                
                
            }
            /*for (int i = 0; i < _listFromSelectServises.Count; i++)
            {
                
            }*/
        }
        else
        {
            ErrorDate.IsVisible = true;
        }
    }
}
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
    private List<int> _listFromSelectServisesId = new List<int>();
    private List<Servicetipe> _servicetipes = Helper.Database.Servicetipes.ToList();
    private int? _role;
    
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
        _role = roleUser;
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
        _listFromSelectServisesId.Remove(ServiceList.SelectedIndex);
        LoadServiseList();
    }

    private void plus(object? sender, RoutedEventArgs e)
    {
        if (ServiceBox.SelectedIndex != -1)
        {
            _listFromSelectServises.Add(_servicetipes[ServiceBox.SelectedIndex].Nameservice!);
            _listFromSelectServisesId.Add(_servicetipes[ServiceBox.SelectedIndex].Id);
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
                CreateANewPatientWindow createANewPatientWindow = new CreateANewPatientWindow(_role);
                createANewPatientWindow.Show();
            }
            else
            {

                List<Patient> patients = Helper.Database.Patients.ToList();
                var idusername = Helper.Database.Usertables.Where(y => y.Fullname == FullNameClients.Text)
                    .Select(y => y.Id).ToList();
                var idpatient = patients.Where(x => x.Userid == idusername[0])
                    .Select(x => x.Id).ToList();
                
                
                
                
                
                
                
                    
                Ordertable ordertable = new Ordertable();
                for (int i = 0; i < _listFromSelectServises.Count; i++)
                {
                    ordertable.Id = Helper.Database.Ordertables.Count() + 1;
                    ordertable.Datecreate = DateTime.Now;
                    ordertable.Orderstatus = false;
                    ordertable.Servicestatus = "Rejected";
                    ordertable.Serviceid = _listFromSelectServisesId[i];
                    ordertable.Patientid = idpatient[0];
                    Helper.Database.Add(ordertable);
                    Helper.Database.SaveChanges();
                }
                
            }
        }
        else
        {
            ErrorDate.IsVisible = true;
        }
    }
    private void TestingVariant(object? sender, KeyEventArgs e)
    {


        List<Usertable> usertables = Helper.Database.Usertables.Where(x => x.Roleid == 5).ToList();

        List<string> check = new List<string>();

        string firstWord = FullNameClients.Text;

        foreach (var secondWord in usertables)
        {
            var n = FullNameClients.Text.Length + 1;
            var m = secondWord.Fullname.Length + 1;
            var matrixD = new int[n, m];

            const int deletionCost = 1;
            const int insertionCost = 1;

            for (var i = 0; i < n; i++)
            {
                matrixD[i, 0] = i;
            } 

            for (var j = 0; j < m; j++)
            {
                matrixD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var substitutionCost = firstWord[i - 1] == secondWord.Fullname[j - 1] ? 0 : 1;

                    matrixD[i, j] = Minimum(matrixD[i - 1, j] + deletionCost,          // удаление
                        matrixD[i, j - 1] + insertionCost,         // вставка
                        matrixD[i - 1, j - 1] + substitutionCost); // замена
                }
            }
            check.Add(matrixD[n - 1, m - 1].ToString());
            FullNameClients.Items =  check.Select(x => x).ToList();
        }
        
        
        
        
        
        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        /*List<Usertable> usertables = Helper.Database.Usertables.Where(x => x.Roleid == 5).ToList();

        List<string> check = new List<string>();

        if (string.IsNullOrEmpty(FullNameClients.Text) == false)
        {
            int schet = 0;
            foreach (var element in usertables)
            {
                int k = 0;
                if (FullNameClients.Text.Length > element.Fullname.Length)
                {
                    
                    foreach (var i in FullNameClients.Text)
                    {
                        if (i != element.Fullname[k])
                        {
                            schet++;
                        }
                        k++;
                    }
                }
                else
                {
                    foreach (var i in element.Fullname)
                    {
                        if (i != FullNameClients.Text)
                        {
                            schet++;
                        }
                        k++;
                    }
                }
                if (schet < 4)
                {
                    check.Add(element.Fullname);
                }
                
            }
            FullNameClients.Items = check.Select(x => x).ToList();
        }*/
    }
}
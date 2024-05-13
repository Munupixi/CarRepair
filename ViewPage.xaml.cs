using CarRepair.Models;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace CarRepair
{
    public partial class ViewPage : Page
    {
        private List<Request> requests;
        private List<Request> viewRequests = new List<Request>();

        public ViewPage()
        {
            InitializeComponent();
            CarRepairContext carRepairContext = new CarRepairContext();
            if (User.ActiveUser.RoleId == 1)
            {
                requests = carRepairContext.Requests.Include(r => r.Status).ToList();
            }
            else
            {
                requests = carRepairContext.Requests.Where(r => r.ExecutorId == User.ActiveUser.UserId).Include(r => r.Status).ToList();
                Create.IsEnabled = false;
            }
            UpdateView();
        }

        private void UpdateView()
        {
            viewRequests.Clear();
            foreach (Request request in requests)
            {
                if (request.ProblemDescription.ToLower().Contains(Search.Text.ToLower()))
                {
                    viewRequests.Add(request);
                }
            }
            Requests.ItemsSource = null;
            Requests.ItemsSource = viewRequests;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage();
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateView();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Frame.Content = new ManipulationPage((sender as FrameworkElement).DataContext as Request);
        }
    }
}
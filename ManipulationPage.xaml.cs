using CarRepair.Models;
using System.Windows;
using System.Windows.Controls;

namespace CarRepair
{
    public partial class ManipulationPage : Page
    {
        public ManipulationPage()
        {
            InitializeComponent();
            CarRepairContext carRepairContext = new CarRepairContext();
            SetUp();
            if (carRepairContext.Requests.Count() == 0)
            {
                RequestId.Text = "1";
            }
            else
            {
                RequestId.Text = (carRepairContext.Requests.Max(x => x.RequestId) + 1).ToString();
            }
        }

        public ManipulationPage(Request request)
        {
            InitializeComponent();
            SetUp();
            RequestId.Text = request.RequestId.ToString();
            Executor.SelectedItem = request.ExecutorId;
            Status.SelectedIndex = request.StatusId - 1;
            CreationDate.Text = request.CreationDate.ToString();
            TypeCar.Text = request.TypeCar;
            ModelCar.Text = request.ModelCar;
            ProblemDescription.Text = request.ProblemDescription;
            ClientCompleteName.Text = request.ClientCompleteName;
            ClientPhone.Text = request.ClientPhone;
            ExecutorComment.Text = request.ExecutorComment;
            if (User.ActiveUser.RoleId == 2)
            {
                RemoveExecutor.IsEnabled = Executor.IsEnabled = CreationDate.IsEnabled = TypeCar.IsEnabled = ModelCar.IsEnabled = ProblemDescription.IsEnabled = ClientCompleteName.IsEnabled = ClientPhone.IsEnabled = false;
            }
        }

        private void SetUp()
        {
            CarRepairContext carRepairContext = new CarRepairContext();
            Status.ItemsSource = carRepairContext.Statuses.OrderBy(s => s.StatusId).Select(s => s.Title).ToList();
            Executor.ItemsSource = carRepairContext.Users.Where(u => u.RoleId == 2).Select(u => u.UserId).ToList();
        }

        private void RemoveExecutor_Click(object sender, RoutedEventArgs e)
        {
            Executor.SelectedIndex = -1;
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Status.SelectedIndex != -1 && DateOnly.TryParse(CreationDate.Text, out _) && TypeCar.Text.Length <= 100 && ModelCar.Text.Length <= 100 && ClientCompleteName.Text.Length <= 150 && ClientPhone.Text.Length <= 11)
            {
                CarRepairContext carRepairContext = new CarRepairContext();
                Request? request = carRepairContext.Requests.FirstOrDefault(r => r.RequestId == Convert.ToInt32(RequestId.Text));
                if (request != null)
                {
                    carRepairContext.Requests.Remove(request);
                }
                request = new Request
                (
                    Convert.ToInt32(RequestId.Text),
                    Executor.SelectedItem != null ? Convert.ToInt32(Executor.SelectedItem.ToString()) : null,
                    Status.SelectedIndex + 1,
                    DateOnly.Parse(CreationDate.Text),
                    TypeCar.Text,
                    ModelCar.Text,
                    ProblemDescription.Text,
                    ClientCompleteName.Text,
                    ClientPhone.Text,
                    ExecutorComment.Text);
                carRepairContext.Add(request);
                carRepairContext.SaveChanges();
                MainWindow.Frame.Content = new ViewPage();
            }
            else
            {
                MessageBox.Show("Данные введены с ошибками(ой)");
            }
        }
    }
}
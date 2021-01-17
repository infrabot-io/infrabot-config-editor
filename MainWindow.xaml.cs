using System;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Collections.Generic;

namespace TelegramBotConfigEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Config config = null;
        string JsonConfigFile = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainMenuNewConfig_Click(object sender, RoutedEventArgs e)
        {
            string DefaultConfig = "{\"telegram_bot_token\":\"YOUR_API_TOKEN\",\"telegram_enable_logging\":false,\"telegram_enable_reminder\":true,\"telegram_enable_reloadconfig\":true,\"telegram_enable_emergency\":true,\"telegram_powershell_path\":\"C:\\\\Windows\\\\System32\\\\WindowsPowerShell\\\\v1.0\\\\powershell.exe\",\"telegram_powershell_executionpolicy\":\"-ExecutionPolicy Unrestricted\",\"telegram_logs_path\":\"C:\\\\ProgramFiles\\\\infrabot.io\\\\logs\",\"telegram_result_max_length\":12000,\"telegram_allowed_chats_id\":[],\"telegram_allowed_users_id\":[],\"telegram_allowed_users_id_emergency\":[],\"telegram_allowed_chats_id_emergency\":[],\"telegram_allowed_users_id_reloadconfig\":[],\"telegram_allowed_chats_id_reloadconfig\":[],\"telegram_allowed_users_id_getcommands\":[],\"telegram_allowed_chats_id_getcommands\":[],\"telegram_allowed_users_id_remindme\":[],\"telegram_allowed_chats_id_remindme\":[],\"telegram_commands\":[]}";
            config = null;
            JsonConfigFile = "";
            config = JsonConvert.DeserializeObject<Config>(DefaultConfig);
            this.Title = "InfraBot.IO configurator * - Not Saved - New File";
            MainPanelData.IsEnabled = true;
            InitFormWithConfig(config);
        }

        private void MainMenuStopService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RunCmdWithArguments("/c net stop infrabot.io");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuStartService_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RunCmdWithArguments("/c net start infrabot.io");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainMenuOpenConfig_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON Files (*.json)|*.json";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                try
                {
                    JsonConfigFile = "";
                    config = null;
                    JsonConfigFile = File.ReadAllText(dlg.FileName);
                    config = JsonConvert.DeserializeObject<Config>(JsonConfigFile);
                    InitFormWithConfig(config);
                    this.Title = "InfraBot.IO configurator * - Not Saved - " + dlg.FileName;
                    MainPanelData.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cannot read file Or READ permission error, or json file constitency is wrong, or this is not a JSON file! \n\nAdditional info: " + ex.Message);
                }
            }
        }

        private void ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            ManualViewer.ShowInfo(button);
        }

        private void MainMenuSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                config.telegram_bot_token = telegram_bot_token.Text;
                config.telegram_enable_logging = (bool)telegram_enable_logging.IsChecked;
                config.telegram_enable_reminder = (bool)telegram_enable_reminder.IsChecked;
                config.telegram_enable_reloadconfig = (bool)telegram_enable_reloadconfig.IsChecked;
                config.telegram_enable_emergency = (bool)telegram_enable_emergency.IsChecked;
                config.telegram_enable_showmyid = (bool)telegram_enable_showmyid.IsChecked;
                config.telegram_powershell_path = telegram_powershell_path.Text;
                config.telegram_powershell_executionpolicy = telegram_powershell_executionpolicy.Text;
                config.telegram_result_max_length = Convert.ToInt32(telegram_result_max_length.Text);
                List<int> telegram_allowed_users_id_array = new List<int> { };
                foreach (AllowedIDs id in telegram_allowed_users_id_add_list.Items)
                {
                    telegram_allowed_users_id_array.Add(id.ID);
                }
                config.telegram_allowed_users_id = telegram_allowed_users_id_array;

                //emergency allowed chats and users
                List<int> telegram_allowed_users_id_emergency_array = new List<int> { };
                foreach (AllowedIDs id in telegram_allowed_users_id_emergency_list.Items)
                {
                    telegram_allowed_users_id_emergency_array.Add(id.ID);
                }
                config.telegram_allowed_users_id_emergency = telegram_allowed_users_id_emergency_array;

                //reloadconfig allowed chats and users
                List<int> telegram_allowed_users_id_reloadconfig_array = new List<int> { };
                foreach (AllowedIDs id in telegram_allowed_users_id_reloadconfig_list.Items)
                {
                    telegram_allowed_users_id_reloadconfig_array.Add(id.ID);
                }
                config.telegram_allowed_users_id_reloadconfig = telegram_allowed_users_id_reloadconfig_array;

                //getcommands allowed chats and users
                List<int> telegram_allowed_users_id_getcommands_array = new List<int> { };
                foreach (AllowedIDs id in telegram_allowed_users_id_getcommands_list.Items)
                {
                    telegram_allowed_users_id_getcommands_array.Add(id.ID);
                }
                config.telegram_allowed_users_id_getcommands = telegram_allowed_users_id_getcommands_array;

                //remindme allowed chats and users
                List<int> telegram_allowed_users_id_remindme_array = new List<int> { };
                foreach (AllowedIDs id in telegram_allowed_users_id_remindme_list.Items)
                {
                    telegram_allowed_users_id_remindme_array.Add(id.ID);
                }
                config.telegram_allowed_users_id_remindme = telegram_allowed_users_id_remindme_array;

                string config_serialized = JsonConvert.SerializeObject(config);
                File.WriteAllText(saveFileDialog.FileName, config_serialized);
                this.Title = "InfraBot.IO configurator";
            }
        }

        private void MainMenuInfrabotIO_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://infrabot.io");
        }

        private void MainMenuDocumentation_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://infrabot.io/documentation");
        }

        private void MainMenuCloseFile_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to close this file? All changes will not be saved!", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Title = "InfraBot.IO configurator";
                config = null;
                JsonConfigFile = "";
                MainPanelData.IsEnabled = false;
                CleanFormData();
            }
        }

        private void MainMenuExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeCheckItemText(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.IsChecked == false)
            {
                cb.Content = "False";
            }
            else
            {
                cb.Content = "True";
            }
        }

        private void AddItemToAllowedList(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                if (button.Name == "telegram_allowed_users_id_add_button")
                    telegram_allowed_users_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(telegram_allowed_users_id_add_text.Text) });
                else if (button.Name == "telegram_allowed_users_id_emergency_add_button")
                    telegram_allowed_users_id_emergency_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(telegram_allowed_users_id_emergency_add_text.Text) });
                else if (button.Name == "telegram_allowed_users_id_reloadconfig_add_button")
                    telegram_allowed_users_id_reloadconfig_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(telegram_allowed_users_id_reloadconfig_add_text.Text) });
                else if (button.Name == "telegram_allowed_users_id_getcommands_add_button")
                    telegram_allowed_users_id_getcommands_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(telegram_allowed_users_id_getcommands_add_text.Text) });
                else if (button.Name == "telegram_allowed_users_id_remindme_add_button")
                    telegram_allowed_users_id_remindme_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(telegram_allowed_users_id_remindme_add_text.Text) });

            }
            catch { }
        }

        private void DeleteItemFromAllowedList(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn.Name == "telegram_allowed_users_id_delete_button")
                    telegram_allowed_users_id_add_list.Items.Remove((AllowedIDs)btn.DataContext);
                else if (btn.Name == "telegram_allowed_users_id_emergency_delete_button")
                    telegram_allowed_users_id_emergency_list.Items.Remove((AllowedIDs)btn.DataContext);
                else if (btn.Name == "telegram_allowed_users_id_reloadconfig_delete_button")
                    telegram_allowed_users_id_reloadconfig_list.Items.Remove((AllowedIDs)btn.DataContext);
                else if (btn.Name == "telegram_allowed_users_id_getcommands_delete_button")
                    telegram_allowed_users_id_getcommands_list.Items.Remove((AllowedIDs)btn.DataContext);
                else if (btn.Name == "telegram_allowed_users_id_remindme_delete_button")
                    telegram_allowed_users_id_remindme_list.Items.Remove((AllowedIDs)btn.DataContext);
            }
            catch { }
        }

        private void InitFormWithConfig(Config config)
        {
            telegram_allowed_users_id_add_list.Items.Clear();
            telegram_allowed_users_id_emergency_list.Items.Clear();
            telegram_allowed_users_id_reloadconfig_list.Items.Clear();
            telegram_allowed_users_id_getcommands_list.Items.Clear();
            telegram_allowed_users_id_remindme_list.Items.Clear();

            telegram_bot_token.Text = config.telegram_bot_token;
            telegram_enable_logging.IsChecked = config.telegram_enable_logging;
            if (config.telegram_enable_logging)
            {
                telegram_enable_logging.Content = "True";
            }
            else
            {
                telegram_enable_logging.Content = "False";
            }

            telegram_enable_reminder.IsChecked = config.telegram_enable_reminder;
            if (config.telegram_enable_reminder)
            {
                telegram_enable_reminder.Content = "True";
            }
            else
            {
                telegram_enable_reminder.Content = "False";
            }

            telegram_enable_reloadconfig.IsChecked = config.telegram_enable_reloadconfig;
            if (config.telegram_enable_reloadconfig)
            {
                telegram_enable_reloadconfig.Content = "True";
            }
            else
            {
                telegram_enable_reloadconfig.Content = "False";
            }

            telegram_enable_emergency.IsChecked = config.telegram_enable_emergency;
            if (config.telegram_enable_emergency)
            {
                telegram_enable_emergency.Content = "True";
            }
            else
            {
                telegram_enable_emergency.Content = "False";
            }

            telegram_enable_showmyid.IsChecked = config.telegram_enable_showmyid;
            if (config.telegram_enable_showmyid)
            {
                telegram_enable_showmyid.Content = "True";
            }
            else
            {
                telegram_enable_showmyid.Content = "False";
            }

            telegram_powershell_path.Text = config.telegram_powershell_path;
            telegram_powershell_executionpolicy.Text = config.telegram_powershell_executionpolicy;
            telegram_result_max_length.Text = config.telegram_result_max_length.ToString();

            foreach (int AllowedUser in config.telegram_allowed_users_id)
            {
                telegram_allowed_users_id_add_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(AllowedUser) });
            }

            foreach (int AllowedUser in config.telegram_allowed_users_id_emergency)
            {
                telegram_allowed_users_id_emergency_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(AllowedUser) });
            }

            foreach (int AllowedUser in config.telegram_allowed_users_id_reloadconfig)
            {
                telegram_allowed_users_id_reloadconfig_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(AllowedUser) });
            }

            foreach (int AllowedUser in config.telegram_allowed_users_id_getcommands)
            {
                telegram_allowed_users_id_getcommands_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(AllowedUser) });
            }

            foreach (int AllowedUser in config.telegram_allowed_users_id_remindme)
            {
                telegram_allowed_users_id_remindme_list.Items.Add(new AllowedIDs { ID = Convert.ToInt32(AllowedUser) });
            }
        }

        private void CleanFormData()
        {
            telegram_allowed_users_id_add_list.Items.Clear();
            telegram_allowed_users_id_emergency_list.Items.Clear();
            telegram_allowed_users_id_reloadconfig_list.Items.Clear();
            telegram_allowed_users_id_getcommands_list.Items.Clear();
            telegram_allowed_users_id_remindme_list.Items.Clear();
            telegram_bot_token.Text = "";
            telegram_enable_logging.IsChecked = false;
            telegram_enable_reminder.IsChecked = false;
            telegram_enable_reloadconfig.IsChecked = false;
            telegram_enable_emergency.IsChecked = false;
            telegram_enable_showmyid.IsChecked = false;
            telegram_powershell_path.Text = "";
            telegram_powershell_executionpolicy.Text = "";
            telegram_result_max_length.Text = "";
            telegram_allowed_users_id_add_text.Text = "";
            telegram_allowed_users_id_emergency_add_text.Text = "";
            telegram_allowed_users_id_reloadconfig_add_text.Text = "";
            telegram_allowed_users_id_getcommands_add_text.Text = "";
            telegram_allowed_users_id_remindme_add_text.Text = "";
        }

        private static void RunCmdWithArguments(string arguments)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = arguments;
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();
            MessageBox.Show(process.StandardOutput.ReadToEnd());
        }

        private void OnlyNumberCheckTextbox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

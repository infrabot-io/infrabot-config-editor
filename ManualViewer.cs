using System.Windows;
using System.Windows.Controls;

namespace TelegramBotConfigEditor
{
    public partial class ManualViewer
    {
        public static void ShowInfo(Button button)
        {
            if (button.Name == "telegram_bot_token_i")
                MessageBox.Show("Parameter in config.json file is: telegram_bot_token \n\nProvide your Telegram Bot Token API which you got from Telegram bot - @BotFather");
            else if (button.Name == "telegram_enable_logging_i")
                MessageBox.Show("Parameter in config.json file is: telegram_enable_logging \n\nIf value FALSE, then Bot will not write usage logs into application`s Logs directory. If TRUE logs will be written there.");
            else if (button.Name == "telegram_enable_reminder_i")
                MessageBox.Show("Parameter in config.json file is: telegram_enable_reminder \n\nIf value FALSE, then you will not be able to use /remindme command. This commands reminds you about something. Write /remindme ? to bot to get more info.");
            else if (button.Name == "telegram_enable_reloadconfig_i")
                MessageBox.Show("Parameter in config.json file is: telegram_enable_reloadconfig \n\nIf value FALSE, then you will not be able to use /reloadconfig command. This command reloads config.json file (in same folder with TelegramBot.exe file) after changes without restarting service");
            else if (button.Name == "telegram_enable_emergency_i")
                MessageBox.Show("Parameter in config.json file is: telegram_enable_emergency \n\nIf value FALSE, then you will not be able to use /emergency command. This command stops bot service on server. To be able to send commands to bot again, you will have to connect to server and start the service again");
            else if (button.Name == "telegram_enable_showmyid_i")
                MessageBox.Show("Parameter in config.json file is: telegram_enable_showmyid \n\nIf value FALSE, then you will not be able to use /showmyid command. This command shows id of the user who used this command. \n\nThis command is necessary when you want to give access to user to bot or to command but do not know his id");
            else if (button.Name == "telegram_powershell_path_i")
                MessageBox.Show("Parameter in config.json file is: telegram_powershell_path \n\nSpecifiy full path to your PowerShell.exe file. On new versions of PowerShell path might differ.");
            else if (button.Name == "telegram_powershell_executionpolicy_i")
                MessageBox.Show("Parameter in config.json file is: telegram_powershell_executionpolicy \n\nSpecifiy Execution policy of PowerShell. By default it is Unrestricted. You can strengthen your policy by yourself or even make it empty if you dont need that. Specifying wrong policy on restricted system will affect scripts. Read more about this on Microsoft site before!");
            else if (button.Name == "telegram_result_max_length_i")
                MessageBox.Show("Parameter in config.json file is: telegram_result_max_length \n\nSpecifiy max length of the message which will be sent from bot when reacting on commands. \n\nExample: You created command which gets logs from some server. Bot will take all output of your script and send it to you. If characters count of output is more than specified in this field, then it will stop sending messages. Imagine sending gigabytes of data to your telegram chat if this limiting parameter not existed....");
            else if (button.Name == "telegram_allowed_users_id_i")
                MessageBox.Show("Parameter in config.json file is: telegram_allowed_users_id \n\nList of Telegram Users` ids from whom Bot can accept commands. \n\nPlease read more about how to configure it on https://infrabot.io/documentation/configoverview");
            else if (button.Name == "telegram_allowed_users_id_emergency_i")
                MessageBox.Show("Parameter in config.json file is: telegram_allowed_users_id_emergency \n\nList of Telegram Users` ids from whom Bot can accept /emergency command. \n\nPlease read more about how to configure it on https://infrabot.io/documentation/configoverview");
            else if (button.Name == "telegram_allowed_users_id_reloadconfig_i")
                MessageBox.Show("Parameter in config.json file is: telegram_allowed_users_id_reloadconfig \n\nList of Telegram Users` ids from whom Bot can accept /reloadconfig command. This parameter is checked before telegram_allowed_chats_id_emergency.  \n\nPlease read more about how to configure it on https://infrabot.io/documentation/configoverview");
            else if (button.Name == "telegram_allowed_users_id_getcommands_i")
                MessageBox.Show("Parameter in config.json file is: telegram_allowed_users_id_getcommands \n\nList of Telegram Users` ids from whom Bot can accept /getcommands command.  \n\nPlease read more about how to configure it on https://infrabot.io/documentation/configoverview");
            else if (button.Name == "telegram_allowed_users_id_remindme_i")
                MessageBox.Show("Parameter in config.json file is: telegram_allowed_users_id_remindme \n\nList of Telegram Users` ids from whom Bot can accept /getcommands command.  \n\nPlease read more about how to configure it on https://infrabot.io/documentation/configoverview");
        }
    }
}
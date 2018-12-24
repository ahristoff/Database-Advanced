namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using System;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string command = commandParameters[0].ToLower();

            string result = null;

            switch (command)
            {
                case "registeruser":
                    result = RegisterUserCommand.Execute(commandParameters);
                    break;
                case "addtown":
                    result = AddTownCommand.Execute(commandParameters);
                    break;
                case "modifyuser":
                    result = ModifyUserCommand.Execute(commandParameters);
                    break;
                case "delete":
                    result = DeleteUser.Execute(commandParameters);
                    break;
                case "addtag":
                    result = AddTagCommand.Execute(commandParameters);
                    break;
                case "exit":
                    result = ExitCommand.Execute();
                    break;
                case "makefriends":
                    result = AddFriendCommand.Execute(commandParameters);
                    break;
                case "listfriends":
                    result = PrintFriendsListCommand.Execute(commandParameters);
                    break;
                case "createalbum":
                    result = CreateAlbumCommand.Execute(commandParameters);
                    break;
                case "uploadpicture":
                    result = UploadPictureCommand.Execute(commandParameters);
                    break;
                case "login":
                    result = LoginCommand.Execute(commandParameters);
                    break;
                case "logout":
                    result = LogoutCommand.Execute(commandParameters);
                    break;
                default:
                    throw new InvalidOperationException($"Command {command} not valid");
            }

            return result;
        }
    }
}

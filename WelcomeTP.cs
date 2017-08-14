using System.Collections.Generic;
using Oxide.Core.Libraries.Covalence;

namespace Oxide.Plugins
{
    [Info("WelcomeTP", "Ryan", "1.0.0")]
    [Description("Teleports players to a position if they're new")]

    class WelcomeTP : CovalencePlugin
    {
        private string Lang(string key, string id = null, params object[] args) => string.Format(lang.GetMessage(key, this, id), args);

        private static ConfigFile _cFile;

        private const string _perm = "welcometp.used";

        #region Config

        public class ConfigFile
        {
            public GenericPosition Position;

            public static ConfigFile DefaultConfig()
            {
                return new ConfigFile
                {
                    Position = new GenericPosition(0, 0, 0)
                };
            }
        }

        protected override void LoadDefaultConfig()
        {
            Puts("Generating default configuration file...");
            _cFile = ConfigFile.DefaultConfig();
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            _cFile = Config.ReadObject<ConfigFile>();
            if (_cFile.Position == null)
            {
                Puts("Regerating configuration file because it was incorrectly formatted");
                LoadDefaultConfig();
                SaveConfig();
            }
        }

        protected override void SaveConfig() => Config.WriteObject(_cFile);

        #endregion

        #region Lang

        private void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["Welcome"] = "Welcome to {0}, {1}! You've been teleported to our hub",
            }, this);
        }

        #endregion

        #region Hooks

        private void Init()
        {
            SaveConfig();
            permission.RegisterPermission(_perm, this);
        }

        private void OnUserConnected(IPlayer player)
        {
            if (!permission.UserHasPermission(player.Id, _perm))
            {
                player.Teleport(_cFile.Position.X, _cFile.Position.Y, _cFile.Position.Z);
                permission.GrantUserPermission(player.Id, _perm, this);
                timer.Once(2f, () =>
                {
                    player.Reply(Lang("Welcome", player.Id, server.Name, player.Name));
                });
            }
        }

        #endregion
    }
}
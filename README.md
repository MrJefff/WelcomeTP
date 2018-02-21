**WelcomeTP** randomly selects a spawn point for new players out of the ones you have set yourself with the in-game command described below. This is useful for servers who want new players to be teleported to a the hub or shop etc.

**Note:** This plugin determines new players on whether they have the permission "welcometp.used" or not. If they have the permission, they'll not be teleported to the set position. If they don't have it, they'll be teleported when they connect. So, all players that connect after this plugin has been loaded will be classed as new players.

## Permission
- `welcometp.set` -- Required to use command.

## Command
- **/tpset** - Sets the position where you currently are and saved it into the datafile.

## Default Language File
`oxide/lang/en/WelcomeTP.json`
```json
{
  "Welcome": "Welcome to {0}, {1}! You've been teleported to our hub",
  "PositionSet": "You've sucessfully added your current position to the data file",
  "Permission": "You don't have permission to use that command"
}
```

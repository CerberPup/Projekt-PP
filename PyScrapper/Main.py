from Scrapper import *
from CmdParser import *

cmdParser = CmdParser(sys.argv)
if len(cmdParser.args) > 1:
    if not cmdParser.GetHelp():
        login = cmdParser.GetLogin()
        passwd = cmdParser.GetPassword()
        scrapper = Scrapper(login, passwd, cmdParser.GetDebugMode())
        if cmdParser.GetFollowers():
            scrapper.getFollowers()
        if cmdParser.GetFollowings():
            scrapper.getFollowings()
        if cmdParser.GetPhotosCmd():
            usersList = cmdParser.GetUsers()
        if len(usersList) > 0:
            for user in usersList:
               scrapper.GetPhotosForUser(user)

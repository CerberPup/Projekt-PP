class CmdParser:

    def __init__(self, argv):
        self.Users = []
        self.args = argv
        if(len(self.args)==1):
            self.DisplayHelp()
            return
        elif self.GetHelp():
            self.DisplayHelp()
            return
        else:
            self.Credentials={}
            self.Credentials['login'] = self.args[1]
            self.Credentials['password'] = self.args[2]

    def IsParameterPresent(self, parameter):
        for arg in self.args:
            if arg == parameter:
                return True
        return False

    def GetPhotosCmd(self):
        return self.IsParameterPresent('-p')

    def GetUsers(self):
        present = False
        for arg in self.args:
            if(arg=='-u'):
                present=True
                continue
            if(present):
                if '-' in arg:
                    return self.Users
                else:
                    self.Users.append(arg)
        return self.Users

    def GetLogin(self):
        return self.Credentials['login']

    def GetPassword(self):
        return self.Credentials['password']

    def GetDebugMode(self):
        return self.IsParameterPresent('-debug')

    def GetFollowings(self):
        return self.IsParameterPresent("-f1")

    def GetFollowers(self):
        return self.IsParameterPresent("-f2")

    def GetHelp(self):
        return self.IsParameterPresent("-h")

    def GetOffline(self):
        return self.IsParameterPresent("-offline")

    def UseFollowingsList(self):
        return self.IsParameterPresent("-sf1")

    def UseFollowersList(self):
        return self.IsParameterPresent("-sf2")

    def DisplayHelp(self):
        print("Cmd args: <email> <password> [<args>]")
        print("args:")
        print("\t-f1\t-\tget followings list")
        print("\t-sf1\t-\tuser followings list as the users list to get data about")
        print("\t-f2\t-\tget followers list - might not work xD")
        print("\t-p\t-\tget photos for users given in the -u parameter")
        print("\t-u\t-\tlist of users to process, now used only with -p parameter")
        print("\t-debug\t-\tturn on debug info on std output (anyway logged to log_email file)")
        print("\t-offline\t-\tdo not log in to service ( debug purposes mostly) ")

        print("\nexample: email@domain.com samplepasswd -f1")
        print("\nexample: email@domain.com samplepasswd -p -u username1 username2")

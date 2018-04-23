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

    def GetPhotosCmd(self):
        for arg in self.args:
            if(arg=='-p'):
                return True
        return False

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
        for arg in self.args:
            if arg == "-debug":
                return True
        return False

    def GetFollowings(self):
        for arg in self.args:
            if arg == "-f1":
                return True
        return False

    def GetFollowers(self):
        for arg in self.args:
            if arg == "-f2":
                return True
        return False

    def GetHelp(self):
        for arg in self.args:
            if arg == "-h":
                return True
        return False

    def DisplayHelp(self):
        print("Cmd args: <email> <password> [<args>]")
        print("args:")
        print("\t-f1\t-\tget followings list")
        print("\t-f2\t-\tget followers list - might not work xD")
        print("\t-p\t-\tget photos for users given in the -u parameter")
        print("\t-u\t-\tlist of users to process, now used only with -p parameter")
        print("\t-debug\t-\tturn on debug info on std output (anyway logged to log_email file)")
        print("\nexample: email@domain.com samplepasswd -f1")
        print("\nexample: email@domain.com samplepasswd -p -u username1 username2")

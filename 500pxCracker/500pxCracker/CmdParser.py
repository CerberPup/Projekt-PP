try:
    import re
except ImportError:
    try:
        with open ("error.log", 'a+') as f:
            f.write("Import error in CmdParser.py")
    except Exception:
        print("Import error in CmdParser.py")

class CmdParser:

    def __init__(self, argv):
        self.Users = []
        self.args = argv
        if(len(self.args)<3):
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
                if re.match('^[-].*', arg):
                    return self.Users
                else:
                    self.Users.append(arg)
        return self.Users

    def GetHoldCleanup(self):
        return self.IsParameterPresent('-noCleanup')

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

    def GetVote(self):
        return self.IsParameterPresent("-v")

    def FollowUser(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-fl":
                return next(it)
        return ""

    def UnfollowUser(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-ufl":
                return next(it)
        return ""

    def VoteForPhoto(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-v":
                return next(it)
        return ""

    def UnvotePhoto(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-uv":
                return next(it)
        return ""

    def VoteForFresh(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-vf":
                return next(it)
        return ""

    def VoteForUpcoming(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-vu":
                return next(it)
        return ""

    def GetVotesForPhoto(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-l":
                return next(it)
        return ""

    def GetDBUpdate(self):
        return self.IsParameterPresent("-udb")

    def GetGalleriesAndPhotosFilter(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-filter":
                return next(it)
        return ""

    def GetPhotosPages(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-pages":
                return next(it)
        return ""

    def GetGalleriesAmount(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-galleries":
                return next(it)
        return ""

    def GetDelayTime(self):
        delay=0.5
        it = iter(self.args)
        for arg in it:
            if arg == "-s":
                try:
                    delay = float(next(it))
                except ValueError:
                    delay=0.5
                finally:
                    break
        return delay

    def GetParamFile(self):
        it = iter(self.args)
        for arg in it:
            if arg == "-file":
                return next(it)
        return ""

    def ParseParamFile(self):
        args=[]
        paramFile = self.GetParamFile()
        if paramFile == "":
            return args
        with open(paramFile, 'r') as f:
            for line in f:
                args.append(line.strip())
        return args


    def DisplayHelp(self):
        print("Cmd args: <email> <password> [<args>]")
        print("args:")
        print("\t-f1\t-\tget followings list")
        print("\t-f2\t-\tget followers list")
        print("\t-sf1\t-\tuse followings list as the users list to photos")
        print("\t-p\t-\tget photos for users given in the -u parameter")
        print("\t-u\t-\tlist of users to process, now used only with -p parameter")
        print("\t-v\t-\tvotes for photo [photoID]")
        print("\t-uv\t-\tunvotes for photo [photoID]")
        print("\t-fl\t-\tfollows user [username]")
        print("\t-ufl\t-\tunfollows user [username]")
        print("\t-vf\t-\tvotes for photos in fresh list [amount]")
        print("\t-vu\t-\tvotes for photos in upcoming list [amount]")
        print("\t-l\t-\tget votes for photo in [photoID]")
        print("\t-udb\t-\tupdate local database")
        print("\t-s\t-\tsets the delay value in seconds as floating point number")
        print("\t-debug\t-\tturns on debug info on std output (anyway logged to log_email file), should be used in terminal only")
        print("\t-file\t-\tparse parameters from file")
        print("\nexample: email@domain.com samplepasswd -f1")
        print("\nexample: email@domain.com samplepasswd -v -file UserInfo/photos -debug -s 0")
        print("\nexample: email@domain.com samplepasswd -p -u username1 username2"  )
        print("\nexample: email@domain.com samplepasswd -sf1")
        print("\nexample: email@domain.com samplepasswd -fl someUser")
        print("\nexample: email@domain.com samplepasswd -v 123456789")
        print("\nexample: email@domain.com samplepasswd -vf 100")

from Scrapper import *
from CmdParser import *



cmdParser = CmdParser(sys.argv)
if len(cmdParser.args) > 1:
    if not cmdParser.GetHelp():
        login = cmdParser.GetLogin()
        passwd = cmdParser.GetPassword()
        scrapper = Scrapper(login, passwd, cmdParser.GetDebugMode(), cmdParser.GetOffline())

        if cmdParser.GetFollowers():
            scrapper.getFollowers()
        if cmdParser.GetFollowings():
            scrapper.getFollowings()

        toFollow = cmdParser.FollowUser()
        toUnfollow = cmdParser.UnfollowUser()
        if toFollow != "":
            scrapper.FollowUser(toFollow)
        if toUnfollow != "":
            scrapper.UnfollowUser(toUnfollow)

        if cmdParser.GetPhotosCmd():
            usersList=[]
            if cmdParser.UseFollowingsList():
                usersList=scrapper.ParseFollowingsFiles()
            else:
                usersList = cmdParser.GetUsers()
            if len(usersList) > 0:
               for user in usersList:
                   scrapper.GetPhotosForUser(user)

        if cmdParser.GetVote():
            if scrapper.PhotoIsLiked(cmdParser.VoteForPhoto()):
                scrapper.logger.LogLine("Photo already likes")
            else:
                scrapper.VoteForPhoto(cmdParser.VoteForPhoto())

        if cmdParser.UnvotePhoto()!="":
            scrapper.DeleteVoteForPhoto(cmdParser.UnvotePhoto())

        if cmdParser.VoteForFresh()!="":
            scrapper.VoteFreshOrUpcoming(True, int(cmdParser.VoteForFresh()))

        if cmdParser.VoteForUpcoming()!="":
            scrapper.VoteFreshOrUpcoming(False, int(cmdParser.VoteForUpcoming()))

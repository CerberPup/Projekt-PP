import jsonpickle
import os
from datetime import datetime

class User(object):
    def __init__(self, username, userID, follows_since, following_since,likes):
        self.username=username
        self.userID=userID
        self.follows_since=follows_since
        self.following_since=following_since
        self.likes=likes
        self.likes_amount = len(likes)

    def SetLikes(self, likes):
        self.likes=likes
        self.likes_amount=len(likes)

    def __str__(self):
        return "username: {} \n userID: {} \n follows_since: {} \n following_since: {} \n likes: {} \n likes_amount: {}".format(self.username, self.userID, self.follows_since, self.following_since, self.likes, self.likes_amount)

class Users(object):
    def __init__(self, users):
        self.users = users
        self.users_amount=len(users)

class DatabaseManager(object):
    def __init__(self, scrapper):
        self.scrapper = scrapper
        self.dbPath = self.scrapper.dbDir
        self.usersDict = {}
        self.Users = None
        self.recordFile = self.dbPath+'/record_'+datetime.now().strftime('%Y-%m-%d_%H:%M:%S')

    def CreateUsersList(self):
       followers = self.scrapper.getFollowers()
       followings = self.scrapper.getFollowings()
       for follower in followers:
           self.usersDict[follower['id']] = User(follower['username'], follower['id'], datetime.now().strftime('%Y-%m-%d_%H:%M:%S'),"",[])
       for following in followings:
           if following['id'] in self.usersDict.keys():
               user = self.usersDict[following['id']]
               user.following_since=datetime.now().strftime('%Y-%m-%d_%H:%M:%S')
               self.usersDict[following['id']] =user
           else:
               self.usersDict[following['id']] = User(following['username'], following['id'], "",datetime.now().strftime('%Y-%m-%d_%H:%M:%S'),[])

       self.UpdateFollowInfo()
       self.UpdatePhotoInfo()
       self.Users = Users(self.usersDict.values())
       with open(self.recordFile, 'w') as f:
           f.write(jsonpickle.encode(self.Users))

    def UpdateFollowInfo(self):
        files = sorted([f for f in os.listdir(self.dbPath) if os.path.isfile(os.path.join(self.dbPath, f))])
        try:
            latest = files[len(files)-1]
        except IndexError:
            latest=""
        if latest!="":
            latestRecord = open(os.path.join(self.dbPath, latest),'r')
            latestRecord_json = jsonpickle.loads(latestRecord.readline())
            for CurUser in self.usersDict.keys():
                for DbUser in latestRecord_json.users:
                    if CurUser == DbUser.userID:
                        user = self.usersDict[CurUser]
                        user.following_since=DbUser.following_since
                        user.follows_since=DbUser.follows_since
                        self.usersDict[CurUser]=user

    def UpdatePhotoInfo(self):
        galleries = self.scrapper.GetPhotosForUser(self.scrapper.UserData['username'])
        photosDict = {}
        for gallery in galleries:
            photos = galleries[gallery]
            for photo in photos:
                photosDict[photo['id']] = self.scrapper.GetVotesForPhoto(photo['id'])
        photos = self.scrapper.GetUnassignedPhotosForUser(self.scrapper.UserData['username'], self.scrapper.UserData['id'])
        for photo in photos:
            photosDict[photo['id']] = self.scrapper.GetVotesForPhoto(photo['id'])
        for user in self.usersDict.values():
            likes=[]
            for photoID in photosDict.keys():
                votes = photosDict[photoID]
                for vote in votes:
                    if user.username == vote['username']:
                        likes.append(photoID)
            user.SetLikes(likes)

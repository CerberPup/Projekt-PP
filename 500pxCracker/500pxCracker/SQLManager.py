try:
    import sqlite3
    from Logger import Logger
    import os
    import datetime
    from collections import OrderedDict
except ImportError:
    try:
        with open ("error.log", 'a+') as f:
            f.write("Import error in SQLManager.py")
    except Exception:
        print("Import error in SQLManager.py")

scriptDir = os.path.abspath(os.path.dirname(__file__))


class SQLManager(object):

    usersTableName = 'Users'
    usersScheme = OrderedDict()
    usersScheme['name']='text'
    usersScheme['fullname']= 'text'
    usersScheme['following_since']='text'
    usersScheme['follower_since']='text'
    usersPrimaryKey = ('userID', 'INTEGER')
    likesTableName = 'Likes'
    likesPrimaryKey = ('LikeID', 'INTEGER')
    likesScheme  = OrderedDict()
    likesScheme['userID'] = 'INTEGER'
    likesScheme['photoID'] = 'INTEGER'
    likesScheme['liked'] = 'text'
    likesForeignKey= ('userID', usersTableName, usersPrimaryKey[0])

    def __init__(self, dbPath, logPath, debugMode, scrapper):
        self.dbPath = dbPath
        self.dbConection = sqlite3.connect(dbPath)
        self.dbCursor = self.dbConection.cursor()
        self.Logger = Logger(logPath, 'dbLog', debugMode)
        self.scrapper = scrapper
        self.CreateTables()


    def CreateTable(self, tableName, primaryKey = (), foreignKey=(), dataScheme={}, Unique=()):
        createString = 'CREATE TABLE IF NOT EXISTS ' + tableName + ' ('
        if primaryKey != ():
            createString += primaryKey[0] + ' ' + primaryKey[1] + ' PRIMARY KEY, '
        for name in dataScheme.keys():
            createString+= name + ' ' + dataScheme[name]+', '
        createString = createString.rstrip(', ')
        if foreignKey != ():
            createString+= ', FOREIGN KEY (' + foreignKey[0] +') REFERENCES ' +foreignKey[1]+'('+foreignKey[2]+')'
        if Unique != ():
            createString += ', UNIQUE ('
            for item in Unique:
                createString += item + ', '
            createString = createString.rstrip(', ')
            createString += ')'
        createString += ')'

        try:
            self.dbCursor.execute(createString)
            self.Logger.LogLine("Executed: " + createString)
        except sqlite3.OperationalError as e:
            self.Logger.LogLine("Exception occured: " + str(e))

    def CreateTables(self):
        self.CreateTable(SQLManager.usersTableName, primaryKey=SQLManager.usersPrimaryKey,dataScheme=SQLManager.usersScheme)
        self.CreateTable(SQLManager.likesTableName, Unique=(SQLManager.usersPrimaryKey[0], list(SQLManager.likesScheme.keys())[1]),
                                                            foreignKey=SQLManager.likesForeignKey, dataScheme=SQLManager.likesScheme)

    def InsertUsers(self, Users=[]):
        try:
            self.dbCursor.executemany('INSERT into '+ SQLManager.usersTableName + ' VALUES (?,?,?,?,?)', Users)
            for user in Users:
                self.Logger.LogLine("Added: ( {} | {} | {} | {} | {} )".format(*user))
            self.dbConection.commit()
            self.Logger.LogLine("commited")
        except sqlite3.IntegrityError as e:
            self.Logger.LogLine("Integrity error: " + str(e))

    def ReplaceUsers(self, Users=[]):
        try:
            self.dbCursor.executemany('REPLACE into '+ SQLManager.usersTableName + ' VALUES (?,?,?,?,?)', Users)
            self.dbConection.commit()
            self.Logger.LogLine("Users replace commited")
        except sqlite3.IntegrityError as e:
            self.Logger.LogLine("Integrity error: " + str(e))


    def InsertLikes(self, Likes=[]):
        try:
            self.dbCursor.executemany('INSERT into '+ SQLManager.likesTableName + ' VALUES (?,?,?)', Likes)
            self.dbConection.commit()
            self.Logger.LogLine("Likes commited")
        except sqlite3.IntegrityError as e:
            self.Logger.LogLine("Integrity error: " + str(e))

    def SelectUsersWhere(self, Column, Value, GetColumn='*'):
        if Value == '':
            Value = "''"
        query = "SELECT {3} FROM {0} WHERE {1}={2}".format(SQLManager.usersTableName, Column, Value, GetColumn)
        self.Logger.LogLine( query)
        self.dbCursor.execute(query)
        retVal = self.dbCursor.fetchall()
        return retVal

    def SelectUsersWhereNot(self, FilterColumn, FilterValue, Column='*'):
        if FilterValue == '':
            FilterValue = "''"
        query = "SELECT {3} FROM {0} WHERE {1}!={2}".format(SQLManager.usersTableName, FilterColumn, FilterValue, Column)
        self.Logger.LogLine( query)
        self.dbCursor.execute(query)
        retVal = self.dbCursor.fetchall()
        return retVal


    def UpdateFollowers(self):
        dbUsersIDs = self.SelectUsersWhereNot(list(SQLManager.usersScheme.keys())[3], '', SQLManager.usersPrimaryKey[0])
        usersDict = {}
        webUsers = self.scrapper.getFollowers()
        #delete already registered followers
        for webUser in webUsers:
            userID = int(webUser['id'])
            userIDTuple=(userID,)
            if userIDTuple not in dbUsersIDs:
                usersDict[userID] = webUser
        curDateTime=datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        dbUsers = self.SelectUsersWhere(list(SQLManager.usersScheme)[3], '')
        newUsers=[]
        #update data for followings who started following us
        for user in dbUsers:
            if user[0] in usersDict.keys():
                newUser = (user[0], user[1], user[2], user[3], curDateTime)
                del usersDict[user[0]]
                newUsers.append(newUser)
        #create new records for new followers
        for user in usersDict.values():
            newUser = (user['id'], user['username'], user['fullname'], "", curDateTime)
            newUsers.append(newUser)
        self.ReplaceUsers(newUsers)
        return webUsers

    def UpdateFollowings(self):
        dbUsersIDs = self.SelectUsersWhereNot(list(SQLManager.usersScheme.keys())[2], '', SQLManager.usersPrimaryKey[0])
        usersDict = {}
        webUsers = self.scrapper.getFollowings()
        # delete already registered followings
        for webUser in webUsers:
            userID = int(webUser['id'])
            userIDTuple = (userID,)
            if userIDTuple not in dbUsersIDs:
                usersDict[userID] = webUser
        curDateTime = datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        dbUsers = self.SelectUsersWhere(list(SQLManager.usersScheme)[2], '')
        newUsers = []
        # update data for follwers which we have started following
        for user in dbUsers:
            if user[0] in usersDict.keys():
                newUser = (user[0], user[1], user[2], curDateTime, user[4])
                del usersDict[user[0]]
                newUsers.append(newUser)
        # create new records for new followers
        for user in usersDict.values():
            newUser = (user['id'], user['username'], user['fullname'], curDateTime, "")
            newUsers.append(newUser)
        self.ReplaceUsers(newUsers)
        return webUsers

    def CheckUsers(self, followers, followings):
        followersIDs=[]
        followingsIDs=[]
        for follower in followers:
            if follower is None:
                continue
            followerID = int(follower['id'])
            if followerID not in followersIDs:
                followersIDs.append(followerID)
        for following in followings:
            if following is None:
                continue
            followingID = int(following['id'])
            if followingID not in followingsIDs:
                followingsIDs.append(followingID)
        followersDb = self.SelectUsersWhereNot(list(SQLManager.usersScheme.keys())[3], '')
        followersToUpdate = []
        followingsToUpdate=[]
        for followerDb in followersDb:
            if followerDb[0] not in followersIDs:
               followersToUpdate.append((followerDb[0], followerDb[1], followerDb[2], followerDb[3],""))
        self.ReplaceUsers(followersToUpdate)
        followingsDb = self.SelectUsersWhereNot(list(SQLManager.usersScheme.keys())[2], '')
        for followingDb in followingsDb:
            if followingDb[0] not in followingsIDs:
               followingsToUpdate.append((followingDb[0], followingDb[1], followingDb[2], "",followingDb[4]))
        self.ReplaceUsers(followingsToUpdate)
        deleteQuery = "DELETE FROM {0} WHERE {1} = '' AND {2} = ''".format(SQLManager.usersTableName, list(SQLManager.usersScheme.keys())[2], list(SQLManager.usersScheme.keys())[3])
        self.dbCursor.execute(deleteQuery)
        self.dbConection.commit()



    def UpdatePhotoInfo(self):
        curDateTime = datetime.datetime.now().strftime('%Y-%m-%d %H:%M:%S')
        galleries = self.scrapper.GetPhotosForUser(self.scrapper.UserData['username'])
        photosDict = {}
        for gallery in galleries:
            photos = galleries[gallery]
            for photo in photos:
                photosDict[photo['id']] = self.scrapper.GetVotesForPhoto(photo['id'])
        photos = self.scrapper.GetUnassignedPhotosForUser(self.scrapper.UserData['username'], self.scrapper.UserData['id'])
        currentLikes = self.dbCursor.execute("SELECT {1}, {2} FROM {0}".format(SQLManager.likesTableName, list(SQLManager.likesScheme.keys())[0],list(SQLManager.likesScheme.keys())[1])).fetchall()
        likes=[]
        for photo in photos:
            photosDict[photo['id']] = self.scrapper.GetVotesForPhoto(photo['id'])
        for photo in photosDict.items():
            for like in photo[1]:
                newLike = (like['id'], photo[0])
                if newLike not in currentLikes:
                    likes.append((newLike[0], newLike[1],curDateTime))
        self.InsertLikes(likes)

    def PrintDB(self):
        for row in self.dbCursor.execute("SELECT * FROM {}".format(SQLManager.usersTableName)):
            print(row)
        for row in self.dbCursor.execute("SELECT * FROM {} ".format(SQLManager.likesTableName)):
            print ( row )
from Logger import *
import requests, time, json, os, shutil
from bs4 import BeautifulSoup
import sys
from pprint import pprint
import random
import pickle


def cleanupDir(fullpath):
    for the_file in os.listdir(fullpath):
        file_path = os.path.join(fullpath, the_file)
        try:
            if os.path.isfile(file_path):
                os.unlink(file_path)
            elif os.path.isdir(file_path):
                shutil.rmtree(file_path)
        except Exception as e:
            continue

scriptDir = os.path.abspath(os.path.dirname(__file__))

class Scrapper:

    userInfo = scriptDir + '/UserInfo'
    photosDir = scriptDir + '/photosDumps'
    galleriesDir = scriptDir + '/galleriesDumps'
    likesDir = scriptDir + '/likesForPhotos'

    def __init__(self, email, password, debugMode, offlineMode):
        self.userDir = self.userInfo + '/' + email
        self.followingDir = self.userDir + '/followings'
        self.followersDir = self.userDir + '/followers'
        self.dbDir = self.userDir + '/db'
        self.sessionObject =  self.userDir + '/.session'
        self.payloadObject =  self.userDir + '/.payload'
        self.csrfObject =  self.userDir + '/.csrf'
        self.UserDataObject= self.userDir + '/.usr'
        self.logger = Logger(self.userDir, "log", debugMode)
        self.session = requests.Session()
        self.session.headers.update({
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36'
        })
        self.payload = dict()
        self.csrfHeaders = dict()
        self.payload['session[email]'] = email
        self.payload['session[password]'] = password
        self.payload['authenticity_token'] = ''
        self.csrfHeaders['X-CSRF-Token'] = ''
        self.csrfHeaders['X-Requested-With'] = 'XMLHttpRequest'
        self.pendingFollowList = []
        self.acceptedFollowList = []
        self.ignoredFollowList = []
        self.web = None
        self.UserData = None
        if not offlineMode:
            if not self.readSessionObject():
                self._retrieveToken()
                self._login()
            self.dumpSessionObject()

        if not os.path.exists(Scrapper.photosDir):
            os.makedirs(Scrapper.photosDir)
        if not os.path.exists(Scrapper.galleriesDir):
            os.makedirs(Scrapper.galleriesDir)
        if not os.path.exists(Scrapper.likesDir):
            os.makedirs(Scrapper.likesDir)
        if not os.path.exists(self.dbDir):
            os.makedirs(self.dbDir)

    def cleanupTempFiles(self):
        if os.path.exists(Scrapper.photosDir):
            cleanupDir(Scrapper.photosDir)
        if os.path.exists(Scrapper.galleriesDir):
            cleanupDir(Scrapper.photosDir)
        if os.path.exists(Scrapper.likesDir):
            cleanupDir(Scrapper.photosDir)

    def dumpSessionObject(self):
        sessionDumpFile = open(self.sessionObject,'wb')
        payloadDumpFile = open(self.payloadObject,'wb')
        csrfDumpFile =  open(self.csrfObject, 'wb')
        userDataDumpFile = open(self.UserDataObject,'wb')
        pickle.dump(self.session, sessionDumpFile , pickle.HIGHEST_PROTOCOL)
        pickle.dump(self.payload, payloadDumpFile, pickle.HIGHEST_PROTOCOL)
        pickle.dump(self.csrfHeaders, csrfDumpFile , pickle.HIGHEST_PROTOCOL)
        pickle.dump(self.UserData, userDataDumpFile, pickle.HIGHEST_PROTOCOL)

    def readSessionObject(self):
        self.logger.LogLine("Attempt to recover last session...")
        try:
            sessionDumpFile = open(self.sessionObject, 'rb')
            payloadDumpFile = open(self.payloadObject, 'rb')
            csrfDumpFile = open(self.csrfObject, 'rb')
            userDataDumpFile = open(self.UserDataObject, 'rb')
            self.session = pickle.load(sessionDumpFile)
            self.payload = pickle.load(payloadDumpFile)
            self.csrfHeaders = pickle.load(csrfDumpFile)
            self.UserData= pickle.load(userDataDumpFile)
        except IOError:
            self.logger.LogLine("No session to recover")
            return False
        valid = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(
                self.UserData['id']) + '/followers?fullformat=0&page=' + str(1), headers=self.csrfHeaders)
        if valid.status_code == 200:
            self.logger.LogLine("Session recovered successfully")
            return True
        else:
            self.logger.LogLine("Session expired")
        return False

    def getFollowings(self):
        pageNum = 1
        following = []
        self.logger.LogLine("Attempt to retrieve followings list...")
        if not os.path.exists(self.followingDir):
            os.makedirs(self.followingDir)
        else:
            files = [f for f in os.listdir(self.followingDir) if os.path.isfile(os.path.join(self.followingDir, f))]
            for file in files:
                os.remove(os.path.join(self.followingDir, file))
        while True:
            followingPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(
                self.UserData['id']) + '/friends?fullformat=0&page=' + str(pageNum), headers=self.csrfHeaders)
            if followingPage.status_code == 200:
                self.logger.LogLine("Succesfully retrieved followings page: " + str(pageNum))
                followingPage_json = json.loads(followingPage.text)
                following += followingPage_json['friends']
                if pageNum == followingPage_json['friends_pages']:
                    break
                pageNum += 1
                time.sleep(3)
            else:
                self.logger.LogLine("Unable to retrieve followings lists at " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followingPage.url))
        for user in following:
            followingFile = u"{}".format(self.followingDir + '/' + user['username'])
            with open (followingFile,'w') as f:
                f.write(json.dumps(user))
        return following

    def getFollowers(self):
        pageNum = 1
        followers = []
        self.logger.LogLine("Attempt to retrieve followers list...")
        if not os.path.exists(self.followersDir):
            os.makedirs(self.followersDir)
        else:
            files = [f for f in os.listdir(self.followersDir) if os.path.isfile(os.path.join(self.followersDir, f))]
            for file in files:
                os.remove(os.path.join(self.followersDir, file))
        while True:
            followersPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(
                self.UserData['id']) + '/followers?fullformat=0&page=' + str(pageNum), headers=self.csrfHeaders)
            if followersPage.status_code == 200:
                self.logger.LogLine("Succesfully retrieved followers page: " + str(pageNum))
                followersPage_json = json.loads(followersPage.text)
                followers += followersPage_json['followers']
                if pageNum == followersPage_json['followers_pages']:
                    break
                pageNum += 1
                time.sleep(3)
            else:
                self.logger.LogLine("Unable to retrieve followings lists at " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followersPage.url))
        for user in followers:
            followerFile = u"{}".format(self.followersDir + '/' + user['username'])
            with open (followerFile,'w') as f:
                f.write(json.dumps(user))
        return followers

    def ParseFollowingsFiles(self):
        followings = []
        for file in os.listdir(self.followingDir):
            filePath = os.path.join(self.followingDir, file)
            with open (filePath, 'r') as f:
                for line in f:
                    followings.append(json.loads(line)['username'])

        return followings

    def requestWebPage(self, method, url, data={}, headers={}, checkStatusCode=True, Retries=5):
        retriesCounter=0
        while True:
            retriesCounter=retriesCounter+1
            try:
                response = self.session.request(method, url, data=data, headers=headers, timeout=5)
            except requests.exceptions.RequestException:
                self.logger.LogLine('Requested page is not responding, retrying...')
                time.sleep(5)
                continue
            if Retries>0:
                if retriesCounter > Retries:
                    self.logger.LogLine("Request web page reached limit of retries.")
                    self.logger.LogLine(url + " " + str(retriesCounter))
            if retriesCounter > Retries:
                return response
            if response.status_code == 429:
                self.logger.LogLine("Too many requests in time, please shut down scrapper and try again later")
                time.sleep(600)
            if checkStatusCode and response.status_code != 200:
                self.logger.LogLine('Requested URL error: (' + str(response.status_code) + ') occured. Retrying...')
                self.logger.LogLine('Error URL: ' + response.url)
                time.sleep(5)
                continue
            return response

    def _retrieveToken(self):
        self.logger.LogLine("Attempt to retrieve token. Requesting login page...")
        self.web = self.requestWebPage('GET', 'https://500px.com/login')
        time.sleep(2)
        if self.web.status_code == 200:
            self.logger.LogLine("Requested login page")
            web_soup = BeautifulSoup(self.web.text, 'html.parser')
            self.payload['authenticity_token'] = web_soup.find('meta', {'name': 'csrf-token'}).get('content')
            self.csrfHeaders['X-CSRF-Token'] = self.payload['authenticity_token']
        else:
            self.logger.LogLine("Unable to retrieve token: " + str(self.web.status_code))
            self.logger.LogLine("URL: " + str(self.web.url))

    def _login(self):
        self.logger.LogLine("Attempt to log in...")
        self.web = self.requestWebPage('POST', 'https://api.500px.com/v1/session', data = self.payload)
        time.sleep(2)
        if self.web.status_code == 200:
            self.UserData = json.loads(self.web.text)['user']
            self.logger.LogLine("Logged in as: " + self.UserData['username'])
            self.logger.LogLine("User ID: " + str(self.UserData['id']))
        else:
            self.logger.LogLine("Unable to log in : " + str(self.web.status_code))
            self.logger.LogLine("URL: " + str(self.web.url))

    def FollowUser(self, targetUserName):
        self.logger.LogLine("Attempt to follow user: " + targetUserName)
        acceptPage = self.requestWebPage('POST', 'https://500px.com/' + targetUserName + '/follow', headers=self.csrfHeaders, Retries=0)
        if acceptPage.status_code == 200:
           self.logger.LogLine("Followed successfully")
        elif acceptPage.status_code == 403:
           self.logger.LogLine("The user requested has been disabled or already in followers list.")
        elif acceptPage.status_code == 404:
           self.logger.LogLine("User does not exist")
        else:
           self.logger.LogLine('A server error (' + str(acceptPage.status_code) + ') occured. Retrying...')
           self.logger.LogLine('Error URL: ' + acceptPage.url)

    def UnfollowUser(self, targetUserName):
        self.logger.LogLine("Attempt to unfollow user: " + targetUserName)
        acceptPage = self.requestWebPage('POST', 'https://500px.com/' + targetUserName + '/unfollow',
                                         headers=self.csrfHeaders, Retries=3)
        if acceptPage.status_code == 200:
            self.logger.LogLine("Unfollowed successfully")
        elif acceptPage.status_code == 403:
            self.logger.LogLine("The user requested has been disabled or not present in followers list.")
        elif acceptPage.status_code == 404:
            self.logger.LogLine("User does not exist")
        else:
            self.logger.LogLine('A server error (' + str(acceptPage.status_code) + ') occured. Retrying...')
            self.logger.LogLine('Error URL: ' + acceptPage.url)

    def GetUserInfo(self, username):
        UserInfo = {}
        infoPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/show?username=' + username, data=self.payload)
        self.logger.LogLine("Attempt to get info for " + username)
        if infoPage.status_code == 200:
            self.logger.LogLine("Get info about user succesful.")
            UserInfo_json = json.loads(infoPage.text)
            UserInfo=UserInfo_json['user']
            return UserInfo
        else:
            self.logger.LogLine("Unable to get info about user")

    def GetPhotosGalleriesForUser(self, id):
        Galleries = []
        page=1
        jsonFile = Scrapper.galleriesDir + '/' + str(id) + '_galleries'
        if os.path.isfile(jsonFile):
            os.remove(jsonFile)
        while True:
            galleryPage= self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(id) + '/galleries?sort=last_added_to_at&sort_direction=desc&page='+ str(page), data=self.payload)
            self.logger.LogLine("Attempt to get galleries for user " + str(id))
            if galleryPage.status_code == 200:
                galleryPage_json = json.loads(galleryPage.text)
                with open(jsonFile, 'a') as f:
                    f.write(json.dumps(galleryPage_json['galleries']))
                Galleries += galleryPage_json['galleries']
                self.logger.LogLine("Galleries retrieved succesfully")
                if page == galleryPage_json['total_pages']:
                    break
                page=page+1
            else:
                self.logger.LogLine("Unable to get galleries")
                break
        return Galleries

    def GetItemsForGallery(self, UserId, GalleryId):
        Photos=[]
        page=1
        dir = Scrapper.photosDir + '/User' + str(UserId)
        if not os.path.exists(dir):
            os.makedirs(dir)
        galleryDir = dir + '/Gallery_' + str(GalleryId)
        if not os.path.exists(galleryDir):
            os.makedirs(galleryDir)
        json_file = galleryDir + '/photos'
        if os.path.isfile(json_file):
            os.remove(json_file)
        while True:
            photosPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(UserId) + '/galleries/' + str(GalleryId) + '/items?sort=created_at&sort_direction=desc&page='+ str(page), data=self.payload)
            self.logger.LogLine("Attempt to get photos for user " + str(UserId) + " in gallery " + str(GalleryId))
            if photosPage.status_code == 200:
                photosPage_json = json.loads(photosPage.text)
                with open(json_file, 'a') as f:
                    f.write(json.dumps(photosPage_json))
                Photos += photosPage_json['photos']
                self.logger.LogLine("Galleries retrieved succesfully")
                if page == photosPage_json['total_pages']:
                    break
                page=page+1
            else:
                self.logger.LogLine("Unable to get galleries")
                break
        return Photos

    def GetPhotosForUser(self, username):

        ID = self.GetUserInfo(username)['id']
        self.GetUnassignedPhotosForUser(username, ID)
        galleries = self.GetPhotosGalleriesForUser(ID)
        retDict= {}
        for gallery in galleries:
            retDict[gallery['id']] = self.GetItemsForGallery(ID, gallery['id'])
        return retDict

    def GetUnassignedPhotosForUser(self, username, userID):

        Photos=[]
        dir = Scrapper.photosDir + '/User' + str(userID)
        APIURL = 'https://api.500px.com/v1/photos?feature=user&username='
        if not os.path.exists(dir):
            os.makedirs(dir)
        jsonFile = dir + '/photos_unassigned'
        if os.path.isfile(jsonFile):
            os.remove(jsonFile)
        page=1
        self.logger.LogLine("Attempt to get photos for user " + str(username))
        while True:
            PhotosPage = self.requestWebPage('GET', APIURL + str(username) + '&page=' + str(page) + '&rpp=100', data=self.payload)
            if PhotosPage.status_code == 200:
                photosPage_json = json.loads(PhotosPage.text)
                total_pages = photosPage_json['total_pages']
                with open(jsonFile, 'a') as f:
                    f.write(json.dumps(photosPage_json))
                Photos+=photosPage_json['photos']
                self.logger.LogLine("Photos retrieved successfully")
                if page == total_pages:
                    break
                self.logger.LogLine("Page " + str(page) + '/' + str(total_pages))
                page=page+1
            else:
                self.logger.LogLine("Unable to get photos")
                break
        return Photos

    def GetVotesForPhoto(self, photoID):
        Votes=[]
        page=1
        photoFile = Scrapper.likesDir + '/' + str(photoID)
        if os.path.isfile(photoFile):
            os.remove(photoFile)
        while True:
            likesPage = self.requestWebPage('GET', 'https://api.500px.com/v1/photos/' + str(photoID) + '/votes?page='+ str(page), data=self.payload)
            self.logger.LogLine("Attempt to get votes for photo " + str(photoID))
            if likesPage.status_code == 200:
                likesPage_json= json.loads(likesPage.text)
                with open(photoFile, 'a') as f:
                    f.write(json.dumps(likesPage_json['users']))
                Votes+= likesPage_json['users']
                self.logger.LogLine("Votes retrieved succesfully")
                if page == likesPage_json['total_pages']:
                    break
                page=page+1
            else:
                self.logger.LogLine("Unable to get galleries")
                break
        return Votes

    #Like = True - like photo, False - dislike
    def VoteForPhoto(self, photoID, Like=True):
        votePage = self.requestWebPage("POST", 'https://api.500px.com/v1/photos/' + str(photoID) + '/vote?vote=' + str(int(Like)),data=self.payload, Retries=0)
        self.logger.LogLine("Attempt to vote for photo: " + str(photoID) + " Like: " + str(Like))
        if votePage.status_code==200:
            self.logger.LogLine("Voted successfully")
        elif votePage.status_code==400:
            self.logger.LogLine("Invalid request")
        elif votePage.status_code==403:
            self.logger.LogLine("The vote has been rejected; common reasons are: current user is inactive, has not completed their profile, is trying to vote on their own photo, or has already voted for the photo.")
        elif votePage.status_code==404:
            self.logger.LogLine("Photo does not exists")
        else:
            self.logger.LogLine("Unexpected error: " + str (votePage.status_code))
        return votePage.status_code==200

    def DeleteVoteForPhoto(self, photoID):
        votePage = self.requestWebPage("DELETE", 'https://api.500px.com/v1/photos/' + str(photoID) + '/vote',data=self.payload, Retries=0)
        self.logger.LogLine("Attempt to delete vote for photo: " + str(photoID))
        if votePage.status_code==200:
            self.logger.LogLine("Unvoted successfully")
        elif votePage.status_code==404:
            self.logger.LogLine("The requested photo does not exist or was deleted")
        else:
            self.logger.LogLine("Unexpected error: " + str (votePage.status_code))

    def GetPhotosFromFresh(self,startPage=1, amount=50):
        Photos = []
        file = self.photosDir + '/FreshPhotos'
        if os.path.isfile(file):
            os.remove(file)
        page = startPage
        while amount>0:
            URL = 'https://webapi.500px.com/discovery/fresh?rpp=50&feature=fresh&image_size[]=1&image_size[]=2&image_size[]=32&image_size[]=31&image_size[]=33&image_size[]=34&image_size[]=35&image_size[]=36&image_size[]=2048&image_size[]=4&image_size[]=14&sort=&include_states=true&include_licensing=false&formats=jpeg,lytro&only=&exclude=&personalized_categories=false&page='+ str(page) + '&rpp=50'
            self.logger.LogLine("Attempt to get photos from Fresh, page " + str(page))
            response = self.requestWebPage("GET", URL, data=self.payload)
            if response.status_code == 200:
                self.logger.LogLine("Photos retrieved successfully")
                response_json = json.loads(response.text)
                with open(file, 'a') as f:
                    f.write(json.dumps(response_json['photos']))
                Photos += response_json['photos']
            else:
                self.logger.LogLine("Error in retrieving photos: " + str(response.status_code))
            page+=1
            amount-=50
        return Photos

    def GetPhotosFromUpcoming(self,startPage=1, amount=50):
        Photos = []
        file = self.photosDir + '/UpcomingPhotos'
        if os.path.isfile(file):
            os.remove(file)
        page = startPage
        while amount>0:
            URL = 'https://api.500px.com/v1/photos?rpp=50&feature=upcoming&image_size[]=1&image_size[]=2&image_size[]=32&image_size[]=31&image_size[]=33&image_size[]=34&image_size[]=35&image_size[]=36&image_size[]=2048&image_size[]=4&image_size[]=14&sort=&include_states=true&include_licensing=false&formats=jpeg,lytro&only=&exclude=&personalized_categories=false&page='+ str(page) + '&rpp=50'
            self.logger.LogLine("Attempt to get photos from Upcoming, page " + str(page))
            response = self.requestWebPage("GET", URL, data=self.payload)
            if response.status_code == 200:
                self.logger.LogLine("Photos retrieved successfully")
                response_json = json.loads(response.text)
                with open(file, 'a') as f:
                    f.write(json.dumps(response_json['photos']))
                Photos += response_json['photos']
            else:
                self.logger.LogLine("Error in retrieving photos: " + str(response.status_code))
            page+=1
            amount-=50
        return Photos

    def PhotoIsLiked(self, photoID):
        votes = self.GetVotesForPhoto(int(photoID))
        for vote in votes:
            if vote['username'] == self.UserData['username']:
                return True
        return False

    def VoteFreshOrUpcoming(self, fresh=True, amount=100):
        Photos=[]
        page=1
        if fresh:
            self.logger.LogLine("Attempt to like " + str(amount) + " photos from fresh")
        else:
            self.logger.LogLine("Attempt to like " + str(amount) + " photos from upcoming")

        while amount>0:
            if fresh:
                Photos = self.GetPhotosFromFresh(page, amount)
            else:
                Photos = self.GetPhotosFromUpcoming(page, amount)
            page += int(round(amount / 50.0 + 0.499))
            for photo in Photos:
                if not self.PhotoIsLiked(photo['id']):
                    if random.randint(0,100) > 20:
                        if self.VoteForPhoto(photo['id']):
                            amount-=1

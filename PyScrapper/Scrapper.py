from Logger import *
import requests, time, json, os
from bs4 import BeautifulSoup
import sys

scriptDir = os.path.abspath(os.path.dirname(__file__))

class Scrapper:

    userDir = scriptDir + '/UserInfo'
    followingFile = userDir + '/following.json'
    followersFile = userDir + '/followers.json'
    photosDir = scriptDir + '/photosDumps'
    galleriesDir = scriptDir + '/galleriesDumps'

    def __init__(self, email, password, debugMode):
        self.logger = Logger(Scrapper.userDir, "log_"+email, debugMode)
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
        self._retrieveToken()
        self._login()
        if not os.path.exists(Scrapper.photosDir):
            os.makedirs(Scrapper.photosDir)
        if not os.path.exists(Scrapper.galleriesDir):
            os.makedirs(Scrapper.galleriesDir)

    def getFollowings(self):
        pageNum = 1
        following = []
        self.logger.LogLine("Attempt to retrieve followings list...")
        if os.path.isfile(Scrapper.followingFile):
            os.remove(Scrapper.followingFile)
        while True:
            followingPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(
                self.UserData['id']) + '/friends?fullformat=0&page=' + str(pageNum), headers=self.csrfHeaders)
            if followingPage.status_code == 200:
                self.logger.LogLine("Succesfully retrieved followings page: " + str(pageNum))
                followingPage_json = json.loads(followingPage.text)
                with open(Scrapper.followingFile, 'a') as f:
                    f.write(json.dumps(followingPage_json['friends']))
                following += followingPage_json['friends']
                if pageNum == followingPage_json['friends_pages']:
                    break
                pageNum += 1
                time.sleep(20)
            else:
                self.logger.LogLine("Unable to retrieve followings lists at " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followingPage.url))
        return following

    def getFollowers(self):
        pageNum = 1
        followers = []
        self.logger.LogLine("Attempt to retrieve followers list...")
        if os.path.isfile(Scrapper.followersFile):
            os.remove(Scrapper.followersFile)
        while True:
            followersPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(
                self.UserData['id']) + '/followers?fullformat=0&page=' + str(pageNum), headers=self.csrfHeaders)
            if followersPage.status_code == 200:
                self.logger.LogLine("Succesfully retrieved followers at page: " + str(pageNum))
                followersPage_json = json.loads(followersPage.text)
                with open(Scrapper.followersFile, 'a') as f:
                    f.write(json.dumps(followersPage_json['followers']))
                followers += followersPage_json['followers']
                if pageNum == followersPage_json['followers_pages']:
                    break
                pageNum += 1
                time.sleep(20)
            else:
                self.logger.LogLine("Unable to retrieve followers at page: " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followersPage.url))
        return followers

    def requestWebPage(self, method, url, data={}, headers={}, checkStatusCode=True, Retries=10):
        retriesCounter=0
        while True:
            retriesCounter=retriesCounter+1
            try:
                response = self.session.request(method, url, data=data, headers=headers, timeout=5)
            except requests.exceptions.RequestException:
                self.logger.LogLine('Requested page is not responding, retrying...')
                time.sleep(5)
                continue
            if retriesCounter > Retries:
                self.logger.LogLine("Request web page reached limit of retries. Aborting...")
                self.logger.LogLine(url + " " + str(retriesCounter))
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
        time.sleep(20)
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
        time.sleep(20)
        if self.web.status_code == 200:
            self.UserData = json.loads(self.web.text)['user']
            self.logger.LogLine("Logged in as: " + self.UserData['username'])
            self.logger.LogLine("User ID: " + str(self.UserData['id']))
        else:
            self.logger.LogLine("Unable to log in : " + str(self.web.status_code))
            self.logger.LogLine("URL: " + str(self.web.url))

    def FollowUser(self, userID):
        self.logger.LogLine("Attempt to follow user: " + str(userID))
        acceptPage = self.requestWebPage('POST', 'https://api.500px.com/v1/users/' + str(userID) + '/friends', data = self.payload, Retries=3)
        continueLoop = True
        while continueLoop:
            try:
                if acceptPage.status_code == 200:
                    self.logger.LogLine("Followed successfully")
                    continueLoop=False
                elif acceptPage.status_code == 403:
                    self.logger.LogLine("The user requested has been disabled or already in followers list.")
                    continueLoop=False
                elif acceptPage.status_code == 404:
                    self.logger.LogLine("User does not exist")
                    continueLoop=False
                else:
                    self.logger.LogLine('A server error (' + str(acceptPage.status_code) + ') occured. Retrying...')
                    self.logger.LogLine('Error URL: ' + acceptPage.url)
                    time.sleep(5)
            except requests.exceptions.RequestException:
                self.logger.LogLine('Web page timed out. Retrying...')
                time.sleep(5)
            time.sleep(20)

    def UnfollowUser(self, targetUserName):
        continueLoop = True
        while continueLoop:
            try:
                unfollowResp = self.session.post('https://500px.com/' + targetUserName + '/unfollow', timeout=5,
                                                headers=self.csrfHeaders)
                if unfollowResp.status_code == 200:
                    self.logger.LogLine('Unfollowed ' + targetUserName + '.')
                    continueLoop = False
                elif unfollowResp.status_code == 404:
                    self.logger.LogLine('User ' + targetUserName + ' no longer exists. Skipped unfollow.')
                    continueLoop = False
                else:
                    self.logger.LogLine('A server error (' + str(unfollowResp.status_code) + ') occured. Retrying...')
                    self.logger.LogLine('Error page: ' + unfollowResp.url)
                    time.sleep(5)
            except requests.exceptions.RequestException:
                self.logger.LogLine('Web page timed out. Retrying...')
                time.sleep(5)
        time.sleep(20)

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
            galleryPage= self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(id) + '/galleries?page='+ str(page), data=self.payload)
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
            photosPage = self.requestWebPage('GET', 'https://api.500px.com/v1/users/' + str(UserId) + '/galleries/' + str(GalleryId) + '/items?page='+ str(page), data=self.payload)
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
        galleries = self.GetPhotosGalleriesForUser(ID)
        for gallery in galleries:
            self.GetItemsForGallery(ID, gallery['id'])

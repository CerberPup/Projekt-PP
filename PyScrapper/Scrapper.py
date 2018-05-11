from Logger import *
import requests, time, json, os
from bs4 import BeautifulSoup
import sys
from pprint import pprint

scriptDir = os.path.abspath(os.path.dirname(__file__))

class Scrapper:

    userDir = scriptDir + '/UserInfo'
    followingDir = userDir + '/followings'
    followersDir= userDir + '/followers'
    photosDir = scriptDir + '/photosDumps'
    galleriesDir = scriptDir + '/galleriesDumps'
    likesDir = scriptDir + '/likesForPhotos'

    def __init__(self, email, password, debugMode, offlineMode):
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
        if not offlineMode:
            self._retrieveToken()
            self._login()
        if not os.path.exists(Scrapper.photosDir):
            os.makedirs(Scrapper.photosDir)
        if not os.path.exists(Scrapper.galleriesDir):
            os.makedirs(Scrapper.galleriesDir)
        if not os.path.exists(Scrapper.likesDir):
            os.makedirs(Scrapper.likesDir)

    def getFollowings(self):
        pageNum = 1
        following = []
        self.logger.LogLine("Attempt to retrieve followings list...")
        if not os.path.exists(Scrapper.followingDir):
            os.makedirs(Scrapper.followingDir)
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
                time.sleep(20)
            else:
                self.logger.LogLine("Unable to retrieve followings lists at " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followingPage.url))
        for user in following:
            followingFile = Scrapper.followingDir + '/' + user['username']
            with open (followingFile,'w') as f:
                f.write(json.dumps(user))
        return following

    def getFollowers(self):
        pageNum = 1
        followers = []
        self.logger.LogLine("Attempt to retrieve followers list...")
        if not os.path.exists(Scrapper.followersDir):
            os.makedirs(Scrapper.followersDir)
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
                time.sleep(20)
            else:
                self.logger.LogLine("Unable to retrieve followings lists at " + str(pageNum))
                self.logger.LogLine("Error URL: " + str(followersPage.url))
        for user in followers:
            followerFile = Scrapper.followersDir + '/' + user['username']
            with open (followerFile,'w') as f:
                f.write(json.dumps(user))
        return followers

    def ParseFollowingsFiles(self):
        followings = []
        for file in os.listdir(Scrapper.followingDir):
            filePath = os.path.join(Scrapper.followingDir, file)
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
        galleries = self.GetPhotosGalleriesForUser(ID)
        for gallery in galleries:
            self.GetItemsForGallery(ID, gallery['id'])

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
                self.logger.LogLine("Galleries retrieved succesfully")
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
                if photo['liked'] == False:
                    if self.VoteForPhoto(photo['id']):
                        amount-=1
                        if amount == 0:
                            return
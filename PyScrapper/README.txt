Script parses data requested from 500px to .json files.

Currently generated files:

	1. followings.json / followers.json - files with information about our followings or followers in directory UserInfo.
	2. <UserID>galleries - dump of user's galleries in directory galleriesDumps
	3. photos - dump of  photos of gallery for user in directory photosDumps organized by UserID and GalleryID.
	4.log_<email> - debug information for users logged in with email in UserInfo directory

Debug information:

	Script called without arguments or with -h parameter displays helpwith information about calling parameters. 
	To enable generating debug logs to standard output use "-debug" command argument.
	Script is retransmitting the request 10 times in case of failure. It can cause in temporary block requesting from Web - it responds with 429 status code. While using script please always look at logs if there are no problems with connection to avoid such situation. 

Dependencies:

	1. Python 2 (tested on 2.7)
	2. BeautifulSoup
	3. Requests
	4. Time
	5. Json
	6. Os

3-6 should be in Python 2 be default, to install module:
In bash/cmd/powershell : pip install <module_name>
	BeautifulSoup: pip install BeautifulSoup
May require root/administrator privileges.

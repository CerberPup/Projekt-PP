Script parses data requested from 500px to .json files.
Currently generated files:
	1. followings.json / followers.json - files with information about our followings or followers.
	2. <UserID>galleries - dump of user's galleries.
	3. photos_of_<galleryID>_gallery_user_<UserID> - dump of  photos of gallery for user.
	4.log_<email> - debug information for users logged in with email.



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

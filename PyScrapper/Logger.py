import os, time

class Logger:
    def __init__(self, path, name, debugMode):
        self.filePath = path
        self.fileName = name
        self.LogFile = path + "/" + name
        if not os.path.exists(self.filePath):
            os.makedirs(self.filePath)
        self.DebugMode = debugMode

    def LogLine(self, line):
        logTime = time.strftime('%H:%M:%S')
        try:
            with open(self.LogFile, 'a+') as f:
                f.write(logTime + ' - ' + line + '\n')
            if self.DebugMode==True:
                print(logTime + ' - ' + line)
        except ValueError:
                pass

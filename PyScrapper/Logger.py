try:
    import os, time
except ImportError:
    try:
        with open ("error.log", 'a+') as f:
            f.write("Import error in Logger.py")
    except Exception:
        print("Import error in Logger.py")

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
        if self.DebugMode == True:
            try:
                with open(self.LogFile, 'a+') as f:
                    f.write(logTime + ' - ' + line + '\n')
                    print(logTime + ' - ' + line)
            except ValueError:
                    pass

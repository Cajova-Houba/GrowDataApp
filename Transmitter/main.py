import configparser
from growDataAppService import  GrowDataAppService
from arduinoService import ArduinoService

def load_config(filename):
    print("Loading configuration from "+filename)
    config = configparser.ConfigParser()
    config.read(filename)
    print("Done")

    return config

def main():
    config = load_config("config.ini")

    arduinoService = ArduinoService(config)
    growDataAppService = GrowDataAppService(config)

    while True:
        data = arduinoService.waitForMessage()
        parsedData = arduinoService.parseArduinoMessage(data)
        if (parsedData is None):
            print(data + " is not arduino message")
        else:
            print("Got data from arduino: "+str(parsedData))
            growDataAppService.pushData(parsedData[0], parsedData[1], parsedData[2], parsedData[3])

main()
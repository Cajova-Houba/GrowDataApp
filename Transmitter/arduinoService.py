import serial;

class ArduinoService:

    def __init__(self, config):
        self.serial = serial.Serial(config["Arduino"]["port"], 9600, timeout=1)
        self.serial.flush()
    
    def waitForMessage(self):
        msg = False
        line = ""
        while not msg:
            if self.serial.in_waiting > 0:
                line = self.serial.readline().decode('utf-8').rstrip()
                msg = True
        
        return line

    def parseArduinoMessage(self, msg):
        if (msg.startswith("Data:")):
            dataPart = msg.split(":")[1]
            data = dataPart.split(";")
            return [float(data[0]), float(data[1]), float(data[2]), float(data[3])]
        else:
            return None
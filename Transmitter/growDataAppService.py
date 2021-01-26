import requests
import jwt
import datetime

class GrowDataAppService : 

    def __init__(self, config):
        """
        Initialize this service with given config.
        """
        self.config = config

    
    def pushData(self, airTemp, airHum, soilTemp, soilHum):
        """
        Push given data to server.
        """
        print("Inserting data")
        url = self.getDataApiUrl()
        token = self.prepareJwtToken()
        data = self.prepareBody(soilHum, airHum, soilTemp, airTemp)
        print("Data: "+str(data))
        response = requests.post(url, json=data, headers={"Authorization":"Bearer "+token})
        
        if (response.status_code == 200):
            print("Done")
        else:
            print("Error: "+str(response.status_code)+"; "+response.text)

    def getDataApiUrl(self):
        return self.config["GrowDataApp"]["address"]+self.config["GrowDataApp"]["dataApiPath"]

    def prepareJwtToken(self):
        return jwt.encode(
            {
                "iss": self.config["JWT"]["Issuer"],
                "aud": self.config["JWT"]["Audience"],
                "exp": datetime.datetime.utcnow() + datetime.timedelta(seconds=30)
            },
            self.config["JWT"]["Secret"],
            algorithm="HS256"
        )
    
    def prepareBody(self, soilHum, airHum, soilTemp, airTemp):
        return {
            "Timestamp": datetime.datetime.now().strftime("%Y-%m-%dT%H:%M:%S"),
            "SoilTemperature": soilTemp,
            "AirTemperature": airTemp,
            "SoilHumidity": soilHum,
            "AirHumidity": airHum
        }